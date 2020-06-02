using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Tiny.Common.Dapper.DI;
using Tiny.Common.Dapper.Persistence.UnitOfWork;
using Tiny.OPS.Common;
using Tiny.OPS.Common.Web.XGJTools;
using Tiny.OPS.Contract;
using Tiny.OPS.Domain.Enum;
using Tiny.OPS.Domain.XGJProduct;
using Tiny.OPS.DomainService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tiny.OPS.WebApi.Controllers
{
    /// <summary>
    /// 后台手动维护数据
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class JobDataController : BaseController
    {
        private readonly ConnectTool ConnectTool = new ConnectTool();
        private readonly IT_POC_EcsThresholdDomainService thresholdDomainService = IoC.Resolve<IT_POC_EcsThresholdDomainService>();
        private readonly IInterfaceConfigDomainService XgjInterfaceConfigureService = IoC.Resolve<IInterfaceConfigDomainService>();
        private readonly IBasicDataDomainService basicDataDomainService = IoC.Resolve<IBasicDataDomainService>();

        #region 手动获取收入中心-阈值数据
        /// <summary>
        /// 手动获取收入中心-阈值数据
        /// </summary>
        /// <param name="jobData"></param>
        /// <returns></returns>
        [HttpPost]
        public BaseResponse GetThresholdData(JobDataRequest jobData)
        {
            _Log4Net.Info($"手动获取阈值数据,jobData：{JsonConvert.SerializeObject(jobData)}");
            GetThresholdDataAsync(jobData);
            return ApiSuccessResult("完成");
        }

        private async Task GetThresholdDataAsync(JobDataRequest jobData)
        {
            await Task.Run(() =>
            {
                BaseResponse response = new BaseResponse() { Code = "9999", Message = "失败" };
                try
                {
                    EcsThresholdRequest thresholdRequest = new EcsThresholdRequest()
                    {
                        appid = ConfigVal.ECSAppid,
                        EndTime = Convert.ToDateTime(DateTime.Now.ToShortDateString()),
                        StartTime = Convert.ToDateTime(jobData.QueryDate)
                    };

                    _Log4Net.Info($"GetThresholdDataAsync请求参数：{JsonConvert.SerializeObject(thresholdRequest)}");
                    string data = JsonConvert.SerializeObject(new { StartTime = thresholdRequest.StartTime, EndTime = thresholdRequest.EndTime });
                    thresholdRequest.sign = ConnectTool.ToMD5(data + ConfigVal.ECSAppsecret);

                    _Log4Net.Info($"开始请求收入中心 开始日期：{thresholdRequest.StartTime} 结束日期：{thresholdRequest.EndTime} sign：{thresholdRequest.sign}");
                    string result = ConnectTool.PostHttps(ConfigVal.ECSThresholdURL, JsonConvert.SerializeObject(thresholdRequest));
                    _Log4Net.Info($"收入中心响应：{result}");
                    EcsThresholdResponse thresholdResponse = JsonConvert.DeserializeObject<EcsThresholdResponse>(result);

                    if (thresholdResponse.IsSuccess)
                    {
                        //同步数据
                        int iCount = 0;
                        if (thresholdResponse.ResultInfo != null)
                        {
                            iCount = thresholdResponse.ResultInfo.Count();
                        }
                        _Log4Net.Info($"获取阈值数据记录数：{iCount}，开始保存数据");
                        response = thresholdDomainService.AddOrUpdateThresholdValue(thresholdResponse.ResultInfo);
                        if (response.Code.Equals("0000"))
                        {
                            _Log4Net.Info($"同步阈值数据成功:{JsonConvert.SerializeObject(response)}");
                        }
                        else
                        {
                            _Log4Net.Error($"同步阈值数据失败：{response.Message}");
                        }
                    }
                    else
                    {
                        _Log4Net.Error($"请求收入中心失败：{thresholdResponse.AlertMessage}");
                    }
                }
                catch (Exception ex)
                {
                    _Log4Net.Error($"GetThreshold失败：Message:{ex.Message} Source:{ex.Source} StackTrace:{ex.StackTrace} TargetSite:{ex.TargetSite}");
                    throw ex;
                }

            });
        }

        #endregion


        #region 手动获取产品体系
        /// <summary>
        /// 手动获取产品体系
        /// </summary>
        /// <param name="jobData"></param>
        /// <returns></returns>
        [HttpPost]
        public BaseResponse GetProductData(JobDataRequest jobData)
        {
            _Log4Net.Info($"手动获取产品体系,jobData：{JsonConvert.SerializeObject(jobData)}");
            GetProductDataAsync(jobData);
            return ApiSuccessResult("完成");

        }

        private async Task GetProductDataAsync(JobDataRequest jobData)
        {
            await Task.Run(() =>
            {
                try
                {
                    _Log4Net.Info($"产品体系同步,日期：{DateTime.Now}");
                    string url = string.Empty;
                    switch (jobData.DataType.ToString())
                    {
                        case "ProductTypeList":
                            url = "{1}/angli/GetProductTypeListByUpdateTime?UpdateTime={0}";
                            break;
                        case "ShiftList":
                            url = "{1}/angli/GetShiftListByUpdateTime?UpdateTime={0}";
                            break;
                        case "ClassList":
                            url = "{1}/angli/GetClassListByUpdateTime?UpdateTime={0}";
                            break;
                    }
                    foreach (var system in XgjInterfaceConfigureService.GetInterfaceConfigure(XGJInterfaceConfigureStatus.ProductEnable, XGJInterfaceConfigureCategory.XGJInSideInterface, jobData).Systems)
                    {
                        List<PushConfig> configList = new List<PushConfig>();
                        configList = system.Configs.ToList();
                        foreach (var config in configList)
                        {
                            T_EXT_SyncHistory _data = new T_EXT_SyncHistory();
                            _data.FromSystem = config.FromSystem;
                            _data.TeachLevelOneOrgID = Guid.Parse(config.Value);
                            _data.TeachLevelOneOrgName = config.Name;
                            _data.POCSource = config.POCSource;
                            _data.DataAPIPath = string.Format(url, jobData.QueryDate, ConfigVal.SchoolKeeperURL);
                            _Log4Net.Info($"同步开始,Url：{_data.DataAPIPath}");
                            try
                            {
                                bool invalid_access_token = true;//token是否失效   默认设置失效  重新获取 (ehs系统每次信息推送都会重新获取token 员工等信息更新，大型事业部过于频繁 所以每次重新获取token)
                                int count = 0;
                                while (invalid_access_token == true && count < 5)
                                {
                                    var _token = ConnectTool.GetToken(config, "api");
                                    _Log4Net.Info($"本次同步token：{_token}");
                                    _Log4Net.Info($"开始-请求校管家接口：{_data.DataAPIPath}");
                                    _data.DataJson = ConnectTool.HttpGet(_data.DataAPIPath, _token).Result;
                                    _Log4Net.Info($"结束-请求校管家接口：{_data.DataJson}");
                                    var _json = (JObject)JsonConvert.DeserializeObject(_data.DataJson);
                                    if (_json["ErrorCode"].ToString() == "200")
                                    {
                                        _data.SyncStatus = SynchroStatus.ManualSuccess;
                                        invalid_access_token = false;
                                    }
                                    else
                                    {
                                        _Log4Net.Info($"ErrorCode信息：{_json}");
                                        if (_json["ErrorCode"].ToString() == "401")
                                        {
                                            invalid_access_token = true;
                                        }
                                        if (count == 4)
                                        {
                                            throw new Exception(_json["ErrorMsg"].ToString());
                                        }
                                    }
                                    count++;
                                }

                                _Log4Net.Info("开始添加数据");
                                //IoC.Resolve<ITransform>(_data.DataType.ToString()).ExecuteTrans(_data);
                                switch (jobData.DataType.ToString())
                                {
                                    case "ProductTypeList":
                                        new PruductTypeTransform().ExecuteTrans(_data);
                                        break;
                                    case "ShiftList":
                                        new ShiftTransform().ExecuteTrans(_data);
                                        break;
                                    case "ClassList":
                                        new ClassTransform().ExecuteTrans(_data);
                                        break;
                                }

                                var _unit = UnitOfWorkResult.GetCurrentUow();
                                if (_unit.IsSuccess)
                                {
                                    _unit.Commit();
                                    _Log4Net.Info($"{_data.DataAPIPath}同步数据完成");
                                }
                                else
                                {
                                    _Log4Net.Info($"{_data.DataAPIPath}同步数据提交失败");

                                }
                            }
                            catch (Exception ex)
                            {
                                if (string.IsNullOrWhiteSpace(_data.DataJson))
                                {
                                    _data.DataJson = "";
                                }
                                _Log4Net.Error($"产品体系同步数据处理异常：{ex}");
                                _data.SyncStatus = SynchroStatus.ManualFailure;
                                _data.ErrorMessage = ex.ToString();
                                var _unit = UnitOfWorkResult.GetCurrentUow();
                                if (_unit.IsSuccess)
                                    _unit.Commit();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    _Log4Net.Error(ex.ToString());
                    throw ex;
                }
            });
            
        }

        #endregion

        #region 手动获取基础数据
        /// <summary>
        /// 手动获取基础数据
        /// </summary>
        /// <param name="jobData"></param>
        /// <returns></returns>
        [HttpPost]
        public BaseResponse GetBasicData(JobDataRequest jobData)
        {
            _Log4Net.Info($"手动获取基础数据同步,jobData：{JsonConvert.SerializeObject(jobData)}");
            GetBasicDataAsync(jobData);
            return ApiSuccessResult("完成");
        }

        private async Task GetBasicDataAsync(JobDataRequest jobData)
        {
            await Task.Run(() =>
            {
                try
                {
                    Dictionary<string, string> types = new Dictionary<string, string>  {
                    { "SHIFT_GRADE","所属年级" },
                    { "SHIFT_CATEGORY","所属类型" },
                    { "SHIFT_SUBJECT","所属科目" },
                    { "SHIFT_TERM","所属期段" },
                    { "SHIFT_TYPE","所属班型" }
                };
                    foreach (var system in XgjInterfaceConfigureService.GetInterfaceConfigure(XGJInterfaceConfigureStatus.BasicDataEnable, XGJInterfaceConfigureCategory.XGJInSideInterface, jobData).Systems)
                    {
                        List<PushConfig> configList = new List<PushConfig>();
                        configList = system.Configs.ToList();
                        foreach (var config in configList)
                        {
                            foreach (var type in types)
                            {
                                bool invalid_access_token = true;//token是否失效   默认设置失效  重新获取 (ehs系统每次信息推送都会重新获取token 员工等信息更新，大型事业部过于频繁 所以每次重新获取token)
                                int count = 0;
                                var dataJson = "";
                                while (invalid_access_token == true && count < 5)
                                {
                                    var _token = ConnectTool.GetBasicDataToken(config, "api");
                                    var apiPath = $"{ConfigVal.XGJBdURL}/base/get_dict?access_token={_token}&type={type.Key}";
                                    _Log4Net.Info($"本次同步token：{_token}");
                                    _Log4Net.Info($"开始-请求校管家接口：{apiPath}");
                                    dataJson = ConnectTool.HttpGet(apiPath, _token).Result;
                                    _Log4Net.Info($"结束-请求校管家接口：{dataJson}");
                                    var _json = JObject.Parse(dataJson);
                                    if (_json["errcode"].ToString() == "0")
                                    {
                                        invalid_access_token = false;
                                    }
                                    else
                                    {
                                        _Log4Net.Info($"errcode信息：{_json}");
                                        if (_json["errcode"].ToString() == "401")
                                        {
                                            invalid_access_token = true;
                                        }
                                        if (count == 4)
                                        {
                                            throw new Exception(_json["errmsg"].ToString());
                                        }
                                    }
                                    count++;
                                }

                                _Log4Net.Info("基础数据-开始添加数据");
                                basicDataDomainService.ExecuteTrans(config, dataJson, type.Key, type.Value);
                                var _unit = UnitOfWorkResult.GetCurrentUow();
                                if (_unit.IsSuccess)
                                {
                                    _unit.Commit();
                                    _Log4Net.Info($"基础数据-{type.Value.ToString()}同步数据完成");
                                }
                                else
                                {
                                    _Log4Net.Info($"基础数据-{type.Value.ToString()}同步数据提交失败");
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    _Log4Net.Error(ex.ToString());
                    throw ex;
                }
            });
        }

        #endregion

    }
}
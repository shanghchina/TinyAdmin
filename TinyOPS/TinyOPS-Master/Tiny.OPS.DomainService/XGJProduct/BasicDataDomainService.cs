using Newtonsoft.Json;
using Tiny.Common.Dapper.DI;
using Tiny.Common.Dapper.Service;
using Tiny.OPS.Common.Web.XGJTools;
using Tiny.OPS.Contract;
using Tiny.OPS.Contract.XGJ;
using Tiny.OPS.Domain.XGJProduct;
using Tiny.OPS.Repository;
using System;
using System.Collections.Generic;

namespace Tiny.OPS.DomainService
{
    public class BasicDataDomainService : RealDomainService<T_EXT_BasicData>, IBasicDataDomainService
    {
        private readonly IBasicDataRepository repository = IoC.Resolve<IBasicDataRepository>();

        public void ExecuteTrans(PushConfig config, string dataJson, string dicTypeCode, string dictTypeName)
        {
            var _responses = JsonConvert.DeserializeObject<ReturnBasicResponse<BasicResponse>>(dataJson);
            foreach (var response in _responses.Data)
            {
                var _result = repository.GetInfoByGuid(config.FromSystem, response.id.ToString());
                if (_result == null)
                {
                    _result = new T_EXT_BasicData();
                    _result.BasicDataGuid = Guid.NewGuid();
                }
                _result.FromSystem = config.FromSystem;
                _result.OneOrgId = config.ChargeLevelOneOrgId;
                _result.OneOrgName = config.ChargeLevelOneOrgName;
                _result.DictTypeCode = dicTypeCode;
                _result.DictTypeName = dictTypeName;
                _result.DictSort = 0;
                _result.DictId = response.id.ToString();
                _result.DictValue = response.value;
                _result.DictCreatetime = DateTime.Parse(DateTimeFormat(response.createtime));
                _result.DictUpdatetime = DateTime.Parse(DateTimeFormat(response.updatetime));
                _result.DictStatus = response.status;
                _result.DictDescribe = response.describe;
                _result.UpdaterUserId = "系统同步";
                _result.UpdaterUserName = "系统同步";
                _result.CreatedDate = DateTime.Now;
                _result.UpdateDate = DateTime.Now;
                _result.POCSource = config.POCSource;
                if (_result.Id == 0)
                    Add(_result);
                else
                    Save(_result);
            }
        }

        public BasicDataMapResponse GetBasicDataMapList(BasicDataMapRequest request)
        {
            int total = 0;
            BasicDataMapResponse response = new BasicDataMapResponse();
            response.DataList = repository.GetBasicDataMapList(request, out total);
            response.Total = total;
            return response;
        }

        private string DateTimeFormat(string time)
        {
            var str = time.Insert(4, "-");
            str = str.Insert(7, "-");
            str = str.Insert(10, " ");
            str = str.Insert(13, ":");
            str = str.Insert(16, ":");
            return str;
        }

        /// <summary>
        /// 根据字典Guid获取产品池的基础数据
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public List<T_EXT_BasicData> GetBasicDataByDictIdMap(List<int> pocSource, List<Guid> fkDictGuid)
        {
            return repository.GetBasicDataByDictIdMap(pocSource, fkDictGuid);
        }

        public BasicDataPageResponse GetBasicDataInfoPage(BasicDataPageRequest request)
        {
            var info = new BasicDataPageResponse();
            info.DataList = repository.GetBasicDataInfoPage(request, out int total);
            info.Total = total;
            return info;
        }/// <summary>
         ///  判断产品池的所有基础参数是否全部映射
         /// </summary>
         /// <returns></returns>
        public bool IsAllMapping()
        {
            return repository.IsAllMapping();

        }
    }
}

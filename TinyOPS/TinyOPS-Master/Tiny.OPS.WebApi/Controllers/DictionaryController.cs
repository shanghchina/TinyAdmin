using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Tiny.Common.Dapper.DI;
using Tiny.OPS.Common;
using Tiny.OPS.Contract;
using Tiny.OPS.DomainService;
using Tiny.OPS.WebApi.Controllers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Tiny.OPS.WebApi
{
    /// <summary>
    /// 数据字典
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DictionaryController : BaseController
    {
        private ISYS_DictionaryDomainService dictionaryDomainService => IoC.Resolve<ISYS_DictionaryDomainService>();
        /// <summary>
        /// 获取字典集合
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public BaseResponse GetDictionary(GetDictionaryRequest request)
        {
            try
            {
                GetDictionaryResponse response = dictionaryDomainService.GetDictionary(request);
                return ApiSuccessResult(response);
            }
            catch (Exception ex)
            {
                _Log4Net.Error("GetDictionary--异常信息", ex);
                return ApiErrorResult(ex.Message);
            }

        }

        /// <summary>
        /// 获取POC字典集合树，下拉分类用
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public BaseResponse GetPOCDictParams()
        {
            try
            {
                List<DictTreeResponse> response = dictionaryDomainService.GetPOCDictParams();
                return ApiSuccessResult(response);
            }
            catch (Exception ex)
            {
                _Log4Net.Error("GetPOCDictParams--异常信息", ex);
                return ApiErrorResult(ex.Message);
            }

        }

        /// <summary>
        /// 获取POC自定义参数大类，产品中心-基础参数维护页面用
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public BaseResponse GetPOCBasicParamsParentList()
        {
            try
            {
                GetDictionaryRequest request = new GetDictionaryRequest();//参数为空，获取5个基本参数
                GetVMDictionaryResponse response = dictionaryDomainService.GetPOCBasicParamsParentList(request);
                return ApiSuccessResult(response);
            }
            catch (Exception ex)
            {
                _Log4Net.Error("GetPOCDictParams--异常信息", ex);
                return ApiErrorResult(ex.Message);
            }

        }

        /// <summary>
        /// 获取POC系统维护-基础参数子节点列表 产品中心-基础参数维护页面用
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public BaseResponse GetPOCBasiceChildParamsList(GetVMDictionaryRequest request)
        {
            try
            {
                //GetDictionaryRequest request = new GetDictionaryRequest();//参数为空，获取5个基本参数
                Guid parentGuid = new Guid(request.ParentGuid);
                GetVMDictionaryResponse response = dictionaryDomainService.getPOCBasiceChildParamsList(parentGuid);
                return ApiSuccessResult(response);
            }
            catch (Exception ex)
            {
                _Log4Net.Error("getPOCBasiceChildParamsList--异常信息", ex);
                return ApiErrorResult(ex.Message);
            }
        }

        /// <summary>
        /// 系统维护-基础参数子节点-提交保存
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public BaseResponse SubmitChildParamsOption(SaveVMDictionaryRequest request)
        {
            try
            {
                var result = dictionaryDomainService.SaveOrUpdateChild(request);
                if (result.IsSuccess)
                { 
                    result?.Commit();
                    return ApiSuccessResult(true);
                }
                else
                {
                    return ApiFailResult("提交失败,请查看原因!");
                }

            }
            catch (Exception ex)
            {
                _Log4Net.Error("SubmitChildParamsOption--异常信息", ex);
                return ApiErrorResult(ex.Message);
            }
        }

        /// <summary>
        /// 系统维护-基础参数子节点-删除子节点判断是否已经映射
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public BaseResponse CheckHasMappedBasciData(GetVMDictionaryRequest request)
        {
            try
            {
                Guid dictGuid = new Guid(request.DictGuid);
                bool blResult = false;//能删除
                var response = dictionaryDomainService.CheckHasMappedBasciData(dictGuid);
                if(response)
                {
                    blResult = true;//不能删除
                    return ApiErrorResult("已存在映射关系的数据，不能删除！");
                }
                //校验提取器正在使用，不能删除 参数
                var responseExtractor = dictionaryDomainService.CheckIsExtractorBasciData(dictGuid);
                if (responseExtractor)
                {
                    blResult = true;//不能删除
                    return ApiErrorResult("校验提取器正在使用，不能删除！");
                }
                return ApiSuccessResult(blResult);
            }
            catch (Exception ex)
            {
                _Log4Net.Error("CheckHasMappedBasciData--异常信息", ex);
                return ApiErrorResult(ex.Message);
            }
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Tiny.Common.Dapper.DI;
using Tiny.OPS.Common;
using Tiny.OPS.Contract;
using Tiny.OPS.Domain.XGJProduct;
using Tiny.OPS.DomainService;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Tiny.OPS.WebApi.Controllers
{
    /// <summary>
    /// 基础参数
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Description("基础参数")]
    public class BasicDataController : BaseController
    {
        private IBasicDataDomainService basicDataDomainService => IoC.Resolve<IBasicDataDomainService>();
        private IT_POC_BasicDataMapDomainService basicDataMapDomainService => IoC.Resolve<IT_POC_BasicDataMapDomainService>();
        /// <summary>
        /// 基础参数映射列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public BaseResponse GetBasicDataMapList(BasicDataMapRequest request)
        {
            try
            {
                BasicDataMapResponse response = basicDataDomainService.GetBasicDataMapList(request);
                return ApiSuccessResult(response);
            }
            catch (Exception ex)
            {
                return ApiErrorResult(ex.Message);
            }
        }

        /// <summary>
        /// 基础参数映射操作
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public BaseResponse OperateBasicDataMap(OperateBasicDataMapRequest request)
        {
            try
            {
                basicDataMapDomainService.OperateBasicDataMap(request);
                return ApiSuccessResult("");
            }
            catch (Exception ex)
            {
                return ApiErrorResult(ex.Message);
            }

        }

        /// <summary>
        /// 根据字典Guid获取产品池的基础数据
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public BaseResponse GetBasicDataByDictIdMap(GetBasicDataByDictIdMapRequest request)
        {
            try
            {
                List<T_EXT_BasicData> list = basicDataDomainService.GetBasicDataByDictIdMap(request.pocSource, request.fkDictGuid);
                return ApiSuccessResult(list);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// 获取基础参数分页列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public BaseResponse GetBasicDataInfoPage(BasicDataPageRequest request)
        {
            try
            {
                var response = basicDataDomainService.GetBasicDataInfoPage(request);
                return ApiSuccessResult(response);
            }
            catch (Exception ex)
            {
                _Log4Net.Error("GetEXTCourseByPage--异常信息", ex);
                return ApiErrorResult(ex.Message);
            }
        }

        /// <summary>
        /// 获取基础参数分页列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult exportExcle(BasicDataPageRequest request)
        {
            try
            {
                var response = basicDataDomainService.GetBasicDataInfoPage(request);
                var stream = ExcelHelper.SaveExcel(response.DataList);
                return File(stream, "application/vnd.ms-excel", "基础数据.xlsx");
            }
            catch (Exception ex)
            {
                _Log4Net.Error("GetEXTCourseByPage--异常信息", ex);
                return null;
            }
        }

        /// <summary>
        /// 判断来源系统所有参数类型是否映射
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public BaseResponse IsAllMapping()
        {
            try
            {
                bool istrue = basicDataDomainService.IsAllMapping();
                return ApiSuccessResult(istrue);
            }
            catch (Exception ex)
            {
                return ApiErrorResult(ex.Message);
            }
        }
    }
}
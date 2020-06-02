using Microsoft.AspNetCore.Mvc;
using Tiny.Common.Dapper.DI;
using Tiny.OPS.Common;
using Tiny.OPS.Contract;
using Tiny.OPS.DomainService;
using System;

namespace Tiny.OPS.WebApi.Controllers
{
    /// <summary>
    /// 产品中心--产品信息表
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class POCProductController : BaseController
    {
        private IT_POC_ProductDomainService PocProductDomainService => IoC.Resolve<IT_POC_ProductDomainService>();

        /// <summary>
        /// 获取分页产品信息列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public BaseResponse GetPOCProductByPage(POCProductRequest request)
        {
            try
            {
                POCProductPageInfoResponse response = PocProductDomainService.GetPOCProductByPage(request);
                return ApiSuccessResult(response);
                //VMPOCProductPageInfoResponse response = PocProductDomainService.GetVMPOCProductByPage(request);
                //return ApiSuccessResult(response);
            }
            catch (Exception ex)
            {
                _Log4Net.Error("GetPOCProductByPage--异常信息", ex);
                return ApiErrorResult(ex.Message);
            }
        }

        /// <summary>
        /// 获取来源系统产品体系--课程信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public BaseResponse GetEXTCourseByPage(POCEXTCourseRequest request)
        {
            try
            {
                VMEXTCoursePageInfoResponse response = PocProductDomainService.GetVMEXTCourseByPage(request);
                return ApiSuccessResult(response);
            }
            catch (Exception ex)
            {
                _Log4Net.Error("GetEXTCourseByPage--异常信息", ex);
                return ApiErrorResult(ex.Message);
            }
        }
    }
}
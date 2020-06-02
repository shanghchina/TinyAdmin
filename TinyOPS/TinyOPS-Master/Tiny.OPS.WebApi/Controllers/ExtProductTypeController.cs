using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tiny.Common.Dapper.DI;
using Tiny.OPS.Common;
using Tiny.OPS.Contract;
using Tiny.OPS.DomainService;
using Tiny.OPS.WebApi.Filter;

namespace Tiny.OPS.WebApi.Controllers
{
    /// <summary>
    /// 校管家产品类别
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Description("校管家产品类别")]
    public class ExtProductTypeController : BaseController
    {
        private IProductItemTypeDomainService productItemTypeDomainService => IoC.Resolve<IProductItemTypeDomainService>();
        /// <summary>
        /// 校管家产品类别列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [NoCheck]
        public BaseResponse GetProductItemTypeList(ExtItemTypeRequest request)
        {
            try
            {
                ExtItemTypeResponse response = productItemTypeDomainService.GetProductItemTypeList(request);
                return ApiSuccessResult(response);
            }
            catch (Exception ex)
            {
                return ApiErrorResult(ex.Message);
            }
        }
        /// <summary>
        /// 导出校管家产品类别列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult ExportExcel(ExtItemTypeRequest request)
        {
            try
            {
                ExtItemTypeResponse response = productItemTypeDomainService.GetProductItemTypeList(request);
                var stream = ExcelHelper.SaveExcel(response.DataList);
                return File(stream, "application/vnd.ms-excel", "产品分类.xlsx");
            }
            catch (Exception ex)
            {
                _Log4Net.Error("ExportExcel--异常信息", ex);
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
                bool istrue = productItemTypeDomainService.IsAllMapping();
                return ApiSuccessResult(istrue);
            }
            catch (Exception ex)
            {
                return ApiErrorResult(ex.Message);
            }
        }



    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using Tiny.OPS.Common;
using Microsoft.AspNetCore.Mvc;
using Tiny.Common.Dapper.DI;
using Tiny.OPS.Common;
using Tiny.OPS.Contract;
using Tiny.OPS.DomainService;

namespace Tiny.OPS.WebApi.Controllers
{
    /// <summary>
    /// 事业部产品池--班级信息表
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EXTClassController : BaseController
    {
        private IProductClassDomainService productClassDomainService => IoC.Resolve<IProductClassDomainService>();

        /// <summary>
        /// 获取分页产品信息列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public BaseResponse GetEXTClassByPage(EXTClassRequest request)
        {
            try
            {
                VMEXTClassPageInfoResponse response = productClassDomainService.GetEXTClassByPage(request);
                return ApiSuccessResult(response);
                //VMEXTClassPageInfoResponse response = EXTClassDomainService.GetVMEXTClassByPage(request);
                //return ApiSuccessResult(response);
            }
            catch (Exception ex)
            {
                _Log4Net.Error("GetEXTClassByPage--异常信息", ex);
                return ApiErrorResult(ex.Message);
            }
        }

        /// <summary>
        /// 导出班级信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult ExportExcle(EXTClassRequest request)
        {
            try
            {
                VMEXTClassPageInfoResponse response = productClassDomainService.GetEXTClassByPage(request);

                var stream = ExcelHelper.SaveExcel(response.ReusltList.ToList());
                return File(stream, "application/vnd.ms-excel", "班级信息.xlsx");
            }
            catch (Exception ex)
            {
                _Log4Net.Error("ExportExcle--异常信息", ex);
                return null;
            }
        }

    }
}
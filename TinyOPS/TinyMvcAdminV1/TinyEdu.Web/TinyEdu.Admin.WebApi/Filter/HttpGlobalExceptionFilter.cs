using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using TinyEdu.Admin.Contract;
using TinyEdu.Admin.Domain;
using TinyEdu.Admin.DomainService;
using TinyEdu.Common.Dapper.DI;
using TinyEdu.Util;

namespace TinyEdu.Admin.WebApi
{
    /// <summary>
    /// 捕获全局异常信息
    /// </summary>
    public class HttpGlobalExceptionFilter : ExceptionFilterAttribute
    {
        private IAPP_SysLogDomainService app_SysLogDomainService => IoC.Resolve<IAPP_SysLogDomainService>();

        /// <summary>
        /// 异常处理
        /// </summary>
        /// <param name="context"></param>
        public override void OnException(ExceptionContext context)
        {
            try
            {
                string msg = context.Exception.Message;
                if (string.IsNullOrEmpty(msg)) msg = "发生未知异常";
                BaseResponseModel<string> response = new BaseResponseModel<string> { Code = CodeConst.SystemException, Message = msg };
                context.HttpContext.Response.StatusCode = Convert.ToInt32(HttpStatusCode.InternalServerError);
                context.Result = new JsonResult(response);
                base.OnException(context);
                _Log4Net.InfoFormat("OnlyEdu.POC.WebApi记录全局异常OnException:{0}", context.Exception);
                app_SysLogDomainService.AddLogError("OnlyEdu.POC.WebApi记录全局异常OnException", context.Exception);
            }
            catch (Exception ex)
            {
                _Log4Net.Error("OnlyEdu.POC.WebApi全局异常处理发生异常:" + ex.Message.ToString());
                app_SysLogDomainService.AddLogError("OnlyEdu.POC.WebApi记录全局异常OnException发生错误", ex);
            }
        }
    }
}

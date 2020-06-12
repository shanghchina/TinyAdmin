using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Tiny.Common.Dapper.DI;
using Tiny.OPS.Common;
using Tiny.OPS.Contract;
using Tiny.OPS.Domain;
using Tiny.OPS.DomainService;

namespace Tiny.Ops.Mvc.Filter
{
    /// <summary>
    /// 全局异常处理
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
                _Log4Net.InfoFormat("Tiny.OPS.WebApi记录全局异常OnException:{0}", context.Exception);
                app_SysLogDomainService.AddLogError("Tiny.OPS.WebA记录全局异常OnException", context.Exception);
            }
            catch (Exception ex)
            {
                _Log4Net.Error("Tiny.OPS.WebApi全局异常处理发生异常:" + ex.Message.ToString());
                app_SysLogDomainService.AddLogError("Tiny.OPS.Web 记录全局异常OnException发生错误", ex);
            }
        }
    }
}

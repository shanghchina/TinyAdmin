using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using Tiny.OPS.Contract;
using Tiny.OPS.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tiny.OPS.WebApi.Filter
{
    /// <summary>
    /// 模型校验过略器
    /// </summary>
    public class ModelValidationFilterAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// 模型验证
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            try
            {
                string requestParam = string.Empty;
                IDictionary<string, object> actionArguments = context.ActionArguments;
                object request = null;
                if (!actionArguments.TryGetValue("request", out request)) request = actionArguments.FirstOrDefault().Value;
                requestParam = JsonConvert.SerializeObject(request);
                if (!context.ModelState.IsValid)
                {
                    string errMsg = context.ModelState.Values.Where(p => p.Errors.Count > 0).FirstOrDefault().Errors[0].ErrorMessage;
                    if (string.IsNullOrEmpty(errMsg)) errMsg = "参数有误！";
                    BaseResponseModel<string> response = new BaseResponseModel<string> { Code = CodeConst.ParamterError, Message = errMsg };
                    context.Result = new JsonResult(response);
                }
                base.OnActionExecuting(context);
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}

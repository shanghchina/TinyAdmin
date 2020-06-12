using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Tiny.OPS.Common;
using Tiny.OPS.Common.Web;
using Tiny.OPS.Contract;

namespace Tiny.Ops.Mvc.Controllers
{
    public class BaseController : Controller
    {
        protected ActionResult AjaxPostCorssDomain(string url, string json)
        {
            try
            {
                var _result = PostHelper.GetPostResult(url, json);
                return JsonHelper.LargeJson(_result);
            }
            catch (Exception ex)
            {
                //用log4net记录日志
                _Log4Net.Error("AjaxPostCorssDomain." + url + "." + json, ex);

                // Logger.Default.Error("AjaxPostCorssDomain异常：{0}", ex);
                return Json(JsonConvert.SerializeObject(new ResultParam
                {
                    IsSuccess = false,
                    AlertMessage = JsonConvert.SerializeObject(ex),
                }));
            }
        }
    }
}
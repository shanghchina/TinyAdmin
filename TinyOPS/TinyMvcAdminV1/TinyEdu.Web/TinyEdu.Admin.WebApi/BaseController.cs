using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TinyEdu.Admin.WebApi
{
    /// <summary>
    /// 
    /// </summary>
    public class BaseController : ControllerBase
    {
        //public ActionResult AjaxPostCorssDomain(string url, string json)
        //{
        //    //try
        //    //{
        //    //    var _result = PostHelper.GetPostResult(url, json);
        //    //    return JsonHelper.LargeJson(_result);
        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    //用log4net记录日志
        //    //    _Log4Net.Error("AjaxPostCorssDomain." + url + "." + json, ex);

        //    //    Logger.Default.Error("AjaxPostCorssDomain异常：{0}", ex);
        //    //    return Json(JsonConvert.SerializeObject(new ResultParam
        //    //    {
        //    //        IsSuccess = false,
        //    //        AlertMessage = JsonConvert.SerializeObject(ex),
        //    //    }));

        //    //}
        //}
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TinyEdu.Model;
using TinyEdu.Util;
using TinyEdu.Web.Util;

namespace TinyEdu.Admin.Web.Controllers
{
    public class ApiBaseController : Controller
    {
        /// <summary>
        /// 前后端调用函数
        /// </summary>
        /// <param name = "url" ></ param >
        /// < param name="json"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult AjaxPostCorssDomain([FromBody]AjaxPostParam postParam)
        {
            string url = postParam.url; string json = postParam.json;
            try
            {
                var _result = PostHelper.GetPostResult(url, json);
                //var json_result = Json(_result);
                //return json_result;
                return ApiJsonHelper.LargeJson(_result);
            }
            catch (Exception ex)
            {
                //用log4net记录日志
                _Log4Net.Error("AjaxPostCorssDomain." + url + "." + json, ex);

                //Logger.Default.Error("AjaxPostCorssDomain异常：{0}", ex);
                return Json(JsonConvert.SerializeObject(new ResultParam
                {
                    IsSuccess = false,
                    AlertMessage = JsonConvert.SerializeObject(ex),
                }));

            }
        }

        //[HttpPost]
        //public ActionResult AjaxPostCorssDomain(string url, string json)
        //{
        //    // string url = ""; string json = "";
        //    try
        //    {
        //        var _result = PostHelper.GetPostResult(url, json);
        //        return ApiJsonHelper.LargeJson(_result);
        //    }
        //    catch (Exception ex)
        //    {
        //        //用log4net记录日志
        //        _Log4Net.Error("AjaxPostCorssDomain." + url + "." + json, ex);

        //        //Logger.Default.Error("AjaxPostCorssDomain异常：{0}", ex);
        //        return Json(JsonConvert.SerializeObject(new ResultParam
        //        {
        //            IsSuccess = false,
        //            AlertMessage = JsonConvert.SerializeObject(ex),
        //        }));

        //    }
        //}



        //[HttpGet]
        //public ActionResult AjaxGetCorssDomain(string json)
        //{
        //    string url = "";
        //    try
        //    {
        //        var _result = PostHelper.GetPostResult(url, json);
        //        return ApiJsonHelper.LargeJson(_result);
        //    }
        //    catch (Exception ex)
        //    {
        //        _Log4Net.Error("AjaxGetCorssDomain." + url + "." + json, ex);

        //        return Json(JsonConvert.SerializeObject(new ResultParam
        //        {
        //            IsSuccess = false,
        //            AlertMessage = JsonConvert.SerializeObject(ex),
        //        }));
        //    }
        //}


    }

    /// <summary>
    /// 前端请求到后端的通用参数
    /// </summary>
    public class AjaxPostParam
    {
        public string url { get; set; }
        public string json { get; set; }

    }
}
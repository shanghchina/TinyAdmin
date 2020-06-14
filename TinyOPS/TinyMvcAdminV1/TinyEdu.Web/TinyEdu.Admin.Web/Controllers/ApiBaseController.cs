using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using TinyEdu.Model;
using TinyEdu.Util;
using TinyEdu.Web.Util;

namespace TinyEdu.Admin.Web.Controllers
{
    public class ApiBaseController : Controller
    {
        //环境变量
        public ApiAuthorize apiAuthorize;

        /// <summary>
        /// 读取配置值  string redisPath = Configuration["RedisPath"];
        /// </summary>
        /// <param name="hostingEnvironment"></param>
        /// <param name="configuration"></param>
        public ApiBaseController(IOptions<ApiAuthorize> option)
        {
            apiAuthorize = option.Value;
        }

        /// <summary>
        /// 获取加密字符令牌
        /// </summary>
        /// <param name="ajaxAppId">前端传过来的appid</param>
        /// <returns></returns>
        [HttpPost]
        public string AjaxGetEncryptStr([FromBody]AjaxAppParam ajaxApp)
        {
            string result = "";
            string ajaxAppId = ajaxApp.ajaxAppId;
            try
            {
                string appId = apiAuthorize.Appid;
                string appSecret = apiAuthorize.AppSecret;
                if (ajaxAppId.ToUpper() == appId.ToUpper())
                {
                    var key = appId + appSecret;
                    string md5str = HttpContextUtils.Md5Encrypt(key).ToUpper();
                    _Log4Net.Info(string.Format("api获取的签名参数:{0}，加密后{1}------>", key, md5str));

                    result = md5str;
                }
                else
                {
                    result = "appid不合法";
                    _Log4Net.Info("appid不合法");
                }
            }
            catch (Exception ex)
            {
                //用log4net记录日志
                _Log4Net.Error("AjaxGetEncryptStr", ex);
                result = "校验没通过";
            }
            return result;
        }

        /// <summary>
        /// 前后端调用函数
        /// </summary>
        /// <param name = "url" ></ param >
        /// < param name="json"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AjaxPostCorssDomain([FromBody]AjaxPostParam postParam)
        {
            string url = postParam.url; string json = postParam.json;
            try
            {
                var _result = PostHelper.GetPostResult(url, json);
                var json_result = Json(_result);
                return json_result;
                //var result = ApiJsonHelper.LargeJson(_result);
                //return result;
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

    }

    /// <summary>
    /// 前端请求到后端的通用参数
    /// </summary>
    public class AjaxPostParam
    {
        public string url { get; set; }
        public string json { get; set; }

    }

    /// <summary>
    /// ajaxApp请求参数
    /// </summary>
    public class AjaxAppParam
    {
        public string ajaxAppId { get; set; }
    }
}
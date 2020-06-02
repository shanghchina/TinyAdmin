using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;

namespace OnlyEdu.Common.Web.Authentication.Api
{
    public class OnlyApiAuthenticationMiddleware
    {
        private readonly OnlyApiAuthenticationOptions _options;

        private readonly RequestDelegate _next;

        public OnlyApiAuthenticationMiddleware(RequestDelegate next, IOptions<OnlyApiAuthenticationOptions> options)
        {
            this._next = next;
            this._options = options.Value;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (NeedSso(context))
            {
                await Check(context);
            }
            else
            {
                await _next.Invoke(context);
            }

        }



        #region SSO
        /// <summary>
        /// 登录认证
        /// </summary>
        /// <param name="context"></param>
        /// <param name="requestInfo"></param>
        /// <returns></returns>
        private async Task Check(HttpContext context)
        {
            StringValues token=new StringValues();
            StringValues userid = new StringValues();
            context.Request.Headers.TryGetValue("Only-Token", out token);
            context.Request.Headers.TryGetValue("Only-UserEmail", out userid);
            using (HttpClient httpClient = new HttpClient())
            {
                var content = JsonConvert.SerializeObject(new {loginId = token.ToString(), email = userid.ToString()});
                var validateresponsestr=HttpUtils.PostHttps($"{OnlyAuthenticationConfig.APIConfig.TokenVerify}", content);
                if (!string.IsNullOrEmpty(validateresponsestr))
                {
                    BaseApiResult validateresponse = JsonConvert.DeserializeObject<BaseApiResult>(validateresponsestr);
                    if (validateresponse.Code!="0")
                    {
                        await ReturnResponse(context, (int)HttpStatusCode.Unauthorized, $"未认证通过:{validateresponse.Message}");
                    }
                    else
                    {
                        await _next.Invoke(context);
                    }
                }
                else
                {
                    await ReturnResponse(context, (int) HttpStatusCode.Unauthorized, "未认证通过");
                }
                
            }
            



        }
        /// <summary>
        ///响应 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private async Task ReturnResponse(HttpContext context, int statu = 200, string msg = "Time Out!")
        {
            context.Response.StatusCode = statu;
            context.Response.ContentType = "text/plain; charset=utf-8";
            await context.Response.WriteAsync(JsonConvert.SerializeObject(new { Code = statu, Message = msg }));
        }
        /// <summary>
        /// 签名超时
        /// </summary>
        /// <param name="timestamp"></param>
        /// <param name="expiredSecond"></param>
        /// <returns></returns>
        private bool CheckExpiredTime(double timestamp, double expiredSecond)
        {
            double now_timestamp = (DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;
            return (now_timestamp - timestamp) > expiredSecond;
        }

        /// <summary>
        /// 判断是否需要身份认证
        /// </summary>
        /// <returns></returns>
        private bool NeedSso(HttpContext context)
        {
            //是否要求登陆
            bool result = true;
            string requestPath = context.Request.Path;
            if (requestPath != "/")
            {

                var rejects = OnlyAuthenticationConfig.APIConfig.ApiRejects;
                if (rejects.Length > 0)
                {

                    //if (requestPath != "/")
                    //{
                    //    throw new Exception(requestPath);
                    //}
                    result =
                        !(from ssoReject in rejects
                            where requestPath.StartsWith(ssoReject, StringComparison.CurrentCultureIgnoreCase)
                            select ssoReject).Any();
                }
            }
            else
            {
                result = false;
            }
            return result;
            return true;
        }


        #endregion
    }
}

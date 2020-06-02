using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace OnlyEdu.Common.Web.Authentication.Web
{
    public class OnlyAuthenticationMiddleware
    {
        private readonly OnlyAuthenticationOptions _options;

        private readonly RequestDelegate _next;

        public OnlyAuthenticationMiddleware(RequestDelegate next, IOptions<OnlyAuthenticationOptions> options)
        {
            this._next = next;
            this._options = options.Value;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            await Check(context);

        }



        #region SSO
        /// <summary>
        /// the main check method
        /// </summary>
        /// <param name="context"></param>
        /// <param name="requestInfo"></param>
        /// <returns></returns>
        private async Task Check(HttpContext context)
        {
            string computeSinature = _options.SignKey;
            double tmpTimestamp;
            if (!string.IsNullOrEmpty(context.Request.Query["LoginId"]))// &&double.TryParse(context.Request.Query["timestamp"], out tmpTimestamp),computeSinature.Equals(context.Request.Query["LoginId"])
            {
                //if (CheckExpiredTime(tmpTimestamp, _options.ExpiredSecond))
                //{

                //}
                //else
                //{
                //    await ReturnResponse(context, 200, "成功验证");
                //}
                //await ReturnResponse(context, 200, "Success");
                //context.Response.Redirect(_options.DefaultURL);
                //context.Session.Set("LoginId", context.Request.Query["LoginId"].ToString());
                //context.Session.SetString("LoginId", context.Request.Query["LoginId"].ToString());
                await _next.Invoke(context);
            }
            else
            {
                //await ReturnResponse(context);
                //await ReturnResponse(context, 408, "登录失败，签名错误");
                context.Response.Redirect(_options.LoginURL + $"?ReturnUrl={_options.DefaultURL}&Realm={_options.Realm}");
                //
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
        private bool NeedSso()
        {
            return true;
        }


        #endregion
    }
}

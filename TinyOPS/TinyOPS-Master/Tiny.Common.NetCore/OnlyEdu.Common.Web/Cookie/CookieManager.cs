using Microsoft.AspNetCore.Http;
using System;

namespace OnlyEdu.Common.Web.Cookie
{
    /// <summary>
    /// Cookie实现类
    /// </summary>
    public class CookieManager
    {
        private static IHttpContextAccessor ContextAccessor;

        #region 读取cookies
        /// <summary>
        /// 读取cookies
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetCookies(string key)
        {
            ContextAccessor.HttpContext.Request.Cookies.TryGetValue(key, out string value);
            if (string.IsNullOrEmpty(value))
                value = string.Empty;
            return value;
        }
        #endregion 读取cookies

        #region 删除cookies
        /// <summary>
        /// 删除cookies
        /// </summary>
        /// <param name="key"></param>
        public static void RemoveCookies(string key)
        {
            ContextAccessor.HttpContext.Response.Cookies.Delete(key);
        }
        #endregion 删除cookies

        #region 设置cookies
        /// <summary>
        /// 设置cookies
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="minutes">过期时长，单位：分钟</param>
        public static void SetCookie(string key, string value, int minutes = 30)
        {
            ContextAccessor.HttpContext.Response.Cookies.Append(key, value, new CookieOptions
            {
                Expires = DateTime.Now.AddMinutes(minutes)
            });
        }
        #endregion 设置cookies
    }
}

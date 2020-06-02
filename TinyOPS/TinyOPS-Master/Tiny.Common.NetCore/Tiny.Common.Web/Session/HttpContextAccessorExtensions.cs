using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Tiny.Common.Web.Session
{
    public static class HttpContextAccessorExtensions
    {
        public static string GetStringByKey(this IHttpContextAccessor accessor, string key)
        {
            var dic = accessor.HttpContext.Request.Headers;

            if (dic.ContainsKey(key))
            {
                return dic[key];
            }

            return null;
        }
    }
}

using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tiny.OPS.WebApi
{
    /// <summary>
    /// 方法一：先引用“using Microsoft.AspNetCore.Http.Extensions;”，然后直接用“Request.GetDisplayUrl();”
    /// 方法二：后来参考 Microsoft.AspNetCore.Rewrite 的源代码，写了一个扩展方法实现了。
    /// </summary>
    public static class HttpRequestExtensions
    {
        /// <summary>
        /// 扩展方法获取uri
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static string GetAbsoluteUri(this HttpRequest request)
        {
            return new StringBuilder()
                .Append(request.Scheme)
                .Append("://")
                .Append(request.Host)
                .Append(request.PathBase)
                .Append(request.Path)
                .Append(request.QueryString)
                .ToString();
        }

        /// <summary>
        /// ASP.NET Core Web APi获取原始请求内容
        /// https://www.cnblogs.com/CreateMyself/archive/2018/02/05/8410686.html
        /// </summary>
        /// <param name="request"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static async Task<string> GetRawBodyStringAsync(this HttpRequest request, Encoding encoding = null)
        {
            if (encoding is null)
                encoding = Encoding.UTF8;

            using (var reader = new StreamReader(request.Body, encoding))
                return await reader.ReadToEndAsync();
        }

        public static async Task<byte[]> GetRawBodyBytesAsync(this HttpRequest request)
        {
            using (var ms = new MemoryStream(2048))
            {
                await request.Body.CopyToAsync(ms);
                return ms.ToArray();
            }
        }


        public static async Task<string> GetResponseRawBodyStringAsync(this HttpResponse response, Encoding encoding = null)
        {
            if (encoding is null)
                encoding = Encoding.UTF8;

            using (var reader = new StreamReader(response.Body, encoding))
                return await reader.ReadToEndAsync();
        }

        public static async Task<byte[]> GetResponseRawBodyBytesAsync(this HttpResponse response)
        {
            using (var ms = new MemoryStream(2048))
            {
                await response.Body.CopyToAsync(ms);
                return ms.ToArray();
            }
        }
    }
}

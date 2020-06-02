using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Tiny.OPS.Common.Web
{
    public static class HttpContextUtils
    {
        /// <summary>
        /// 获取IP地址
        /// </summary>
        public static string GetClientIP(HttpContext context)
        {
            string _result = string.Empty;
            _result = context.Connection.RemoteIpAddress?.ToString();
            if (string.IsNullOrEmpty(_result) || !IsIP(_result))
                return "127.0.0.1";
            return _result;
        }

        /// <summary>
        /// 正则判断
        /// </summary>
        private static bool IsIP(string ip)
        {
            return Regex.IsMatch(ip, "^((2[0-4]\\d|25[0-5]|[01]?\\d\\d?)\\.){3}(2[0-4]\\d|25[0-5]|[01]?\\d\\d?)$");
        }

        /// <summary>
        /// md5 32加密
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Md5Encrypt(string str)
        {
            var cl = str;
            var md5 = MD5.Create();//实例化一个md5对像
            // 加密后是一个字节类型的数组，这里要注意编码UTF8/Unicode等的选择　
            var s = md5.ComputeHash(Encoding.UTF8.GetBytes(cl));
            // 通过使用循环，将字节类型的数组转换为字符串，此字符串是常规字符格式化所得
            return s.Aggregate("", (current, t) => current + t.ToString("x2"));
        }
    }
}

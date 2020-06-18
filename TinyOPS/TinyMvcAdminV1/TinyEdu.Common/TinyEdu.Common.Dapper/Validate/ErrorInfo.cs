using System;
using System.Collections.Generic;
using System.Text;

namespace TinyEdu.Common.Dapper.Validate
{
    /// <summary>
    /// 验证错误类
    /// </summary>
    public class ErrorInfo
    {
        /// <summary>
        /// 错误关键字
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 无参数
        /// </summary>
        public ErrorInfo()
        {
        }
        /// <summary>
        /// key和message
        /// </summary>
        /// <param name="key"></param>
        /// <param name="message"></param>
        public ErrorInfo(string key, string message)
        {
            Key = key;
            Message = message;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Tiny.OPS.Contract
{
    /// <summary>
    /// token请求体
    /// </summary>

    public class TokenRequest
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string Username { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 交易流水，yyyyMMddHHmmssfff+4位随机数
        /// </summary>
        public string TransactionID { get; set; }
        /// <summary>
        /// 请求时间yyyyMMddHHmmss
        /// </summary>
        public string RequestDate { get; set; }
    }
}

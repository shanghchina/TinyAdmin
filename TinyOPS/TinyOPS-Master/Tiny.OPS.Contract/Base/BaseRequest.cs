using System;
using System.Collections.Generic;
using System.Text;

namespace Tiny.OPS.Contract
{
    /// <summary>
    /// BaseRequest
    /// </summary>
    public class BaseRequest
    {
        /// <summary>
        /// 申请
        /// </summary>
        public string appid { get; set; }
        /// <summary>
        /// MD5签名
        /// </summary>
        public string sign { get; set; }
    }
}

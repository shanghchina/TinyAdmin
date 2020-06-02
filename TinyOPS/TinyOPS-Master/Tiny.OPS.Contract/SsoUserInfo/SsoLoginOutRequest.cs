using System;
using System.Collections.Generic;
using System.Text;

namespace Tiny.OPS.Contract
{
    /// <summary>
    /// 退出登录请求信息
    /// </summary>
   public class SsoLoginOutRequest
    {
        /// <summary>
        /// LoginId
        /// </summary>
        public string LoginId { get; set; }
    }
}

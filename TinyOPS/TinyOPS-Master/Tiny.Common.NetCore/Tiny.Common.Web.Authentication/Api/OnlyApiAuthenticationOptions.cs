using System;
using System.Collections.Generic;
using System.Text;

namespace Tiny.Common.Web.Authentication.Api
{
    public class OnlyApiAuthenticationOptions
    {
        /// <summary>
        /// Token获取地址
        /// </summary>
        public string TokenUrl { get; set; }
        /// <summary>
        /// Token验证地址
        /// </summary>
        public string TokenVerify { get; set; }

        /// <summary>
        /// Tokens验证频率(间隔/s);0为总是验证;
        /// </summary>
        public string TokenSpeed { get; set; }
        /// <summary>
        /// Tokens刷新址
        /// </summary>
        public string TokenRefresh { get; set; }
    }
}

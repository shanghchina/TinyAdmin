using System;
using System.Collections.Generic;
using System.Text;

namespace OnlyEdu.Common.Web.Authentication.Web
{
    public class OnlyAuthenticationOptions
    {
        /// <summary>
        /// 依赖方
        /// </summary>
        public string RelyingName { get; set; }
        /// <summary>
        /// 默认首页地址
        /// </summary>
        public string DefaultURL { get; set; }
        
        /// <summary>
        /// 单点登录地址
        /// </summary>
        public string LoginURL { get; set; }
        /// <summary>
        /// 等同依赖方
        /// </summary>
        public string Realm { get; set; }
        
        /// <summary>
        /// 获取Token地址
        /// </summary>
        public string TokenURL { get; set; }
        /// <summary>
        /// 加密秘钥
        /// </summary>
        public string SignKey { get; set; }
        /// <summary>
        /// AES-KEY
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// AES-IV
        /// </summary>
        public string IV { get; set; }

        /// <summary>
        /// 超时时间
        /// </summary>
        public int ExpiredSecond { get; set; }

        /// <summary>
        /// 无需验证的地址
        /// </summary>
        public  string ExcludePath { get; set; }
    }
}

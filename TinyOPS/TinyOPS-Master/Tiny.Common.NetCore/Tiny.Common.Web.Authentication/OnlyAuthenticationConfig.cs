using System;
using System.Collections.Generic;
using System.Text;

namespace Tiny.Common.Web.Authentication
{

    public class OnlyAuthenticationConfig
    {
        /// <summary>
        /// 完整配置
        /// </summary>
        public static OnlyAuthentication OnlyAuthConfig
        {
            get { return AppConfig.GetSection<OnlyAuthentication>("OnlyAuthentication"); }
        }
        /// <summary>
        /// MVC配置
        /// </summary>
        public static SSOConfiguration SSOConfig
        {
            get { return AppConfig.GetSection<OnlyAuthentication>("OnlyAuthentication").SSOConfig; }
        }

        /// <summary>
        /// API配置
        /// </summary>
        public static APIConfiguration APIConfig
        {
            get { return AppConfig.GetSection<OnlyAuthentication>("OnlyAuthentication").APIConfig; }
        }

    }
    public class OnlyAuthentication
    {
        public SSOConfiguration SSOConfig { get; set; }
        public APIConfiguration APIConfig { get; set; }

    }
    #region SSO

    public class SSOConfiguration
    {

        public bool State { get; set; }

        public string RelyingName { get; set; }

        public string DomainName { get; set; }


        public string DefaultURL { get; set; }

        public string Realm { get; set; }

        public string LoginURL{ get; set; }

        public string TokenURL{ get; set; }

        public string Signing { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public AESConfiguration AES { get; set; }

        public string[] SsoRejects { get; set; }
    }

    public class AESConfiguration
    {

        public string Key { get; set; }


        public string IV { get; set; }
    }
    #endregion


    public class APIConfiguration
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
        public int TokenSpeed { get; set; }
        /// <summary>
        /// Tokens刷新址
        /// </summary>
        public string TokenRefresh { get; set; }

        /// <summary>
        /// 无需验证的api地址
        /// </summary>
        public string[] ApiRejects { get; set; }

    }

}

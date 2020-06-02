using System;
using System.Collections.Generic;
using System.Text;

namespace Tiny.OPS.Common
{
    /// <summary>
    /// 配置文件信息
    /// </summary>
    public class ConfigVal
    {

        public static string RelyingName
        {
            get
            {
                try
                {
                    return ConfigurationManager.AppSettings["VerificationSsoToken:RelyingName"];
                }
                catch (Exception ex)
                {
                    _Log4Net.ErrorFormat("获取token依赖方失败: {0}", ex.Message);
                    return string.Empty;
                }
            }
        }

        public static string TokenUrl
        {
            get
            {
                try
                {
                    return ConfigurationManager.AppSettings["VerificationSsoToken:TokenUrl"];
                }
                catch (Exception ex)
                {
                    _Log4Net.ErrorFormat("获取token地址失败: {0}", ex.Message);
                    return string.Empty;
                }
            }
        }

        public static string Signing
        {
            get
            {
                try
                {
                    return ConfigurationManager.AppSettings["VerificationSsoToken:Signing"];
                }
                catch (Exception ex)
                {
                    _Log4Net.ErrorFormat("获取Signing失败: {0}", ex.Message);
                    return string.Empty;
                }
            }
        }

        public static string AESKey
        {
            get
            {
                try
                {
                    return ConfigurationManager.AppSettings["VerificationSsoToken:AESKey"];
                }
                catch (Exception ex)
                {
                    _Log4Net.ErrorFormat("获取AESKey失败: {0}", ex.Message);
                    return string.Empty;
                }
            }
        }

        public static string AESIv
        {
            get
            {
                try
                {
                    return ConfigurationManager.AppSettings["VerificationSsoToken:AESIv"];
                }
                catch (Exception ex)
                {
                    _Log4Net.ErrorFormat("获取AESKey失败: {0}", ex.Message);
                    return string.Empty;
                }
            }
        }

        public static string[] CrossDomainUrl
        {
            get
            {
                try
                {
                    return ConfigurationManager.AppSettings["CrossDomainUrl"].Split(';');
                }
                catch (Exception ex)
                {
                    _Log4Net.ErrorFormat("获取CrossDomainUrl失败: {0}", ex.Message);
                    return null;
                }
            }
        }

        #region  ApiAuth

        /// <summary>
        /// api认证appid
        /// </summary>
        public static string Appid
        {
            get
            {
                try
                {
                    return ConfigurationManager.AppSettings["ApiAuthorize:Appid"];
                }
                catch (Exception ex)
                {
                    _Log4Net.ErrorFormat("ApiAuthorize:Appid: {0}", ex.Message);
                    return string.Empty;
                }
            }
        }
        /// <summary>
        /// api认证固定密钥
        /// </summary>
        public static string Secret
        {
            get
            {
                try
                {
                    return ConfigurationManager.AppSettings["ApiAuthorize:Secret"];
                }
                catch (Exception ex)
                {
                    _Log4Net.ErrorFormat("ApiAuthorize:Secret: {0}", ex.Message);
                    return string.Empty;
                }
            }
        }
        /// <summary>
        /// api认证url
        /// </summary>
        public static string GwUrl
        {
            get
            {
                try
                {
                    return ConfigurationManager.AppSettings["ApiAuthorize:Url"];
                }
                catch (Exception ex)
                {
                    _Log4Net.ErrorFormat("ApiAuthorize:Url: {0}", ex.Message);
                    return string.Empty;
                }
            }
        }
        /// <summary>
        /// / api认证第三方url
        /// </summary>
        public static string Scope
        {
            get
            {
                try
                {
                    return ConfigurationManager.AppSettings["ApiAuthorize:Scope"];
                }
                catch (Exception ex)
                {
                    _Log4Net.ErrorFormat("ApiAuthorize:Scope: {0}", ex.Message);
                    return string.Empty;
                }
            }
        }

        /// <summary>
        /// api认证动态密钥类型
        /// </summary>
        public static string GrantType
        {
            get
            {
                try
                {
                    return ConfigurationManager.AppSettings["ApiAuthorize:GrantType"];
                }
                catch (Exception ex)
                {
                    _Log4Net.ErrorFormat("ApiAuthorize:Scope: {0}", ex.Message);
                    return string.Empty;
                }
            }
        }

        #endregion

        #region XGJProduct
        public static string IsSchoolKeeperSyncDateModified
        {
            get
            {
                try
                {
                    return ConfigurationManager.AppSettings["XGJProduct:IsSchoolKeeperSyncDateModified"];
                }
                catch (Exception ex)
                {
                    _Log4Net.ErrorFormat("获取IsSchoolKeeperSyncDateModified失败: {0}", ex.Message);
                    return string.Empty;
                }
            }
        }

        public static string SchoolKeeperSyncDate
        {
            get
            {
                try
                {
                    return ConfigurationManager.AppSettings["XGJProduct:SchoolKeeperSyncDate"];
                }
                catch (Exception ex)
                {
                    _Log4Net.ErrorFormat("获取SchoolKeeperSyncDate失败: {0}", ex.Message);
                    return string.Empty;
                }
            }
        }

        public static string SchoolKeeperURL
        {
            get
            {
                try
                {
                    return ConfigurationManager.AppSettings["XGJProduct:SchoolKeeperURL"];
                }
                catch (Exception ex)
                {
                    _Log4Net.ErrorFormat("获取SchoolKeeperURL失败: {0}", ex.Message);
                    return string.Empty;
                }
            }
        }

        #endregion

        #region XGJBasicsData
        public static string XGJBdURL
        {
            get
            {
                try
                {
                    return ConfigurationManager.AppSettings["XGBasicsData:XGJBdURL"];
                }
                catch (Exception ex)
                {
                    _Log4Net.ErrorFormat("获取XGJBdURL失败: {0}", ex.Message);
                    return string.Empty;
                }
            }
        }

        #endregion

        #region ECSThreshold
        public static string ECSThresholdURL
        {
            get
            {
                try
                {
                    return ConfigurationManager.AppSettings["ECSThreshold:ECSThresholdURL"];
                }
                catch (Exception ex)
                {
                    _Log4Net.ErrorFormat("获取ECSThresholdURL失败: {0}", ex.Message);
                    return string.Empty;
                }
            }
        }

        public static string ECSAppid
        {
            get
            {
                try
                {
                    return ConfigurationManager.AppSettings["ECSThreshold:ECSAppid"];
                }
                catch (Exception ex)
                {
                    _Log4Net.ErrorFormat("获取ECSAppid失败: {0}", ex.Message);
                    return string.Empty;
                }
            }
        }

        public static string ECSAppsecret
        {
            get
            {
                try
                {
                    return ConfigurationManager.AppSettings["ECSThreshold:ECSAppsecret"];
                }
                catch (Exception ex)
                {
                    _Log4Net.ErrorFormat("获取ECSAppsecret失败: {0}", ex.Message);
                    return string.Empty;
                }
            }
        }
        public static string TimeInterval
        {
            get
            {
                try
                {
                    return ConfigurationManager.AppSettings["ECSThreshold:TimeInterval"];
                }
                catch (Exception ex)
                {
                    _Log4Net.ErrorFormat("获取TimeInterval失败: {0}", ex.Message);
                    return string.Empty;
                }
            }
        }

        #endregion

    }
}

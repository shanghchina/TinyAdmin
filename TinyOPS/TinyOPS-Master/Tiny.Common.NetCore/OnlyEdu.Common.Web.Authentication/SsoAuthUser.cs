using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using OnlyEdu.Common.Encryption;
using OnlyEdu.Common.Web.Cookie;
using OnlyEdu.Common.Web.Session;

namespace OnlyEdu.Common.Web.Authentication
{

    /// <summary>
    /// 
    /// </summary>
    public class SsoAuthUser
    {
        private static IHttpContextAccessor _httpContextAccessor;
        private static ISession _session => _httpContextAccessor.HttpContext.Session;

        public SsoAuthUser(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        #region member

        /// <summary>SSO Cookie名称</summary>
        public const string SsoAuthCookie = "SSOAuth";

        /// <summary>获得认证会话名称</summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private static string GetSsoAuthSessionName(string name)
        {
            return "SsoAuthUser_" + name;
        }

        /// <summary>
        /// SSO认证凭据信息
        /// 1、判定登陆状态会使用到值
        /// 未经过SSO的模块，取值可能存在不正确情况
        /// </summary>
        public static SSOAuth SsoAuth
        {
            get
            {

                SSOAuth ssoAuth;
                try
                {
                    var ssoauthtemp = _session.GetObjectFromJson<SSOAuth>(SsoAuthUser.GetSsoAuthSessionName("SSOAuth"));
                    if (ssoauthtemp != null && ssoauthtemp.UserDictionary.Count > 0)
                    {
                        ssoAuth = ssoauthtemp;
                    }
                    else
                    {
                        string cipherText = CookieManager.GetCookies("SSOAuth");
                        if (!string.IsNullOrWhiteSpace(cipherText))
                        {
                            string str = AESEncryption.DecryptString(cipherText, OnlyAuthenticationConfig.SSOConfig.AES.Key, OnlyAuthenticationConfig.SSOConfig.AES.IV);
                            //SsoAuthUser._Log.Debug((object)"static SSOAuth SsoAuth :设定值到session");
                            Type type = typeof(SSOAuth);
                            ssoAuth = (SSOAuth)JsonConvert.DeserializeObject(str, type);
                            _session.SetObjectAsJson(SsoAuthUser.GetSsoAuthSessionName("SSOAuth"), (object)ssoAuth);
                        }
                        else
                            ssoAuth = (SSOAuth)null;
                    }
                }
                catch (Exception ex)
                {
                    //SsoAuthUser._Log.ErrorFormat("static SSOAuth SsoAuth Exception :{0} ", (object)JsonConvert.SerializeObject((object)ex));
                    throw;
                }
                return ssoAuth;
            }
            set
            {
                _session.SetObjectAsJson(SsoAuthUser.GetSsoAuthSessionName("SSOAuth"), (object)value);
            }
        }
        /// <summary>
        ///是否认证； 前后分离不要使用
        /// </summary>
        public static bool IsAuthenticated
        {
            get
            {
                bool flag;
                var flagtemp = _session.GetString(SsoAuthUser.GetSsoAuthSessionName(nameof(IsAuthenticated)));
                if (flagtemp != null)
                {
                    flag = _session.GetObjectFromJson<bool>(SsoAuthUser.GetSsoAuthSessionName(nameof(IsAuthenticated)));
                }
                else
                {
                    SSOAuth ssoAuth = SsoAuthUser.SsoAuth;
                    flag = ssoAuth != null && !(ssoAuth.Expires < DateTime.Now);
                    _session.SetObjectAsJson(SsoAuthUser.GetSsoAuthSessionName(nameof(IsAuthenticated)), (object)flag);
                }
                return flag;
            }
            set
            {
                _session.SetObjectAsJson(SsoAuthUser.GetSsoAuthSessionName(nameof(IsAuthenticated)), (object)value);
            }
        }

        #endregion
        #region SsoAuthUser对象

        /// <summary>个人邮箱</summary>
        public static string OnlyPersonalEmail
        {
            get
            {
                return SsoAuthUser.SsoAuth.UserDictionary[nameof(OnlyPersonalEmail)];
            }
        }

        /// <summary>用户Guid</summary>
        public static Guid OnlyUserGuid
        {
            get
            {
                return Guid.Parse(SsoAuthUser.SsoAuth.UserDictionary[nameof(OnlyUserGuid)]);
            }
        }

        /// <summary>用户ID</summary>
        public static int OnlyUserId
        {
            get
            {
                return int.Parse(SsoAuthUser.SsoAuth.UserDictionary["OnlyUserID"]);
            }
        }

        /// <summary>显示姓名</summary>
        public static string OnlyDisplayName
        {
            get
            {
                return SsoAuthUser.SsoAuth.UserDictionary[nameof(OnlyDisplayName)];
            }
        }

        /// <summary>性别</summary>
        public static string OnlySex
        {
            get
            {
                return SsoAuthUser.SsoAuth.UserDictionary[nameof(OnlySex)];
            }
        }

        /// <summary>用户组织名称</summary>
        public static string OnlyOrganization
        {
            get
            {
                return SsoAuthUser.SsoAuth.UserDictionary[nameof(OnlyOrganization)];
            }
        }

        /// <summary>用户组织ID</summary>
        public static string OnlyOrganizationId
        {
            get
            {
                return SsoAuthUser.SsoAuth.UserDictionary[nameof(OnlyOrganizationId)];
            }
        }

        /// <summary>用户手机号码</summary>
        public static string OnlyMobile
        {
            get
            {
                return SsoAuthUser.SsoAuth.UserDictionary[nameof(OnlyMobile)];
            }
        }

        /// <summary>用户属性</summary>
        /// <param name="onlyClaim"></param>
        /// <returns></returns>
        public static string UserProperty(string onlyClaim)
        {
            return SsoAuthUser.SsoAuth.UserDictionary[onlyClaim];
        }

        #endregion

        /// <summary>DefaultURL</summary>
        /// <returns></returns>
        public static string DefaultURL()
        {
            return OnlyAuthenticationConfig.SSOConfig.DefaultURL;
        }

        /// <summary>SSO登陆页地址</summary>
        /// <returns></returns>
        public static string LoginURL()
        {
            return OnlyAuthenticationConfig.SSOConfig.LoginURL;
        }


        #region operation
        /// <summary>
        /// 退出登陆删除SSOAuth相关信息
        /// 返回默认页地址
        /// </summary>
        public static string DeleteSsoAuthUser()
        {
            string str = SsoAuthUser.SsoAuth.LoginId.ToString();

            try
            {
                List<KeyValuePair<string, string>> keyValuePairList = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("LoginId", str)
                };
                using (HttpClient httpClient = new HttpClient())
                {
                    HttpResponseMessage result = httpClient.PostAsync(string.Format("{0}/Logout", (object)OnlyAuthenticationConfig.SSOConfig.TokenURL), (HttpContent)new FormUrlEncodedContent((IEnumerable<KeyValuePair<string, string>>)keyValuePairList)).Result;
                    //SsoAuthUser._Log.DebugFormat("DeleteSsoAuthUser TokenURL:{0}/Logout StatusCode:{1} loginId:{2}", (object)section.SSOConfig.TokenURL, (object)result.StatusCode, (object)str);
                }
                _session.Clear();
            }
            catch (Exception ex)
            {
                // SsoAuthUser._Log.ErrorFormat("DeleteSsoAuthUser } loginId:{0} Exception :{1}", (object)str, (object)JsonConvert.SerializeObject((object)ex));
            }
            SsoAuthUser.RemoveSso(OnlyAuthenticationConfig.SSOConfig.DomainName, "SSOAuth");
            return SsoAuthUser.DefaultURL();
        }

        /// <summary>删除SSO的Cookie信息</summary>
        /// <returns></returns>
        public static bool RemoveSso(string domainName, string ssoAuthCookie)
        {
            //CookieManager.Remove(domainName, ssoAuthCookie);
            return true;
        }

        /// <summary>移出SSO的cookie内容</summary>
        /// <returns></returns>
        public static bool RemoveSso()
        {
            //CookieManager.Remove(((AuthConfigurationSection)SsoAuthUser._configuration.GetSection("AuthConfigurationSection")).SSOConfig.DomainName, "SSOAuth");
            return true;
        }

        /// <summary>
        /// 获取cookie解密信息
        /// </summary>
        /// <returns></returns>
        public static string GetSSO()
        {
            try
            {
                //SsoAuthUser._Log.Debug((object)"获得Cookie值：GetSSO");

                string cipherText = CookieManager.GetCookies("SSOAuth");
                return string.IsNullOrEmpty(cipherText) ? string.Empty : AESEncryption.DecryptString(cipherText, OnlyAuthenticationConfig.SSOConfig.AES.Key, OnlyAuthenticationConfig.SSOConfig.AES.IV);
            }
            catch (Exception ex)
            {
                //SsoAuthUser._Log.ErrorFormat("GetSSO 获得Cookie  失败:{0}", (object)JsonConvert.SerializeObject((object)ex));
                return string.Empty;
            }
        }

        /// <summary>加密登陆信息字符串,主要应用在SSO登陆验证</summary>
        /// <param name="expires"></param>
        /// <param name="loginId"></param>
        /// <param name="userDictionary"></param>
        /// <param name="ttl"></param>
        /// <param name="domainName"></param>
        /// <returns></returns>
        public static string SetSsoAuthUser(
          out DateTime expires,
          Guid loginId,
          Dictionary<string, string> userDictionary,
          int ttl,
          string domainName = "")
        {
            expires = DateTime.Now.AddMinutes((double)(ttl * 60));
            SSOAuth ssoAuth = new SSOAuth();
            ssoAuth.LoginId = loginId;
            ssoAuth.UserDictionary = userDictionary;
            ssoAuth.Ttl = ttl;
            ssoAuth.Expires = expires;
            if (string.IsNullOrWhiteSpace(domainName))
                domainName = OnlyAuthenticationConfig.SSOConfig.DomainName;
            string str = AESEncryption.EncryptString(JsonConvert.SerializeObject((object)ssoAuth), OnlyAuthenticationConfig.SSOConfig.AES.Key, OnlyAuthenticationConfig.SSOConfig.AES.IV);
            //CookieManager.SetAlongWithBrowser(domainName, "SSOAuth", str);
            //SsoAuthUser.SsoAuth = ssoAuth; //TODO:缓存
            return str;
        }

        /// <summary>获得请求登陆用户状态签名</summary>
        /// <param name="loginId"></param>
        /// <param name="relyingName"></param>
        /// <param name="signing"></param>
        /// <returns></returns>
        public static string GetLoginSign(string loginId, string relyingName, string signing)
        {
            return SecurityEncryption.MD5_Encrypt_String(string.Format("{0}-{1}-{2}", (object)loginId, (object)relyingName, (object)signing));
        }
        #endregion
    }
}

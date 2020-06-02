using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tiny.Common.Dapper.DI;
using Tiny.OPS.Common;
using Tiny.OPS.Contract;
using Tiny.Common.Web;
using Tiny.Common.Web.Session;
using Tiny.OPS.Common.Web;
using Newtonsoft.Json;
using Tiny.Common.Encryption;
using System.Net.Http;
using Tiny.OPS.WebApi.Filter;
using Tiny.OPS.DomainService;

namespace Tiny.OPS.WebApi.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SsoUserInfoController : BaseController
    {
        private IHttpContextAccessor _contextAccessor => IoC.Resolve<IHttpContextAccessor>();
        private IPermissionDomainService permissionDomainService => IoC.Resolve<IPermissionDomainService>();
        /// <summary>
        /// 获取Sso用户信息
        /// </summary>
        [AllowAnonymous]
        [HttpPost]
        [NoCheck]
        public BaseResponse GetSsoUserInfo(UserInfoRequest request)
        {
            try
            {
                var loginId = request.LoginId;
                if (!string.IsNullOrWhiteSpace(request.LoginId))
                {
                    var md5Data = HttpContextUtils.Md5Encrypt($"{loginId}-{ConfigVal.RelyingName}-{ConfigVal.Signing}");
                    var jsonData = JsonConvert.SerializeObject(new { LoginId = loginId, ConfigVal.RelyingName, Sign = md5Data.ToUpper() });
                    var result = HttpUtils.PostHttps(ConfigVal.TokenUrl + "/AccountToken/Token", jsonData);
                    #region 获取用户信息
                    _Log4Net.InfoFormat("Sso获取信息result：{0};AESKey: {1};AESIv: {2}", result, ConfigVal.AESKey, ConfigVal.AESIv);
                    string ssoUserInfoJson = AESEncryption.DecryptString(result, ConfigVal.AESKey, ConfigVal.AESIv);
                    if (ssoUserInfoJson.Contains("LoginId"))
                    {
                        _Log4Net.InfoFormat("Sso获取信息解密后数据：{0}", ssoUserInfoJson);
                        var ssoUserInfo = JsonConvert.DeserializeObject<SsoUserInfo>(ssoUserInfoJson);
                        var userInfoResponse = new UserInfoResponse
                        {
                            UserGuid = ssoUserInfo.UserDictionary.OnlyUserGuid,
                            Name = ssoUserInfo.UserDictionary.OnlyDisplayName,
                            Sex = ssoUserInfo.UserDictionary.OnlySex,
                            LevelOneOrgName = ssoUserInfo.UserDictionary.OnlyLevelOneUserOrgName,
                            OrganizationId = ssoUserInfo.UserDictionary.OnlyOrganizationId,
                            Organization = ssoUserInfo.UserDictionary.OnlyOrganization,
                            phone = ssoUserInfo.UserDictionary.OnlyMobile,
                            Email = ssoUserInfo.UserDictionary.OnlyPersonalEmail,
                        };
                        #endregion

                        #region  获取 api access_token
                        var tokenJsonData = JsonConvert.SerializeObject(new
                        {
                            client_id = ConfigVal.Appid,
                            secret = ConfigVal.Secret,
                            grant_type = ConfigVal.GrantType,
                            scope = ConfigVal.Scope,
                            refresh_token = ""
                        });
                        var gwResult = HttpUtils.PostHttps(ConfigVal.GwUrl + "api/OAuth2Token/token", tokenJsonData);
                        var gwResponse = JsonConvert.DeserializeObject<GatewayResponse>(gwResult);
                        if (gwResponse.resultinfo != null)
                        {
                            userInfoResponse.AccessToken = gwResponse.resultinfo.access_token;
                            userInfoResponse.Secrectkey = gwResponse.resultinfo.secrect_key;
                        }
                        else
                        {
                            return ApiFailResult("获取token失败,请重新登录");
                        }
                        #endregion

                        #region 获取用户权限点
                        if (userInfoResponse != null)
                        {
                            userInfoResponse.PermissionPoints = permissionDomainService.GetByUserId(userInfoResponse.UserGuid).Select(p => p.PermissionsAttributes).ToList();
                        }
                        #endregion

                        return ApiSuccessResult(userInfoResponse);
                    }
                    else
                    {
                        return ApiFailResult("登录超时,请重新登录");
                    }
                }
                else
                {
                    return ApiFailResult("登录超时,请重新登录");
                }
            }
            catch (Exception ex)
            {
                _Log4Net.ErrorFormat("GetSsoUserInfo异常：{0}", ex.Message);
                return ApiErrorResult(ex.Message);
            }
        }

        /// <summary>
        /// 登出
        /// </summary>
        /// <param name="request">LoginId：LoginId</param>
        /// <returns></returns>
        [HttpPost]
        public BaseResponse LoginOut(SsoLoginOutRequest request)
        {
            _Log4Net.Info(string.Format("接收到用户登出，请求参数：{0}", JsonConvert.SerializeObject(request)));
            try
            {
                if (request == null || string.IsNullOrEmpty(request.LoginId))
                {
                    return ApiFailResult("LoginId必填");
                }
                var values = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("LoginId",request.LoginId),
                };
                string StatusCode = "";
                using (var client = new HttpClient())
                {
                    var result = client.PostAsync(ConfigVal.TokenUrl + "/AccountToken/Logout", new FormUrlEncodedContent(values)).Result;
                    StatusCode = result.StatusCode.ToString();
                }
                if (StatusCode.ToUpper() == "OK")
                {
                    return ApiSuccessResult("成功");
                }
                else
                {
                    return ApiFailResult("失败");
                }
            }
            catch (System.Exception ex)
            {
                _Log4Net.ErrorFormat("LoginOut异常：{0}", ex.Message);
                return ApiErrorResult(ex.Message);
            }
        }
    }
}
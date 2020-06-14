using Microsoft.AspNetCore.Mvc.Filters;

namespace TinyEdu.Admin.WebApi.Filter
{
    /// <summary>
    /// api身份验证
    /// </summary>
    public class ApiAuthorizeAttribute : ActionFilterAttribute
    {
        private static readonly bool Enable = false;// bool.Parse(System.Configuration.ConfigurationManager.AppSettings["Md5VerificationEnable"]);
                                                    //private static IAPP_WhiteListDomainService WhiteListDomainService => IoC.Resolve<IAPP_WhiteListDomainService>();

        //private static IAPP_SysLogDomainService app_SysLogDomainService => IoC.Resolve<IAPP_SysLogDomainService>();

        //public class Singleton
        //{
        //    private static T_SYS_InvokeLog _Singleton = null;
        //    private static object InvokeLog_Lock = new object();
        //    public static T_SYS_InvokeLog CreateInstance()
        //    {
        //        if (_Singleton == null) //双if +lock
        //        {
        //            lock (InvokeLog_Lock)
        //            {
        //                if (_Singleton == null)
        //                {
        //                    _Singleton = new T_SYS_InvokeLog();
        //                }
        //            }
        //        }
        //        return _Singleton;
        //    }
        //}


        /// <summary>
        /// api身份验证执行
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {

            //秘钥校验
            var heads = context.HttpContext.Request.Headers;
            string appId = "";
            foreach (var item in heads)
            {
                switch (item.Key.ToUpper())
                {
                    case "APPID":
                        appId = item.Value;
                        break;
                    default:
                        break;
                }
            }


            base.OnActionExecuting(context);
        }

        //    /// <summary>
        //    /// api身份验证执行
        //    /// </summary>
        //    /// <param name="context"></param>
        //    public override void OnActionExecuting(ActionExecutingContext context)
        //    {

        //        //白名单验证
        //        //VerifyWhiteList(context);
        //        var descriptor = context.ActionDescriptor as ControllerActionDescriptor;//.net core 2.1以后获取自定义filter
        //        if (!descriptor.MethodInfo.GetCustomAttributes(typeof(NoCheck), false).Any())//如果要搜索此成员的继承链以查找属性，则为 true；否则为 false。
        //        {
        //            //头部参数和sign加密验证
        //            VerifyHeaderParametersAndSign(context);
        //        }

        //        base.OnActionExecuting(context);
        //    }

        //    /// <summary>
        //    /// 白名单验证
        //    /// </summary>
        //    /// <param name="context"></param>
        //    private void VerifyWhiteList(ActionExecutingContext context)
        //    {
        //        var whiteList = WhiteListDomainService.GetSysWhiteListAll();
        //        var ipAddress = HttpContextUtils.GetClientIP(context.HttpContext);
        //        if (whiteList.All(p => p.Ip != ipAddress))
        //        {
        //            LogInfo("VerifyWhiteList:", ipAddress + "访问IP不在白名单中");
        //            ReturnError(context, ipAddress + "访问IP不在白名单中");
        //        }
        //    }

        //    /// <summary>
        //    /// 返回错误消息信息
        //    /// </summary>
        //    /// <param name="context"></param>
        //    /// <param name="msg"></param>

        //    public void ReturnError(ActionExecutingContext context, string msg)
        //    {
        //        BaseResponseModel<string> response = new BaseResponseModel<string> { Code = CodeConst.SystemException, Message = msg };
        //        context.HttpContext.Response.StatusCode = Convert.ToInt32(HttpStatusCode.InternalServerError);
        //        context.Result = new JsonResult(response);

        //    }

        //    /// <summary>
        //    /// 日记记录
        //    /// </summary>
        //    /// <param name="desc"></param>
        //    /// <param name="info"></param>
        //    public void LogInfo(string desc, string info)
        //    {
        //        _Log4Net.Error(desc + info);
        //        app_SysLogDomainService.AddLogError(desc, desc + info);

        //    }

        //    /// <summary>
        //    /// 头部参数和sign加密验证
        //    /// </summary>
        //    /// <param name="context"></param>
        //    private void VerifyHeaderParametersAndSign(ActionExecutingContext context)
        //    {
        //        var heads = context.HttpContext.Request.Headers;
        //        string appId = "", transactionId = "", token = "", timestamp = "", sign = "", version = "";
        //        foreach (var item in heads)
        //        {
        //            switch (item.Key.ToUpper())
        //            {
        //                case "APPID":
        //                    appId = item.Value;
        //                    break;
        //                case "TRANSACTIONID":
        //                    transactionId = item.Value;
        //                    break;
        //                case "TOKEN":
        //                    token = item.Value;
        //                    break;
        //                case "TIMESTAMP":
        //                    timestamp = item.Value;
        //                    break;
        //                case "SIGN":
        //                    sign = item.Value;
        //                    break;
        //                case "VERSION":
        //                    version = item.Value;
        //                    break;
        //                default:
        //                    break;
        //            }
        //        }
        //        if (string.IsNullOrEmpty(appId))
        //        {
        //            ReturnError(context, "APPID不能为空");
        //        }
        //        if (string.IsNullOrEmpty(token))
        //        {
        //            ReturnError(context, "TOKEN不能为空");
        //        }
        //        if (string.IsNullOrEmpty(sign))
        //        {
        //            ReturnError(context, "SIGN不能为空");
        //        }
        //        if (string.IsNullOrEmpty(version))
        //        {
        //            ReturnError(context, "VERSION不能为空");
        //        }
        //        if (string.IsNullOrEmpty(transactionId))
        //        {
        //            ReturnError(context, "TransactionID不能为空");
        //        }
        //        else
        //        {
        //            try
        //            {
        //                if (transactionId.Length != 21)
        //                {
        //                    ReturnError(context, "TransactionID为21位数字");
        //                }
        //                string datePart = transactionId.Substring(0, 17);
        //                IFormatProvider ifp = new CultureInfo("zh-CN", true);
        //                DateTime time;
        //                bool isDate = DateTime.TryParseExact(datePart, "yyyyMMddHHmmssfff", ifp, DateTimeStyles.None, out time);
        //                if (!isDate)
        //                {
        //                    ReturnError(context, "TransactionID必须包含时间部分");
        //                }
        //                string serialNo = transactionId.Substring(17);
        //                int number;
        //                bool isNumber = int.TryParse(serialNo, out number);
        //                if (!isNumber)
        //                {
        //                    ReturnError(context, "TransactionID后4位必须为随机数字");
        //                }
        //            }
        //            catch (Exception)
        //            {
        //                ReturnError(context, "TransactionID校验异常");
        //            }
        //        }
        //        if (string.IsNullOrEmpty(timestamp))
        //        {
        //            ReturnError(context, "Timestamp不能为空");
        //        }
        //        else
        //        {
        //            try
        //            {
        //                if (timestamp.Length != 14)
        //                {
        //                    ReturnError(context, "Timestamp为14位数字");
        //                }
        //                IFormatProvider ifp = new CultureInfo("zh-CN", true);
        //                DateTime time;
        //                bool isSuccess = DateTime.TryParseExact(timestamp, "yyyyMMddHHmmss", ifp, DateTimeStyles.None, out time);
        //                if (!isSuccess)
        //                {
        //                    ReturnError(context, "Timestamp必须为有效时间");
        //                }
        //            }
        //            catch (Exception)
        //            {
        //                ReturnError(context, "Timestamp校验异常");
        //            }
        //        }


        //        //验证Token
        //        string secrectKey = "";
        //        string parameter = string.Format("appId：{0}token：{1}timestamp：{2}sign：{3}", appId, token, timestamp, sign);
        //        var resultInfo = TokenVerify(token);
        //        var convertResultInfo = JsonConvert.DeserializeObject<dynamic>(resultInfo);
        //        if ((string)convertResultInfo.code == "0")
        //        {
        //            secrectKey = convertResultInfo.resultinfo.secrect_key;
        //        }

        //        if (string.IsNullOrEmpty(secrectKey))
        //        {
        //            _Log4Net.Info(string.Format("token验证失败:{0}", parameter));
        //            ReturnError(context, "token验证失败");
        //        }



        //        var key = appId + timestamp + secrectKey;
        //        string md5str = HttpContextUtils.Md5Encrypt(key).ToUpper();
        //        _Log4Net.Info(string.Format("api获取的签名参数:{0}，加密后{1}------>", key, md5str));
        //        _Log4Net.Info(string.Format("前端加密sign:{0}", sign));
        //        if (sign != md5str)
        //        {
        //            _Log4Net.Info(string.Format("sign加密验证失败 前端:{0}api：{1}------>", sign, md5str));
        //            ReturnError(context, "sign加密验证失败");
        //        }

        //    }

        //    /// <summary>
        //    /// Token验证
        //    /// </summary>
        //    /// <param name="accessToken">需要验证的令牌</param>
        //    /// <returns></returns>
        //    private string TokenVerify(string accessToken)
        //    {

        //        var verifyUrl = ConfigVal.GwUrl + "api/OAuth2Token/Verify";
        //        var tokenInfo = new
        //        {
        //            appid = ConfigVal.Appid,
        //            secret = ConfigVal.Secret,
        //            access_token = accessToken
        //        };
        //        var requestJson = Newtonsoft.Json.JsonConvert.SerializeObject(tokenInfo);
        //        HttpContent content = new StringContent(requestJson);
        //        content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
        //        var returnJson = new HttpClient().PostAsync(verifyUrl, content).Result;
        //        return returnJson.Content.ReadAsStringAsync().Result;

        //    }

    }
}

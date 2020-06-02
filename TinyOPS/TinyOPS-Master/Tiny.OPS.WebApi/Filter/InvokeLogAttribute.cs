using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using Tiny.Common.Dapper.DI;
using Tiny.Common.Dapper.Persistence.Context;
using Tiny.Common.Dapper.Persistence.UnitOfWork;
using Tiny.OPS.Common;
using Tiny.OPS.Contract;
using Tiny.OPS.Domain;
using Tiny.OPS.DomainService;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tiny.OPS.WebApi.Filter
{
    /// <summary>
    /// 日志记录
    /// </summary>
    public class InvokeLogAttribute : ActionFilterAttribute
    {
        public ISYS_InvokeLogDomainService InvokeLogDomainService => Tiny.Common.Dapper.DI.IoC.Resolve<ISYS_InvokeLogDomainService>();

        /// <summary>
        /// InvokeLog单例模式
        /// </summary>
        public class Singleton
        {
            private static T_SYS_InvokeLog _Singleton = null;
            private static object InvokeLog_Lock = new object();
            public static T_SYS_InvokeLog CreateInstance()
            {
                if (_Singleton == null) //双if +lock
                {
                    lock (InvokeLog_Lock)
                    {
                        if (_Singleton == null)
                        {
                            _Singleton = new T_SYS_InvokeLog();
                        }
                    }
                }
                return _Singleton;
            }
        }

        private static bool _Enable = true;// bool.Parse(System.Configuration.ConfigurationManager.AppSettings["InvokeLogEnable"].ToString());

        public bool IgnoreReturn { get; set; }

        /// <summary>
        /// 当前HttpContext
        /// </summary>
        //public Microsoft.AspNetCore.Http.HttpContext currentContext { get; set; }//=CurrentHttpContext.Current;

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            try
            {
                if (_Enable)
                {
                    var hashCode = context.HttpContext.Request.GetHashCode();
                    var _log = Singleton.CreateInstance().GetLog(hashCode);
                    if (_log == null)
                    {
                        _log = Singleton.CreateInstance();
                    }

                    string requestLocalIp = (context.HttpContext.Request.HttpContext.Connection.LocalIpAddress.MapToIPv4().ToString() + ":" + context.HttpContext.Request.HttpContext.Connection.LocalPort);
                    string requestRemoteIp = (context.HttpContext.Request.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString() + ":" + context.HttpContext.Request.HttpContext.Connection.RemotePort);

                    //调用方IP
                    _log.InvokeIP = $"{requestLocalIp};{requestRemoteIp}";//context.HttpContext.Connection.RemoteIpAddress.ToString();
                    //调用链接
                    _log.InvokeURL = context.HttpContext.Request.GetAbsoluteUri();
                    //调用方法
                    _log.InvokeFunction = context.RouteData.Values["action"].ToString();

                    //调用对象
                    _log.InvokeJson = string.Empty;
                    if (context.HttpContext.Request.Body.CanSeek)
                    {
                        using (var requestSm = context.HttpContext.Request.Body)
                        {
                            requestSm.Position = 0;
                            var reader = new StreamReader(requestSm, Encoding.UTF8);
                            _log.InvokeJson = reader.ReadToEnd();
                        }
                    }

                    //开始时间
                    _log.CreatedDate = DateTime.Now;
                    RecordRequestLog(context, _log.InvokeJson, _log.InvokeIP, _log.InvokeURL, _log.InvokeFunction);
                }
                base.OnActionExecuting(context);
            }
            catch (Exception ex)
            {
                _Log4Net.Error("OnActionExecuting", ex);
            }
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            try
            {
                base.OnActionExecuted(context);
                if (_Enable)
                {
                    var hashCode = context.HttpContext.Request.GetHashCode();
                    var _log = Singleton.CreateInstance().GetLog(hashCode);
                    if (_log == null)
                    {
                        _log = Singleton.CreateInstance();
                        _log.CreatedDate = System.DateTime.Now;
                    }

                    string strJson = string.Empty;//返回结果字符串
                    if (!IgnoreReturn)
                    {
                        //返回对象
                        try
                        {
                            dynamic actionResult = context.Result;
                            strJson = JsonConvert.SerializeObject(actionResult.Value);
                            _log.ReturnJson = strJson.Length > 4000 ? "" : strJson;
                        }
                        catch (Exception)
                        {
                            IoC.Resolve<IContext>().Local.UnitOfWorks = new List<IUnitOfWork>();
                        }

                    }
                    //结束时间
                    _log.UpdateDate = DateTime.Now;
                    //总耗时
                    _log.UseTime = Math.Round((_log.UpdateDate.Value - _log.CreatedDate).TotalMilliseconds, 2);
                    _Log4Net.Info(string.Format("响应Url{0},响应参数{1}", context.HttpContext.Request.GetAbsoluteUri(), strJson));
                    var _result = InvokeLogDomainService.Add(_log);
                    if (_result.IsSuccess)
                    {
                        _result.Commit();
                    }
                }
            }
            catch (Exception ex)
            {
                _Log4Net.Error("OnActionExecuted", ex);
            }
        }

        /// <summary>
        /// 记录请求日志并转把head中的transactionId和timestamp做格式校验,通过则赋值给request
        /// </summary>
        /// <param name="context">上下文</param>
        /// <param name="requestJsonBody">请求jsonBody</param>
        /// <param name="ip">调用方接收方ip地址</param>
        /// <param name="url">url地址</param>
        /// <param name="function">方法名称</param>
        public void RecordRequestLog(ActionExecutingContext context, string requestJsonBody, string ip, string url, string function)
        {
            var heads = context.HttpContext.Request.Headers;
            string appId = "", transactionId = "", token = "", timestamp = "", sign = "", version = "";
            foreach (var item in heads)
            {
                switch (item.Key.ToUpper())
                {
                    case "APPID":
                        appId = item.Value;
                        break;
                    case "TRANSACTIONID":
                        transactionId = item.Value;
                        break;
                    case "TOKEN":
                        token = item.Value;
                        break;
                    case "TIMESTAMP":
                        timestamp = item.Value;
                        break;
                    case "SIGN":
                        sign = item.Value;
                        break;
                    case "VERSION":
                        version = item.Value;
                        break;
                    default:
                        break;
                }
            }
            HeadModel headModel = new HeadModel();
            headModel.AppId = appId;
            headModel.Token = token;
            headModel.Timestamp = timestamp;
            headModel.Sign = sign;
            headModel.Version = version;
            headModel.TransactionId = transactionId;
            RequestLog request = new RequestLog();
            request.URI = "url";
            request.Method = function;
            request.IP = ip;
            request.LogData = requestJsonBody;
            request.Head = headModel;

            string logInfo = string.Format("请求Url{0},请求参数{1}", url, JsonConvert.SerializeObject(request));
            _Log4Net.Info(logInfo);
            //var app_SysLogDomainService = IoC.Resolve<IAPP_SysLogDomainService>();
            //app_SysLogDomainService.AddLogInfo("RecordRequestLog", logInfo);
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TinyEdu.Util;
using TinyEdu.Util.Extension;
using TinyEdu.Util.Model;

namespace TinyEdu.Admin.WebApi.Controllers
{
    /// <summary>
    /// CustomJsonResult
    /// </summary>
    public class CustomJsonResult : JsonResult
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public CustomJsonResult() : base(string.Empty)
        { }

        /// <summary>
        /// ExecuteResult
        /// </summary>
        /// <param name="context"></param>
        public override void ExecuteResult(ActionContext context)
        {
            this.ContentType = "text/json;charset=utf-8;";

            JsonSerializerSettings jsonSerizlizerSetting = new JsonSerializerSettings();
            jsonSerizlizerSetting.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

            string json = JsonConvert.SerializeObject(Value, Formatting.None, jsonSerizlizerSetting);
            Value = json;
            base.ExecuteResult(context);
        }

        /// <summary>
        /// ExecuteResultAsync
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>

        public override Task ExecuteResultAsync(ActionContext context)
        {
            this.ContentType = "text/json;charset=utf-8;";

            JsonSerializerSettings jsonSerizlizerSetting = new JsonSerializerSettings();
            jsonSerizlizerSetting.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

            string json = JsonConvert.SerializeObject(Value, Formatting.None, jsonSerizlizerSetting);
            Value = json.ToJObject();
            return base.ExecuteResultAsync(context);
        }
    }

    /// <summary>
    /// base基类方法名必须protected，否则会引起swagger报错
    /// </summary>
    public class BaseController : ControllerBase
    {
        #region 根据action方法名赋值合适的message
        private void SetTDataMessage(object data)
        {
            string action = this.ControllerContext.RouteData.Values["Action"].ParseToString();
            TData obj = data as TData;
            if (obj != null && string.IsNullOrEmpty(obj.Message))
            {
                if (action.Contains("Delete"))
                {
                    obj.Message = "删除成功";
                }
                else if (action.Contains("Save"))
                {
                    obj.Message = "保存成功";
                }
                else
                {
                    obj.Message = "操作成功";
                }
            }
        }
        #endregion

        /// <summary>
        /// 覆盖基类的Json方法，用来自定义序列化实体，比如把long类型转成字符串返回到前端
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        protected CustomJsonResult Json(object data)
        {
            SetTDataMessage(data);

            return new CustomJsonResult
            {
                Value = data
            };
        }

        //    private IAPP_SysLogDomainService app_SysLogDomainService => IoC.Resolve<IAPP_SysLogDomainService>();


        //    /// <summary>
        //    /// API规范返回值
        //    /// </summary>
        //    /// <returns></returns>
        //    protected IHttpActionResult ApiSuccessResult()
        //    {
        //        return Json(new TSC.Models.ApiResult());
        //    }

        //    /// <summary>
        //    /// API规范返回值
        //    /// </summary>
        //    /// <typeparam name="T"></typeparam>
        //    /// <param name="returnData"></param>
        //    /// <param name="message"></param>
        //    /// <returns></returns>
        //    protected IHttpActionResult ApiSuccessResult<T>(T returnData, string message = "")
        //    {
        //        var info = new object[1];
        //        info[0] = returnData;
        //        return Json(new TSC.Models.ApiResult { Code = "0", Message = "操作成功", Data = info });
        //    }

        //    /// <summary>
        //    /// API规范返回值
        //    /// </summary>
        //    /// <typeparam name="T"></typeparam>
        //    /// <param name="returnData"></param>
        //    /// <param name="message"></param>
        //    /// <returns></returns>
        //    protected IHttpActionResult ApiSuccessResult<T>(List<T> returnData, string message = "")
        //    {
        //        return Json(new { Code = "0", Message = "操作成功", Data = returnData });
        //    }

        //    /// <summary>
        //    /// API规范返回值
        //    /// </summary>
        //    /// <typeparam name="T"></typeparam>
        //    /// <param name="returnData"></param>
        //    /// <param name="TotalCount"></param>
        //    /// <returns></returns>
        //    protected IHttpActionResult ApiSuccessResult<T>(List<T> returnData, long TotalCount)
        //    {
        //        return Json(new { Code = "0", Message = "操作成功", Data = returnData, TotalCount = TotalCount });
        //    }

        //    /// <summary>
        //    /// API规范返回值
        //    /// </summary>
        //    /// <param name="message"></param>
        //    /// <returns></returns>
        //    protected IHttpActionResult ApiErrorResult(string message)
        //    {
        //        return Json(new TSC.Models.ApiResult { Code = "999", Message = message });
        //    }

        //    /// <summary>
        //    /// API规范返回值
        //    /// </summary>
        //    /// <param name="message"></param>
        //    /// <returns></returns>
        //    protected IHttpActionResult ApiFailResult(string message)
        //    {
        //        return Json(new TSC.Models.ApiResult { Code = "-1", Message = message });
        //    }



        //    #region 日志记录到数据库中
        //    /// <summary>
        //    /// 添加日志记录到数据库-提示信息
        //    /// </summary>
        //    /// <param name="logDesc">标题</param>
        //    /// <param name="logParam">参数</param>
        //    /// <param name="logInfo">内容</param>
        //    /// <param name="updaterUserId"></param>
        //    protected void AddLogInfo(string logDesc, string logInfo, string logParam = "")
        //    {
        //        try
        //        {
        //            app_SysLogDomainService.AddLogInfo(logDesc, logInfo, logParam);
        //        }
        //        catch (System.Exception ex)
        //        {
        //            Utility._Log4Net.Error("AddLogInfo", ex);
        //        }
        //    }

        //    /// <summary>
        //    /// 添加日志记录到数据库-异常信息
        //    /// </summary>
        //    /// <param name="logDesc">标题</param>
        //    /// <param name="logParam">参数</param>
        //    /// <param name="logInfo">内容</param>
        //    /// <param name="updaterUserId"></param>
        //    protected void AddLogError(string logDesc, string logInfo, string logParam = "")
        //    {
        //        try
        //        {
        //            app_SysLogDomainService.AddLogError(logDesc, logInfo, logParam);
        //        }
        //        catch (System.Exception ex)
        //        {
        //            Utility._Log4Net.Error("AddLogError", ex);
        //        }
        //    }
        //    protected void AddLogError(string logDesc, Exception exInfo, string logParam = "")
        //    {
        //        try
        //        {
        //            app_SysLogDomainService.AddLogError(logDesc, exInfo, logParam);
        //        }
        //        catch (System.Exception ex)
        //        {
        //            Utility._Log4Net.Error("AddLogError", ex);
        //        }
        //    }

        //    /// <summary>
        //    /// 添加日志记录到数据库-警告信息
        //    /// </summary>
        //    /// <param name="logDesc">标题</param>
        //    /// <param name="logParam">参数</param>
        //    /// <param name="logInfo">内容</param>
        //    /// <param name="updaterUserId"></param>
        //    protected void AddLogWarning(string logDesc, string logInfo, string logParam = "")
        //    {
        //        try
        //        {
        //            app_SysLogDomainService.AddLogWarning(logDesc, logInfo, logParam);
        //        }
        //        catch (System.Exception ex)
        //        {
        //            Utility._Log4Net.Error("AddLogWarning", ex);
        //        }
        //    }

        //    protected void DeleteSysLog(string logType, DateTime dateBegin, DateTime dateEnd)
        //    {
        //        try
        //        {
        //            app_SysLogDomainService.DeleteLogByDate(logType, dateBegin, dateEnd);
        //        }
        //        catch (System.Exception ex)
        //        {
        //            Utility._Log4Net.Error("DeleteSysLog", ex);
        //            throw ex;
        //        }
        //    }
        //    #endregion
    }
}

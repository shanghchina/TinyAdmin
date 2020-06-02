using Tiny.Common.Dapper.DI;
using Tiny.Common.Dapper.Service;
using Tiny.OPS.Common;
using Tiny.OPS.Domain;
using Tiny.OPS.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tiny.OPS.DomainService
{
    public class APP_SysLogDomainService : RealDomainService<T_APP_SysLog>, IAPP_SysLogDomainService
    {
        private IAPP_SysLogRepository app_SysLogRepository = IoC.Resolve<IAPP_SysLogRepository>();

        /// <summary>
        /// 添加日志记录到数据库-提示信息
        /// </summary>
        /// <param name="logDesc">标题</param>
        /// <param name="logParam">参数</param>
        /// <param name="logInfo">内容</param>
        /// <param name="updaterUserId"></param>
        public void AddLogInfo(string logDesc, string logInfo, string logParam = "")
        {
            try
            {
                T_APP_SysLog entity = new T_APP_SysLog();
                entity.LogType = "Info";
                entity.LogDesc = logDesc;
                entity.LogParam = logParam;
                entity.LogInfo = logInfo;
                entity.UpdaterUserId = "00000000-0000-0000-0000-000000000000";
                entity.UpdaterUserName = "APP_SysLog";
                entity.UpdateDate = DateTime.Now;
                entity.CreatedDate = DateTime.Now;

                app_SysLogRepository.AddLog(entity);
            }
            catch (Exception ex)
            {
                _Log4Net.Error("APP_SysLogDomainService.AddLogInfo", ex);
            }
        }

        /// <summary>
        /// 添加日志记录到数据库-异常信息
        /// </summary>
        /// <param name="logDesc">标题</param>
        /// <param name="logParam">参数</param>
        /// <param name="exInfo">异常内容</param>
        /// <param name="updaterUserId"></param>
        public void AddLogError(string logDesc, Exception exInfo, string logParam = "")
        {
            try
            {
                T_APP_SysLog entity = new T_APP_SysLog();
                entity.LogType = "Error";
                entity.LogDesc = logDesc;
                entity.LogParam = logParam;
                entity.LogInfo = exInfo.ToString();
                entity.UpdaterUserId = "00000000-0000-0000-0000-000000000000";
                entity.UpdaterUserName = "APP_SysLog";
                entity.UpdateDate = DateTime.Now;
                entity.CreatedDate = DateTime.Now;

                app_SysLogRepository.AddLog(entity);
            }
            catch (Exception ex)
            {
                _Log4Net.Error("APP_SysLogDomainService.AddLogError", ex);
            }
        }
        public void AddLogError(string logDesc, string logInfo, string logParam = "")
        {
            try
            {
                T_APP_SysLog entity = new T_APP_SysLog();
                entity.LogType = "Error";
                entity.LogDesc = logDesc;
                entity.LogParam = logParam;
                entity.LogInfo = logInfo;
                entity.UpdaterUserId = "00000000-0000-0000-0000-000000000000";
                entity.UpdaterUserName = "APP_SysLog";
                entity.UpdateDate = DateTime.Now;
                entity.CreatedDate = DateTime.Now;

                app_SysLogRepository.AddLog(entity);
            }
            catch (Exception ex)
            {
                _Log4Net.Error("APP_SysLogDomainService.AddLogError", ex);
            }
        }

        /// <summary>
        /// 添加日志记录到数据库-警告信息
        /// </summary>
        /// <param name="logDesc">标题</param>
        /// <param name="logParam">参数</param>
        /// <param name="logInfo">内容</param>
        /// <param name="updaterUserId"></param>
        public void AddLogWarning(string logDesc, string logInfo, string logParam = "")
        {
            try
            {
                T_APP_SysLog entity = new T_APP_SysLog();
                entity.LogType = "Warning";
                entity.LogDesc = logDesc;
                entity.LogParam = logParam;
                entity.LogInfo = logInfo;
                entity.UpdaterUserId = "00000000-0000-0000-0000-000000000000";
                entity.UpdaterUserName = "APP_SysLog";
                entity.UpdateDate = DateTime.Now;
                entity.CreatedDate = DateTime.Now;

                app_SysLogRepository.AddLog(entity);
            }
            catch (Exception ex)
            {
                _Log4Net.Error("APP_SysLogDomainService.AddLogWarning", ex);
            }
        }

        /// <summary>
        /// 根据类型和日期删除日志
        /// </summary>
        /// <param name="DateBegin">开始日期</param>
        /// <param name="DateEnd">结束日期</param>
        /// <returns>记录行数</returns>
        public int DeleteLogByDate(string logType, DateTime dateBegin, DateTime dateEnd)
        {
            int result = 0;
            try
            {
                result = app_SysLogRepository.DeleteLogByDate(logType, dateBegin, dateEnd);
            }
            catch (Exception ex)
            {
                _Log4Net.Error("APP_SysLogDomainService.DeleteLogByDate", ex);
            }
            return result;
        }
    }
}

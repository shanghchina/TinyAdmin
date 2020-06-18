using System;
using TinyEdu.Common.Dapper.Service;

namespace TinyEdu.Admin.DomainService
{
    public interface IAPP_SysLogDomainService : IDomainService
    {
        /// <summary>
        /// 添加日志记录到数据库-提示信息
        /// </summary>
        /// <param name="logDesc">标题</param>
        /// <param name="logParam">参数</param>
        /// <param name="logInfo">内容</param>
        /// <param name="updaterUserId"></param>
        void AddLogInfo(string logDesc, string logInfo, string logParam = "");

        /// <summary>
        /// 添加日志记录到数据库-异常信息
        /// </summary>
        /// <param name="logDesc">标题</param>
        /// <param name="logParam">参数</param>
        /// <param name="logInfo">内容</param>
        /// <param name="updaterUserId"></param>
        void AddLogError(string logDesc, string logInfo, string logParam = "");

        /// <summary>
        /// 添加日志记录到数据库-异常信息
        /// </summary>
        /// <param name="logDesc">标题</param>
        /// <param name="logParam">参数</param>
        /// <param name="exInfo">异常内容</param>
        /// <param name="updaterUserId"></param>
        void AddLogError(string logDesc, Exception exInfo, string logParam = "");

        /// <summary>
        /// 添加日志记录到数据库-警告信息
        /// </summary>
        /// <param name="logDesc">标题</param>
        /// <param name="logParam">参数</param>
        /// <param name="logInfo">内容</param>
        /// <param name="updaterUserId"></param>
        void AddLogWarning(string logDesc, string logInfo, string logParam = "");

        /// <summary>
        /// 根据类型和日期删除日志
        /// </summary>
        /// <param name="DateBegin">开始日期</param>
        /// <param name="DateEnd">结束日期</param>
        /// <returns>记录行数</returns>
        int DeleteLogByDate(string logType, DateTime dateBegin, DateTime dateEnd);
    }
}

using System;
using TinyEdu.Admin.Domain;
using TinyEdu.Common.Dapper.Repository;

namespace TinyEdu.Admin.Repository
{
    public interface IAPP_SysLogRepository : IRepository
    {
        /// <summary>
        /// 用实体类添加一条记录
        /// </summary>
        /// <param name="entity"></param>
        void AddLog(T_APP_SysLog entity);

        /// <summary>
        /// 根据类型和日期删除日志
        /// </summary>
        /// <param name="DateBegin">开始日期</param>
        /// <param name="DateEnd">结束日期</param>
        /// <returns>记录行数</returns>
        int DeleteLogByDate(string logType, DateTime dateBegin, DateTime dateEnd);
    }
}
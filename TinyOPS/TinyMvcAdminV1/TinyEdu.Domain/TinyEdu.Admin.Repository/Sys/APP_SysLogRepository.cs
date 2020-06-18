using Dapper;
using System;
using TinyEdu.Admin.Domain;
using TinyEdu.Common.Dapper.Entity;
using TinyEdu.Common.Dapper.Repository;

namespace TinyEdu.Admin.Repository
{
    /// <summary>
    /// 记录数据日志Repository
    /// </summary>
    public class APP_SysLogRepository : RepositoryBase, IAPP_SysLogRepository
    {
        /// <summary>
        /// 用实体类添加一条记录
        /// </summary>
        /// <param name="entity"></param>
        public void AddLog(T_APP_SysLog entity)
        {
            try
            {
                entity.SaveType = SaveType.Add;
                base.Add(entity).Commit();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        ///// <summary>
        ///// 根据类型和日期删除日志
        ///// </summary>
        ///// <param name="DateBegin">开始日期</param>
        ///// <param name="DateEnd">结束日期</param>
        ///// <returns>记录行数</returns>
        //public int DeleteLogByDate(string logType, DateTime dateBegin, DateTime dateEnd)
        //{
        //    string strSql = "delete from dbo.APP_SysLog where LogType=@LogType and CreatedDate between @DateBegin and @DateEnd";

        //    var param = new DynamicParameters();
        //    param.Add("@LogType", logType);
        //    param.Add("@DateBegin", dateBegin);
        //    param.Add("@DateEnd", dateEnd);

        //    return base.Execute(strSql, param);
        //}
        public int DeleteLogByDate(string logType, DateTime dateBegin, DateTime dateEnd)
        {
            string strSql = "delete from dbo.APP_SysLog where LogType=@LogType and CreatedDate between @DateBegin and @DateEnd";

            var param = new DynamicParameters();
            param.Add("@LogType", logType);
            param.Add("@DateBegin", dateBegin);
            param.Add("@DateEnd", dateEnd);
            var iCount = (int)ExecuteScalar<int>(strSql, param);
            return iCount;
        }
    }
}
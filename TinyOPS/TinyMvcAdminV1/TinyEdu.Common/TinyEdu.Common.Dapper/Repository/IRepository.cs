using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using TinyEdu.Common.Dapper.Entity;
using TinyEdu.Common.Dapper.Enumeration;
using TinyEdu.Common.Dapper.Persistence.Data;
using TinyEdu.Common.Dapper.Persistence.UnitOfWork;

namespace TinyEdu.Common.Dapper.Repository
{
    public interface IRepository
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="info"></param>
        /// <returns></returns>
        UnitOfWorkResult Add<T>(T info) where T : BaseInfo;
        /// <summary>
        /// 修改
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="info"></param>
        /// <returns></returns>
        UnitOfWorkResult Save<T>(T info) where T : BaseInfo;
        /// <summary>
        /// 移除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="info"></param>
        /// <returns></returns>
        UnitOfWorkResult Remove<T>(T info) where T : BaseInfo;

        /// <summary>
        /// 全部移除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="infos"></param>
        /// <returns></returns>
        UnitOfWorkResult RemoveList<T>(IList<T> infos) where T : BaseInfo;

        /// <summary>
        /// 得到实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="fromCache"></param>
        /// <returns></returns>
        T GetInfo<T>(object key, bool fromCache = true) where T : BaseInfo;

        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="name"></param>
        /// <param name="commandText"></param>
        /// <param name="commandType"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        UnitOfWorkResult ExecuteCommand<T>(EumDBWay dbWay, string dbName, string commandText, CommandType commandType,
            params DataParameterInfo[] parameters) where T : BaseInfo;
        /// <summary>
        /// 执行语句，按实体参数进行
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="commandText"></param>
        /// <param name="paramEntity"></param>
        /// <param name="callback"></param>
        /// <returns></returns>
        UnitOfWorkResult ExecuteCommand<T>(EumDBWay dbWay, string dbName, string commandText, object paramEntity, Action<IList<dynamic>> callback = null) where T : BaseInfo;
        /// <summary>
        /// 查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        IList<T> GetInfos<T>(string query, object parameter);
        /// <summary>
        /// 按页获取对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filter"></param>
        /// <param name="order"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="count"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        IList<T> GetPageInfos<T>(string filter, string order, int pageIndex, int pageSize, out int count, object parameter);
        /// <summary>
        /// 返回页总数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filter"></param>
        /// <param name="order"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        int GetPageCount<T>(string filter, object parameter);
        /// <summary>
        /// 返回第一行第一条
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="paramEntity"></param>
        /// <returns></returns>
        object ExecuteScalar<T>(string commandText, object paramEntity);
    }
}

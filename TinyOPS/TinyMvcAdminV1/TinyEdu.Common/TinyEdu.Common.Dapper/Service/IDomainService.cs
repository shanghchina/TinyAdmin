using System;
using System.Collections.Generic;
using System.Text;
using TinyEdu.Common.Dapper.Entity;
using TinyEdu.Common.Dapper.Persistence.UnitOfWork;

namespace TinyEdu.Common.Dapper.Service
{
    public interface IDomainService
    {
        /// <summary>
        /// 获取原始数据（不含标识映射）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        T GetOriginalInfo<T>(object key) where T : BaseInfo;
        /// <summary>
        /// 按关键字获取数据（含标识映射）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        T GetInfo<T>(object key) where T : BaseInfo;
        /// <summary>
        /// 修改
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="info"></param>
        /// <param name="funRep"></param>
        /// <returns></returns>
        UnitOfWorkResult Save<T>(T info, Func<T, UnitOfWorkResult> funRep = null) where T : BaseInfo;
        /// <summary>
        /// 删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="info"></param>
        /// <param name="funRep"></param>
        /// <returns></returns>
        UnitOfWorkResult Remove<T>(T info, Func<T, UnitOfWorkResult> funRep = null) where T : BaseInfo;
        /// <summary>
        /// 添加
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="info"></param>
        /// <param name="funRep"></param>
        /// <returns></returns>
        UnitOfWorkResult Add<T>(T info, Func<T, UnitOfWorkResult> funRep = null) where T : BaseInfo;
        /// <summary>
        /// 提交
        /// </summary>
        /// <param name="work"></param>
        /// <returns></returns>
        bool Commit(UnitOfWorkResult work);
    }
}

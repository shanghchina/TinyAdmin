using System;
using System.Collections.Generic;
using System.Text;

namespace TinyEdu.Common.Dapper.Persistence.UnitOfWork
{
    public interface IUnitOfWork
    {
        /// <summary>
        /// 执行
        /// </summary>
        void Commit();
        /// <summary>
        /// 是否执行
        /// </summary>
        bool IsExcute { get; set; }
        /// <summary>
        /// 是否释放
        /// </summary>
        bool IsDispose { get; set; }
        /// <summary>
        /// 执行
        /// </summary>
        void Execute();
        /// <summary>
        /// 回滚事务
        /// </summary>
        void Rollback();
        /// <summary>
        /// 关闭连接
        /// </summary>
        void Close();
        /// <summary>
        /// 获取UnitOfWork唯一键，供Context使用产生唯一UnitOfWork
        /// </summary>
        /// <returns></returns>
        string GetKey();
        /// <summary>
        /// 获取当前工作单元序列
        /// </summary>
        /// <returns></returns>
        int GetSequence();
        /// <summary>
        /// 添加信息
        /// </summary>
        /// <param name="info"></param>
        void Add(object info);
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace TinyEdu.Common.Dapper.Persistence.Context
{
    public interface IContext
    {
        /// <summary>
        /// 统一上下文
        /// </summary>
        ContextInfo Local { get; }
        /// <summary>
        /// 从上下文中获取对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        T Get<T>(object key);
        /// <summary>
        /// 缓存对象到当前上下文
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="info"></param>
        /// <param name="key"></param>
        void Set<T>(T entity, object key);
    }
}

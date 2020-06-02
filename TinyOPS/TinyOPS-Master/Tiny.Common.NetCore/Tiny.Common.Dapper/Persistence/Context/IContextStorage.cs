using System;
using System.Collections.Generic;
using System.Text;

namespace Tiny.Common.Dapper.Persistence.Context
{
    public interface IContextStorage
    {
        /// <summary>
        /// 得到上下文
        /// </summary>
        /// <returns></returns>
        ContextInfo Get();
        /// <summary>
        /// 设置上下文
        /// </summary>
        /// <param name="contexnt"></param>
        void Set(ContextInfo contexnt);
    }
}

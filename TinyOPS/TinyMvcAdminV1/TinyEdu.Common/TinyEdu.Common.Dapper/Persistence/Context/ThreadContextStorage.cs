using System;
using System.Collections.Generic;
using System.Text;

namespace TinyEdu.Common.Dapper.Persistence.Context
{
    public class ThreadContextStorage : IContextStorage
    {
        [ThreadStatic]
        private static ContextInfo _context;
        /// <summary>
        /// 得到上下文
        /// </summary>
        /// <returns></returns>
        public virtual ContextInfo Get()
        {
            return _context;
        }
        /// <summary>
        /// 设置上下文
        /// </summary>
        /// <param name="contexnt"></param>
        public virtual void Set(ContextInfo contexnt)
        {
            _context = contexnt;
        }
    }
}

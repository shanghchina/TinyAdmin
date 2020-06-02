using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using OnlyEdu.Common.Dapper.Persistence.UnitOfWork;

namespace OnlyEdu.Common.Dapper.Persistence.Context
{
    public class ContextInfo
    {
        IDictionary<object, object> _entities = new ConcurrentDictionary<object, object>();
        /// <summary>
        /// 实体
        /// </summary>
        public IDictionary<object, object> Entities
        {
            get { return _entities; }
            set { _entities = value; }
        }

        private IList<IUnitOfWork> _unitofworks = new List<IUnitOfWork>();
        /// <summary>
        /// 事务
        /// </summary>
        public IList<IUnitOfWork> UnitOfWorks
        {
            get { return _unitofworks; }
            set { _unitofworks = value; }
        }


        /// <summary>
        /// 是否存在实体
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public virtual bool HasEntity(object key)
        {
            return Entities.ContainsKey(key);
        }

        /// <summary>
        /// 是否存在实体
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public virtual object GetEntity(object key)
        {
            if (Entities.ContainsKey(key))
                return Entities[key];
            return null;
        }
    }
}

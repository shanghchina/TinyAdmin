using System;
using System.Collections.Generic;
using System.Text;
using TinyEdu.Common.Dapper.DI;

namespace TinyEdu.Common.Dapper.Persistence.Context
{
    public class Context : IContext
    {
        public IContextStorage ContextStorage => IoC.Resolve<IContextStorage>();

        public ContextInfo Local
        {
            get
            {
                var rev = ContextStorage.Get();
                if (rev == null)
                {
                    rev = new ContextInfo();
                    ContextStorage.Set(rev);
                }
                return rev;

            }
            set { ContextStorage.Set(value); }
        }

        public T Get<T>(object key)
        {
            if (key == null) return default(T);
            string cacheKey = GetEntityCacheKey(typeof(T), key);
            if (!Local.HasEntity(cacheKey))
                return default(T);
            return (T)Local.GetEntity(cacheKey);
        }


        protected virtual string GetEntityCacheKey(Type type, object key)
        {
            return $"{type.FullName}{key}";
        }

        public void Set<T>(T entity, object key)
        {
            var cacheKey = GetEntityCacheKey(typeof(T), key);
            if (Local.HasEntity(cacheKey))
                Local.Entities[cacheKey] = entity;
            else
                Local.Entities.Add(cacheKey, entity);
        }
    }
}

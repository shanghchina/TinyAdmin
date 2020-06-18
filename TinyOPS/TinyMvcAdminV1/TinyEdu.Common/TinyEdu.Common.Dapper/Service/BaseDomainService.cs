﻿using System;
using System.Collections.Generic;
using System.Text;
using TinyEdu.Common.Dapper.DI;
using TinyEdu.Common.Dapper.Entity;
using TinyEdu.Common.Dapper.Persistence.UnitOfWork;
using TinyEdu.Common.Dapper.Repository;

namespace TinyEdu.Common.Dapper.Service
{
    public class BaseDomainService : MarshalByRefObject, IDomainService
    {
        public virtual IRepository Repository => IoC.Resolve<IRepository>();

        public virtual UnitOfWorkResult Add<T>(T info, Func<T, UnitOfWorkResult> funRep = null) where T : BaseInfo
        {
            return funRep == null ? Repository.Add(info) : funRep(info);
        }

        public virtual UnitOfWorkResult Remove<T>(T info, Func<T, UnitOfWorkResult> funRep = null) where T : BaseInfo
        {
            return funRep == null ? Repository.Remove(info) : funRep(info);
        }

        public virtual UnitOfWorkResult Save<T>(T info, Func<T, UnitOfWorkResult> funRep = null) where T : BaseInfo
        {
            return funRep == null ? Repository.Save(info) : funRep(info);
        }

        public bool Commit(UnitOfWorkResult work)
        {
            try
            {
                if (work == null) return false;
                work.Commit();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public T GetOriginalInfo<T>(object key) where T : BaseInfo
        {
            return Repository.GetInfo<T>(key, false);
        }

        public T GetInfo<T>(object key) where T : BaseInfo
        {
            return Repository.GetInfo<T>(key);
        }
    }
}

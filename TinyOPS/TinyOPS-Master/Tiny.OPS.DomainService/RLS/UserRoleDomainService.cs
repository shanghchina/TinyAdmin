using Tiny.Common.Dapper.DI;
using Tiny.Common.Dapper.Service;
using Tiny.OPS.Domain;
using Tiny.OPS.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Tiny.OPS.DomainService
{
    public class UserRoleDomainService : RealDomainService<T_RLS_UserRole>, IUserRoleDomainService
    {
        public IUserRoleRepository UserRoleRepository => IoC.Resolve<IUserRoleRepository>();
        public bool RemoveByUserId(long userId)
        {
            return UserRoleRepository.RemoveByUserId(userId);
        }
    }
}

using Tiny.Common.Dapper.DI;
using Tiny.Common.Dapper.Service;
using Tiny.OPS.Domain;
using Tiny.OPS.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
namespace Tiny.OPS.DomainService
{
    public class UserRoleRangeDomainService : RealDomainService<T_RLS_UserRoleRange>, IUserRoleRangeDomainService
    {
        public IUserRoleRangeRepository UserRoleRangeRepository => IoC.Resolve<IUserRoleRangeRepository>();
    }
}

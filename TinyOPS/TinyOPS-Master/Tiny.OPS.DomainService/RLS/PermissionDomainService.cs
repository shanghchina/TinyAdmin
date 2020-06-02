using Tiny.Common.Dapper.DI;
using Tiny.Common.Dapper.Service;
using Tiny.OPS.Domain;
using Tiny.OPS.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Tiny.OPS.DomainService
{
    public class PermissionDomainService : RealDomainService<T_RLS_Permission>, IPermissionDomainService
    {
        public IPermissionRepository PermissionRepository => IoC.Resolve<IPermissionRepository>();
        public T_RLS_Permission GetInfoByPermissionsID(long permissionsID)
        {
            return PermissionRepository.GetInfoByPermissionsID(permissionsID);
        }
        public List<T_RLS_Permission> GetByUserId(Guid userId)
        {
            return PermissionRepository.GetByUserId(userId).ToList();
        }
    }
}

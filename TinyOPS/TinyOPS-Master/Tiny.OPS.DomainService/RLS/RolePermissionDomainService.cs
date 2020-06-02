using Tiny.Common.Dapper.DI;
using Tiny.Common.Dapper.Service;
using Tiny.OPS.Domain;
using Tiny.OPS.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Tiny.OPS.DomainService
{
    public class RolePermissionDomainService : RealDomainService<T_RLS_RolePermission>, IRolePermissionDomainService
    {
        public IRolePermissionRepository RolePermissionRepository => IoC.Resolve<IRolePermissionRepository>();
        public List<T_RLS_RolePermission> GetByRoleId(long roleId)
        {
            return RolePermissionRepository.GetByRoleId(roleId).ToList();
        }
        public bool RemoveByRoleId(long roleId) {
            return RolePermissionRepository.RemoveByRoleId(roleId);
        }
        public List<T_RLS_RolePermission> GetByUserId(Guid userId)
        {
            return RolePermissionRepository.GetByUserId(userId).ToList();
        }
        public List<T_RLS_RolePermission> GetByUserIdFillNav(Guid userId) {
            return RolePermissionRepository.GetByUserIdFillNav(userId).ToList();
        }
    }
}

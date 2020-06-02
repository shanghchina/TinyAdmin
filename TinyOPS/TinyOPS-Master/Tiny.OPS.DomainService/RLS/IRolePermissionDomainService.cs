using Tiny.Common.Dapper.Service;
using Tiny.OPS.Domain;
using System;
using System.Collections.Generic;

namespace Tiny.OPS.DomainService
{
    public interface IRolePermissionDomainService : IDomainService
    {
        List<T_RLS_RolePermission> GetByRoleId(long roleId);
        bool RemoveByRoleId(long roleId);
        List<T_RLS_RolePermission> GetByUserId(Guid userId);
        List<T_RLS_RolePermission> GetByUserIdFillNav(Guid userId);
    }
}

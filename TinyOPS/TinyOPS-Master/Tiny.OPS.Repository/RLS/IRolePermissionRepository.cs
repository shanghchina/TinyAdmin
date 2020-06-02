using Tiny.OPS.Domain;
using System;
using System.Collections.Generic;

namespace Tiny.OPS.Repository
{
    public interface IRolePermissionRepository
    {
        IList<T_RLS_RolePermission> GetByRoleId(long roleId);
        bool RemoveByRoleId(long roleId);
        IList<T_RLS_RolePermission> GetByUserId(Guid userId);
        IList<T_RLS_RolePermission> GetByUserIdFillNav(Guid userId);
    }
}

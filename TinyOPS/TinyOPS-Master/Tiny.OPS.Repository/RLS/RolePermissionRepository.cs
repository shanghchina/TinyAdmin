using Dapper;
using Tiny.Common.Dapper.Entity;
using Tiny.Common.Dapper.Enumeration;
using Tiny.Common.Dapper.Repository;
using Tiny.Common.Types;
using Tiny.OPS.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tiny.OPS.Repository
{
    public class RolePermissionRepository : RepositoryBase, IRolePermissionRepository
    {
        public IList<T_RLS_RolePermission> GetByRoleId(long roleId)
        {
            return GetInfos<T_RLS_RolePermission>("select * from T_RLS_RolePermission where RoleID=@RoleId", new { RoleId = roleId });
        }
        public bool RemoveByRoleId(long roleId)
        {
            var _agentresult = ExecuteCommand<T_RLS_RolePermission>(EumDBWay.Writer, EumDBName.POC.GetDisplayName()," delete from T_RLS_RolePermission where RoleID=@RoleId ", new { RoleId = roleId });
            if (_agentresult.IsSuccess)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public IList<T_RLS_RolePermission> GetByUserId(Guid userId)
        {
            return GetInfos<T_RLS_RolePermission>(@"Select * From T_RLS_RolePermission rp 
                                                Where Exists 
                                                (select 1 from T_RLS_UserRole ur 
                                                    where rp.RoleID=ur.RoleID and ur.UserGuid=@userId and rp.AuthorizationStateID=@stateId)"
                                                , new { userId = userId, stateId = AuthorizationStateType.Authorization });

        }
        public IList<T_RLS_RolePermission> GetByUserIdFillNav(Guid userId)
        {
            //return GetInfos<RolePermission>("Select * From T_RLS_RolePermissions rp Where Exists (select 1 from T_RLS_UserRole ur where rp.RoleID=ur.RoleID and ur.UserGuid=userId)", new { userId = userId });
            var _Result = new Dictionary<long, T_RLS_RolePermission>();
            GetInfos<T_RLS_RolePermission, T_RLS_UserRole, T_RLS_Permission, T_RLS_RolePermission>
                (@"Select * from T_RLS_RolePermission rp
                    Inner Join T_RLS_UserRole ur on rp.RoleID=ur.RoleID
                    Inner Join T_RLS_Permission p on rp.PermissionsID=p.PermissionsID
                    Where ur.UserGuid=@userId and rp.AuthorizationStateID=@stateId",
                (rp, ur, p) =>
                {
                    T_RLS_RolePermission rolePermission;
                    if (!_Result.TryGetValue(rp.Id, out rolePermission))
                    {
                        rp.Permission = p;
                        _Result.Add(rp.Id, rolePermission = rp);
                    }
                    return rp;
                }, new { userId = userId, stateId = AuthorizationStateType.Authorization });
            return _Result.Values.ToList();

        }
    }
}

using Dapper;
using Tiny.Common.Dapper.Entity;
using Tiny.Common.Dapper.Repository;
using Tiny.Common.Types;
using Tiny.OPS.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Tiny.OPS.Repository
{
    public class PermissionRepository : RepositoryBase, IPermissionRepository
    {
        public T_RLS_Permission GetInfoByPermissionsID(long permissionsID)
        {
            return GetInfos<T_RLS_Permission>(@"select * from T_RLS_Permission where PermissionsID=@PermissionsID", new { PermissionsID = permissionsID }).FirstOrDefault();
        }
        public IList<T_RLS_Permission> GetByUserId(Guid userId)
        {
            return GetInfos<T_RLS_Permission>(@"Select Distinct p.*  From T_RLS_Permission p
                    Left Join T_RLS_RolePermission rp on p.PermissionsID=rp.PermissionsID
                    Left Join T_RLS_UserRole ur on rp.RoleID=ur.RoleID
                    Where 1=1
                    and ur.UserGuid=@userId 
                    and p.RowStateID=@state", new { userId = userId, state = RowStateType.Effectivity });
        }
    }
}

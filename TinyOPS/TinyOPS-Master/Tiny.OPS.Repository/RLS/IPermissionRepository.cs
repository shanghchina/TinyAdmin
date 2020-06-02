using Tiny.OPS.Domain;
using System;
using System.Collections.Generic;

namespace Tiny.OPS.Repository
{
    public interface IPermissionRepository
    {
        T_RLS_Permission GetInfoByPermissionsID(long permissionsID);
        IList<T_RLS_Permission> GetByUserId(Guid userId);


    }
}

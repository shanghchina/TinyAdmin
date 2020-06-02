using Tiny.Common.Dapper.Service;
using Tiny.OPS.Domain;
using System;
using System.Collections.Generic;

namespace Tiny.OPS.DomainService
{
    public interface IPermissionDomainService : IDomainService
    {
        T_RLS_Permission GetInfoByPermissionsID(long permissionsID);
        List<T_RLS_Permission> GetByUserId(Guid userId);
    }
}

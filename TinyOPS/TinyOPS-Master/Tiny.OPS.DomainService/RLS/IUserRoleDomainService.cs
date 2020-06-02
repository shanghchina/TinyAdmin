using Tiny.Common.Dapper.Service;
using Tiny.OPS.Domain;
using System;
using System.Collections.Generic;
namespace Tiny.OPS.DomainService
{
    public interface IUserRoleDomainService : IDomainService
    {
        bool RemoveByUserId(long userId);
    }
}

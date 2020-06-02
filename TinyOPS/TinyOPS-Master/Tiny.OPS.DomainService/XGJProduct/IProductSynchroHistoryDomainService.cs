using Tiny.Common.Dapper.Service;
using Tiny.OPS.Domain.Enum;
using Tiny.OPS.Domain.XGJProduct;
using System;

namespace Tiny.OPS.DomainService
{
    public interface IProductSynchroHistoryDomainService : IDomainService
    {
        T_EXT_SyncHistory GetLatestHistory(int from, Guid org, SynchroDataType type);
    }
}

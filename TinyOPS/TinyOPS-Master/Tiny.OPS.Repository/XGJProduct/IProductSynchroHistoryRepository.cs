using Tiny.Common.Dapper.Repository;
using Tiny.OPS.Domain.Enum;
using Tiny.OPS.Domain.XGJProduct;
using System;
using System.Collections.Generic;

namespace Tiny.OPS.Repository
{
    public interface IProductSynchroHistoryRepository : IRepository
    {
        IList<T_EXT_SyncHistory> GetSynchroHistory(int from, Guid org, SynchroDataType type);
    }
}

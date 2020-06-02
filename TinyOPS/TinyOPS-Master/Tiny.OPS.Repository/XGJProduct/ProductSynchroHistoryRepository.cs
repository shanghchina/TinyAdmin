using Tiny.Common.Dapper.Repository;
using Tiny.OPS.Domain.Enum;
using Tiny.OPS.Domain.XGJProduct;
using System;
using System.Collections.Generic;

namespace Tiny.OPS.Repository
{
    public class ProductSynchroHistoryRepository : RepositoryBase, IProductSynchroHistoryRepository
    {
        public IList<T_EXT_SyncHistory> GetSynchroHistory(int from, Guid org, SynchroDataType type)
        {
            return GetInfos<T_EXT_SyncHistory>("select top 1 * from T_EXT_SyncHistory where FromSystem=@from and TeachLevelOneOrgID=@org and DataType=@type and SyncStatus=1000 order by id desc", new { from = @from, org = @org, type = @type });
        }
    }
}

using Tiny.Common.Dapper.DI;
using Tiny.Common.Dapper.Service;
using Tiny.OPS.Domain.Enum;
using Tiny.OPS.Domain.XGJProduct;
using Tiny.OPS.Repository;
using System;
using System.Linq;

namespace Tiny.OPS.DomainService
{
    public class ProductSynchroHistoryDomainService : RealDomainService<T_EXT_SyncHistory>, IProductSynchroHistoryDomainService
    {
        public IProductSynchroHistoryRepository ProductSynchroHistoryRepository => IoC.Resolve<IProductSynchroHistoryRepository>();

        public T_EXT_SyncHistory GetLatestHistory(int from, Guid org, SynchroDataType type)
        {
            return ProductSynchroHistoryRepository.GetSynchroHistory(from, org, type).LastOrDefault();
        }
    }
}

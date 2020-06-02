using Tiny.Common.Dapper.DI;
using Tiny.Common.Dapper.Persistence.UnitOfWork;
using Tiny.Common.Dapper.Service;
using Tiny.OPS.Contract;
using Tiny.OPS.Domain;
using Tiny.OPS.Repository;
using System;

namespace Tiny.OPS.DomainService
{
    public class T_POC_ProductTypeMapDomainService : RealDomainService<T_POC_ProductTypeMap>, IT_POC_ProductTypeMapDomainService
    {
        private IT_POC_ProductTypeMapRepository ProductTypeMapRepository => IoC.Resolve<IT_POC_ProductTypeMapRepository>();
        public bool AddOrUpdateProductTypeMap(AddProductTypeMapRequest request)
        {
            var productTypeMap = ProductTypeMapRepository.GetProductTypeMapByGuid(request.ProductTypeMapGuid);
            if (productTypeMap == null)
            {
                productTypeMap = new T_POC_ProductTypeMap();
                productTypeMap.ProductTypeMapGuid = Guid.NewGuid();
            }
            productTypeMap.FKItemTypeId = request.FKItemTypeId;
            productTypeMap.FKProductTypeGuid = request.FKProductTypeGuid;
            productTypeMap.ProductTypeLevelNo = request.ProductTypeLevelNo;
            productTypeMap.ProductTypeID = request.ProductTypeID;
            productTypeMap.ProductTypeTitle = request.ProductTypeTitle;
            productTypeMap.UpdaterUserId = request.UpdaterUserId;
            productTypeMap.UpdaterUserName = request.UpdaterUserName;
            productTypeMap.UpdateDate = request.CreatedDate;
            if (productTypeMap.Id == 0)
            {
                Add(productTypeMap);
                productTypeMap.CreatedDate = request.CreatedDate;
            }  
            else
                Save(productTypeMap);
            var _unit = UnitOfWorkResult.GetCurrentUow();
            if (_unit.IsSuccess)
            {
                _unit.Commit();
                return true;
            }
            else
                return false;
        }
    }
}


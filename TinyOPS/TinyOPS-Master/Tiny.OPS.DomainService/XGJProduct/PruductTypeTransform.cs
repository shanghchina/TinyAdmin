using Newtonsoft.Json;
using Tiny.Common.Dapper.DI;
using Tiny.OPS.Contract.XGJ;
using Tiny.OPS.Domain.XGJProduct;
using System;

namespace Tiny.OPS.DomainService
{
    public class PruductTypeTransform : ITransform
    {
        private IProductItemTypeDomainService ProductItemTypeDomainService => IoC.Resolve<IProductItemTypeDomainService>();
        public void ExecuteTrans(T_EXT_SyncHistory entity)
        {
            var _responses = JsonConvert.DeserializeObject<ReturnResponse<ProductTypeResponse>>(entity.DataJson);
            foreach (var response in _responses.Data.List)
            {
                var _result = ProductItemTypeDomainService.GetInfoByGuid(entity.FromSystem, response.ID.ToString());
                if (_result == null)
                    _result = new T_EXT_ItemType();
                _result.FromSystem = entity.FromSystem;
                _result.TeachLevelOneOrgID = response.OrgID;
                _result.TeachLevelOneOrgName = response.OrgName;
                _result.ProductTypeID = response.ID.ToString();
                _result.ProductTypeName = response.Name;
                _result.NodeFlag = response.ParentID == Guid.Empty ? 1 : 2;
                _result.ParentProductTypeID = response.ParentID.ToString();
                _result.ItemTypeStatus = response.Status;
                _result.ProductCreatedDate = response.CreateTime;
                _result.ProductUpdateDate = response.UpdateTime;
                _result.Describe = response.Describe;
                _result.POCSource = entity.POCSource;
                _result.History = entity;
                if (_result.Id == 0)
                    ProductItemTypeDomainService.Add(_result);
                else
                    ProductItemTypeDomainService.Save(_result);
            }
        }
    }
}

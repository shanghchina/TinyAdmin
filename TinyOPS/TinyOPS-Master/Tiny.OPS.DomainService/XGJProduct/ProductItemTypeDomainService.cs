using Tiny.Common.Dapper.DI;
using Tiny.Common.Dapper.Service;
using Tiny.OPS.Contract;
using Tiny.OPS.Domain.XGJProduct;
using Tiny.OPS.Repository;

namespace Tiny.OPS.DomainService
{
    public class ProductItemTypeDomainService : RealDomainService<T_EXT_ItemType>, IProductItemTypeDomainService
    {
        private IProductItemTypeRepository ProductItemTypeRepository => IoC.Resolve<IProductItemTypeRepository>();

        public T_EXT_ItemType GetInfoByGuid(int from, string guid)
        {
            return ProductItemTypeRepository.GetInfoByGuid(from, guid);
        }

        public ProductTypePageInfoResponse GetPageInfoList(ProductTypeMapRequest request)
        {
            var response = new ProductTypePageInfoResponse();
            response.DataList = ProductItemTypeRepository.GetPageInfoList(request, out int total);
            response.Total = total;
            return response;
        }

        public ExtItemTypeResponse GetProductItemTypeList(ExtItemTypeRequest request)
        {
            var response = new ExtItemTypeResponse();
            response.DataList = ProductItemTypeRepository.GetProductItemTypeList(request, out int total);
            response.Total = total;
            return response;
        }

        /// <summary>
        /// 判断来源系统的所有产品类别是否映射
        /// </summary>
        /// <returns></returns>
        public bool IsAllMapping()
        {
            return ProductItemTypeRepository.IsAllMapping();

        }
    }
}

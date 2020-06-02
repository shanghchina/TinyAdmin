using Tiny.Common.Dapper.Service;
using Tiny.OPS.Contract;
using Tiny.OPS.Domain.XGJProduct;

namespace Tiny.OPS.DomainService
{
    public interface IProductItemTypeDomainService : IDomainService
    {
        T_EXT_ItemType GetInfoByGuid(int from, string guid);

        ProductTypePageInfoResponse GetPageInfoList(ProductTypeMapRequest request);


        ExtItemTypeResponse GetProductItemTypeList(ExtItemTypeRequest request);

        /// <summary>
        /// 判断来源系统的所有产品类别是否映射
        /// </summary>
        /// <returns></returns>
        bool IsAllMapping();
    }
}

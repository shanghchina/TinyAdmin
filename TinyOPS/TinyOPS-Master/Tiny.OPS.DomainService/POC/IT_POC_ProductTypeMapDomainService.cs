using Tiny.Common.Dapper.Service;
using Tiny.OPS.Contract;
using Tiny.OPS.Domain;


namespace Tiny.OPS.DomainService
{
    /// <summary>
    /// 产品分类映射
    /// </summary>
	public interface IT_POC_ProductTypeMapDomainService : IDomainService
    {
        /// <summary>
        /// 新增或修改产品分类映射
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        bool AddOrUpdateProductTypeMap(AddProductTypeMapRequest request);
    }
}


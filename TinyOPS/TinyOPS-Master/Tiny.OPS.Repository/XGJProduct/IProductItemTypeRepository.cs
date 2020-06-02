using Tiny.Common.Dapper.Repository;
using Tiny.OPS.Contract;
using Tiny.OPS.Domain.XGJProduct;
using System;
using System.Collections.Generic;

namespace Tiny.OPS.Repository
{
    public interface IProductItemTypeRepository : IRepository
    {
        T_EXT_ItemType GetInfoByGuid(int from, string guid);

        List<ProductTypePageInfo> GetPageInfoList(ProductTypeMapRequest request, out int total);

        /// <summary>
        /// 获取POC产品分类映射的校管家产品分类
        /// </summary>
        /// <param name="request"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        List<ProductTypeMapping> GetProductTypeList(ProductTypeMappingListRequest request, out int total);

        /// <summary>
        /// 获取想校管家产品分类
        /// </summary>
        /// <param name="request"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        List<ItemType> GetProductItemTypeList(ExtItemTypeRequest request, out int total);

        /// <summary>
        /// 判断来源系统的所有产品类别是否映射
        /// </summary>
        /// <returns></returns>
        bool IsAllMapping();
    }
}

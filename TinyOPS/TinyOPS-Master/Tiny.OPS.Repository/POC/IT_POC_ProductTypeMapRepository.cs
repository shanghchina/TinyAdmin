using Tiny.Common.Dapper.Repository;
using Tiny.OPS.Contract;
using Tiny.OPS.Domain;
using System;
using System.Collections.Generic;

namespace Tiny.OPS.Repository
{
    /// <summary>
    /// 产品分类映射表
    /// </summary>
	public interface IT_POC_ProductTypeMapRepository : IRepository
    {
        T_POC_ProductTypeMap GetProductTypeMapByGuid(Guid guid);
        /// <summary>
        /// 产品类别是否被映射
        /// </summary>
        /// <param name="ProductTypeGuid"></param>
        /// <returns></returns>
        bool HasMappingProductType(Guid productTypeGuid);

       /// <summary>
       ///根据产品池的产品类型获取映射关系
       /// </summary>
       /// <param name="productTypeGuid"></param>
       /// <returns></returns>
        T_POC_ProductTypeMap GetProductTypeMapByProductTypeID(string productTypeGuid);
    }
}


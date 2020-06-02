using Tiny.Common.Dapper.Service;
using Tiny.OPS.Contract;
using Tiny.OPS.Domain;
using System;
using System.Collections.Generic;

namespace Tiny.OPS.DomainService
{
    /// <summary>
    /// 产品分类
    /// </summary>
	public interface IT_POC_ProductTypeDomainService : IDomainService
    {
        /// <summary>
        /// 获取父级产品类别下拉框选项
        /// </summary>
        /// <returns></returns>
        List<T_POC_ProductType> GetLevelOneProductType();

        /// <summary>
        /// 获取子级产品类别下拉框选项
        /// </summary>
        /// <param name="productTypeGuid"></param>
        /// <returns></returns>
        List<T_POC_ProductType> GetChildProductType(Guid productTypeGuid);

        /// <summary>
        /// 获取多级产品分类
        /// </summary>
        /// <returns></returns>
        List<ProductTypelevelResponse> GetProductTypelevel();

        /// <summary>
        /// 获取产品分类列表(递归不分页)
        /// </summary>
        /// <returns></returns>
        List<ProductTypeLevelAllInfoResponse> GetProductTypeList();

        /// <summary>
        /// 操作产品分类
        /// </summary>
        void OperateProductType(T_POC_ProductType request);

        /// <summary>
        /// 删除产品分类
        /// </summary>
        /// <param name="productTypeId"></param>
        /// <returns></returns>
        bool DeleteProductType(Guid productTypeId);

        /// <summary>
        /// 查看产品类别映射关系
        /// </summary>
        /// <param name="productTypeGuid">产品类别Guid</param>
        /// <returns></returns>
        ProductTypeMappingListResponse SearchProductTypeList(ProductTypeMappingListRequest request);

        /// <summary>
        /// 产品分类启用开关
        /// </summary>
        /// <param name="request"></param>
        void OperateSwitch(OperateSwitchRequest request);
    }
}


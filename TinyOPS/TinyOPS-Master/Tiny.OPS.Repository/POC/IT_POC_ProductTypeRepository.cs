using Tiny.Common.Dapper.Repository;
using Tiny.OPS.Contract;
using Tiny.OPS.Domain;
using System;
using System.Collections.Generic;

namespace Tiny.OPS.Repository
{
    /// <summary>
    /// 产品分类
    /// </summary>
	public interface IT_POC_ProductTypeRepository : IRepository
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
        /// 获取产品分类所有信息
        /// </summary>
        /// <returns></returns>
        List<T_POC_ProductType> GetProductType();

        /// <summary>
        /// 新增产品分类
        /// </summary>
        /// <param name="entity"></param>

        void AddProductType(T_POC_ProductType entity);
        /// <summary>
        /// 修改产品分类
        /// </summary>
        /// <param name="entity"></param>

        void UpdateProductType(T_POC_ProductType entity);
        /// <summary>
        /// 删除产品分类
        /// </summary>
        /// <param name="entity"></param>

        void DeleteProductType(Guid productTypeId);
        /// <summary>
        /// 是否存在下级产品分类
        /// </summary>
        /// <returns></returns>
        bool HasProductTypeChildren(Guid productTypeGuid);

        /// <summary>
        /// 产品分类启用开关
        /// </summary>
        /// <param name="request"></param>
        void OperateSwitch(OperateSwitchRequest request);

        

    }
}


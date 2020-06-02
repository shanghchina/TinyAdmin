using Dapper;
using Tiny.Common.Dapper.Enumeration;
using Tiny.Common.Dapper.Repository;
using Tiny.OPS.Domain;
using System;
using System.Linq;
using System.Text;

namespace Tiny.OPS.Repository
{
    /// <summary>
    /// 产品分类映射表
    /// </summary>
	public class T_POC_ProductTypeMapRepository : RepositoryBase, IT_POC_ProductTypeMapRepository
    {
        public T_POC_ProductTypeMap GetProductTypeMapByGuid(Guid guid)
        {
            return GetInfos<T_POC_ProductTypeMap>("select * from T_POC_ProductTypeMap where ProductTypeMapGuid = @guid", new { guid = guid }).FirstOrDefault();
        }
        /// <summary>
        /// 产品类别是否被映射
        /// </summary>
        /// <param name="ProductTypeGuid"></param>
        /// <returns></returns>
        public bool HasMappingProductType(Guid productTypeGuid)
        {
            StringBuilder _query = new StringBuilder();
            _query.AppendFormat(@"(SELECT * FROM [dbo].[T_POC_ProductTypeMap] where FKProductTypeGuid=@ProductTypeGuid)");
            var _parameters = new DynamicParameters();
            _parameters.Add("ProductTypeGuid", productTypeGuid);
            int total = GetInfos<int>(EumDBName.POC, string.Format("SELECT COUNT(1) FROM  {0} h", _query), _parameters).First();
            if (total > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        ///根据产品池的产品类型获取映射关系
        /// </summary>
        /// <param name="productTypeGuid"></param>
        /// <returns></returns>
        public T_POC_ProductTypeMap GetProductTypeMapByProductTypeID(string productTypeGuid)
        {
            return GetInfos<T_POC_ProductTypeMap>("select * from T_POC_ProductTypeMap where ProductTypeID = @ProductTypeID", new { ProductTypeID = productTypeGuid }).FirstOrDefault();
        }



    }
}


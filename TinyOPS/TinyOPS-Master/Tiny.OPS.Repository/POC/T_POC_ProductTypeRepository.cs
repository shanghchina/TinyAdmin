using Dapper;
using Tiny.Common.Dapper.Enumeration;
using Tiny.Common.Dapper.Repository;
using Tiny.OPS.Common;
using Tiny.OPS.Contract;
using Tiny.OPS.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Tiny.OPS.Repository
{
    /// <summary>
    /// 产品分类
    /// </summary>
	public class T_POC_ProductTypeRepository : RepositoryBase, IT_POC_ProductTypeRepository
    {
        public void AddProductType(T_POC_ProductType entity)
        {
            try
            {
                Add<T_POC_ProductType>(entity).Commit();
            }
            catch (Exception)
            {
                throw;

            }
        }

        /// <summary>
        /// 获取子级产品类别下拉框选项
        /// </summary>
        /// <param name="productTypeGuid"></param>
        /// <returns></returns>
        public List<T_POC_ProductType> GetChildProductType(Guid productTypeGuid)
        {
            try
            {
                StringBuilder _sql = new StringBuilder(@"SELECT * FROM [dbo].[T_POC_ProductType] WHERE 1=1 ");
                //查询条件
                StringBuilder _condition = new StringBuilder("");
                var _parameters = new DynamicParameters();
                //父级Id
                if (productTypeGuid.ToString() != "00000000-0000-0000-0000-000000000000")
                {
                    _condition.AppendFormat(" AND ParentGuid = @ParentGuid  ");
                    _parameters.Add("@ParentGuid", productTypeGuid);
                }
                return GetInfos<T_POC_ProductType>(_sql.ToString(), _condition).ToList();
            }
            catch (Exception ex)
            {

                _Log4Net.Error("T_POC_ProductTypeRepository下方法GetChildProductType异常：" + ex.Message);
                return null;
            }

        }

        /// <summary>
        /// 获取父级产品类别下拉框选项
        /// </summary>
        /// <returns></returns>
        public List<T_POC_ProductType> GetLevelOneProductType()
        {
            try
            {
                string _sql = @"SELECT * FROM [dbo].[T_POC_ProductType] WHERE ParentGuid='00000000-0000-0000-0000-000000000000'";
                return GetInfos<T_POC_ProductType>(_sql.ToString(), null).ToList();
            }
            catch (Exception ex)
            {
                _Log4Net.Error("T_POC_ProductTypeRepository下方法GetLevelOneProductType异常：" + ex.Message);
                return null;
            }
        }

        public List<T_POC_ProductType> GetProductType()
        {
            string _sql = @"SELECT * FROM [dbo].[T_POC_ProductType]";
            return GetInfos<T_POC_ProductType>(_sql.ToString(), null).ToList();
        }

        public bool HasProductTypeChildren(Guid productTypeGuid)
        {
            StringBuilder _query = new StringBuilder();
            _query.AppendFormat(@"(SELECT * FROM [dbo].[T_POC_ProductType] where ParentGuid=@ProductTypeGuid)");
            var _parameters = new DynamicParameters();
            _parameters.Add("ProductTypeGuid", productTypeGuid);
            int total = GetInfos<int>(EumDBName.POC, string.Format("SELECT COUNT(1) FROM  {0} h ", _query), _parameters).First();
            if (total > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void DeleteProductType(Guid productTypeId)
        {
            try
            {
                string _sql = @"delete from  [dbo].[T_POC_ProductType]  WHERE ProductTypeGuid=@ProductTypeGuid";
                var _parameters = new DynamicParameters();
                _parameters.Add("@ProductTypeGuid", productTypeId);
                ExecuteCommand<T_POC_ProductType>(EumDBWay.Reader, "POC", _sql.ToString(), _parameters).Commit();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void UpdateProductType(T_POC_ProductType entity)
        {
            try
            {
                Save<T_POC_ProductType>(entity).Commit();
            }
            catch (Exception)
            {
                throw;

            }
        }
        /// <summary>
        /// 产品分类启用开关
        /// </summary>
        /// <param name="request"></param>
        public void OperateSwitch(OperateSwitchRequest request)
        {
            try
            {
                string _sql = @"update [dbo].[T_POC_ProductType] set IsEnabled=@IsEnabled WHERE ProductTypeGuid=@ProductTypeGuid";
                var _parameters = new DynamicParameters();
                _parameters.Add("@IsEnabled", request.IsEnabled);
                _parameters.Add("@ProductTypeGuid", request.ProductTypeGuid);
                ExecuteCommand<T_POC_ProductType>(EumDBWay.Writer, "POC", _sql.ToString(), _parameters).Commit();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}


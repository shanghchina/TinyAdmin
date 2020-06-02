using Dapper;
using Tiny.Common.Dapper.Enumeration;
using Tiny.Common.Dapper.Repository;
using Tiny.OPS.Common;
using Tiny.OPS.Contract;
using Tiny.OPS.Domain.XGJProduct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tiny.OPS.Repository
{
    public class ProductItemTypeRepository : RepositoryBase, IProductItemTypeRepository
    {
        public T_EXT_ItemType GetInfoByGuid(int from, string guid)
        {
            return GetInfos<T_EXT_ItemType>(@"select * from [T_EXT_ItemType] where ProductTypeID=@guid and FromSystem=@from", new { guid = @guid, from = @from }).FirstOrDefault();
        }

        public List<ProductTypePageInfo> GetPageInfoList(ProductTypeMapRequest request, out int total)
        {
            StringBuilder _query = new StringBuilder();
            _query.Append(@"select a.Id,c.SysDictValue PocSource,a.TeachLevelOneOrgName LevelOneOrgName,a.ProductTypeName,a.ProductTypeID,a.NodeFlag,
                            case when b.Id is null then '否' else '是' end IsMapping,b.ProductTypeMapGuid,b.UpdateDate MappingDateDate ,
                            STUFF((SELECT '>'+ltrim(ProductTypeName) FROM T_POC_ProductType  where CAST(ProductTypeGuid as varchar(36))  in (select Code from  [dbo].[f_split](b.ProductTypeTitle,',') )
                            FOR XML PATH(''),TYPE).value('.','nvarchar(max)'), 1, 1, '')+'|'+b.ProductTypeTitle ProductTypeTitle
                            from T_EXT_ItemType a
                            left join T_POC_ProductTypeMap b on b.FKItemTypeId = a.id
                            left join [T_SYS_Dictionary] c on c.SysDictKey = cast(a.POCSource as nvarchar)");

            var _parameters = new DynamicParameters();
            var _where = new StringBuilder(" where 1 = 1");

            if (request.PocSource?.Count > 0)
            {
                _where.Append(" and a.POCSource in @Pocsource");
                _parameters.Add("@Pocsource", request.PocSource.ToArray());
            }
            if (request.LevelOneOrgID?.Count > 0)
            {
                _where.Append(" and a.TeachLevelOneOrgID in @OneOrgId");
                _parameters.Add("@OneOrgId", request.LevelOneOrgID.ToArray());
            }
            if (!string.IsNullOrWhiteSpace(request.IsMapping))
            {
                if (request.IsMapping.Equals("0"))
                    _where.Append(" and b.Id is null");
                if (request.IsMapping.Equals("1"))
                    _where.Append(" and b.Id is not null");
            }


            var countsql = @"select count(1) from T_EXT_ItemType a
                            left join T_POC_ProductTypeMap b on b.FKItemTypeId = a.id
                            left join [T_SYS_Dictionary] c on c.SysDictKey = cast(a.POCSource as nvarchar)" + _where.ToString();

            total = GetInfos<int>(EumDBName.POC, countsql, _parameters).First();

            return GetPageInfos<ProductTypePageInfo>(EumDBName.POC, _query.ToString() + _where.ToString(), "a.Id ASC", request.PageIndex, request.PageSize, _parameters).ToList();
        }

        public List<ItemType> GetProductItemTypeList(ExtItemTypeRequest request, out int total)
        {
            try
            {
                StringBuilder _query = new StringBuilder();
                _query.AppendFormat(@"(select b.SysDictValue as PocSourceName,a.TeachLevelOneOrgName,a.ProductTypeName,a.NodeFlag,a.Id,
                                       case when a.ItemTypeStatus=0 then '无效' else '有效' end  as ItemTypeStatus,
                                       a.ProductCreatedDate,a.ProductUpdateDate from  T_EXT_ItemType a 
                                       inner join T_SYS_Dictionary b on cast(a.Pocsource as nvarchar)=b.SysDictKey where 1=1 ");
                var _parameters = new DynamicParameters();
                if (request.Source?.Count > 0)
                {
                    _query.AppendFormat(" and a.Pocsource in @Pocsource");
                    _parameters.Add("@Pocsource", request.Source.ToArray());
                }
                if (request.LevelOneOrgID?.Count > 0)
                {
                    _query.AppendFormat(" and a.TeachLevelOneOrgID in @OneOrgId");
                    _parameters.Add("@OneOrgId", request.LevelOneOrgID.ToArray());
                }
                if (request.IsEffect == "0")
                {
                    _query.AppendFormat(" and a.ItemTypeStatus =0 ");
                }
                if (request.IsEffect == "1")
                {
                    _query.AppendFormat(" and a.ItemTypeStatus =1");

                }
                if (!string.IsNullOrEmpty(request.ProductItemName))
                {
                    var signs = new List<string> { "'", "[", "%", "_" };
                    _query.AppendFormat(" and a.ProductTypeName like @ProductTypeName");
                    _parameters.Add("@ProductTypeName", $"%{request.ProductItemName.ReplaceBySigns(signs)}%");
                }
                _query.AppendFormat(" ) h");
                //总数
                total = GetInfos<int>(EumDBName.POC, string.Format("SELECT COUNT(1) FROM  {0} ", _query), _parameters).First();
                //分页
                var _queryPage = string.Format(@"select                PocSourceName,
                                                                       TeachLevelOneOrgName ,
                                                                       ProductTypeName ,
                                                                       NodeFlag ,
                                                                       ItemTypeStatus ,
                                                                       ProductCreatedDate as CreatedDate,ProductUpdateDate as UpdateDate
                                                                      from (select ROW_NUMBER() OVER(ORDER BY h.Id desc) as RowNum,h.* from {0}) as RowNumberTable", _query);
                if (request.Pagination)
                {
                    _queryPage += " where RowNum between @rowbegin and @rowend  ";
                    _parameters.Add("@rowbegin", (request.PageIndex - 1) * request.PageSize + 1);
                    _parameters.Add("@rowend", request.PageIndex * request.PageSize);
                }
                var list = GetInfos<ItemType>(EumDBName.POC, _queryPage.ToString(), _parameters).ToList();
                return list;
            }
            catch (Exception ex)
            {
                total = 0;
                _Log4Net.Error("ProductItemTypeRepository下方法GetProductItemTypeList异常：" + ex.Message);
                return null;
            }
        }

        /// <summary>
        /// 获取产品分类映射列表
        /// </summary>
        /// <param name="request"></param>
        /// <param name="total"></param>
        /// <returns></returns>

        public List<ProductTypeMapping> GetProductTypeList(ProductTypeMappingListRequest request, out int total)
        {
            StringBuilder _query = new StringBuilder();
            _query.AppendFormat(@"(select c.SysDictValue as  FromSystem ,b.TeachLevelOneOrgName,b.ProductTypeName,b.NodeFlag,a.UpdateDate as MappingTime from T_POC_ProductTypeMap a 
                                  inner join  T_EXT_ItemType b on a.FKItemTypeId=b.Id
                                  inner join  T_SYS_Dictionary c on cast(b.Pocsource as nvarchar)=c.SysDictKey 
                                  where a.FKProductTypeGuid=@ProductTypeID");
            var _parameters = new DynamicParameters();
            _parameters.Add("@ProductTypeID", request.ProductTypeId);
            _query.AppendFormat(" ) h");
            //总数
            total = GetInfos<int>(EumDBName.POC, string.Format("SELECT COUNT(1) FROM  {0} ", _query), _parameters).First();
            //分页
            var _queryPage = string.Format(@"select   FromSystem,
                                                      TeachLevelOneOrgName ,
                                                      ProductTypeName ,
                                                      NodeFlag ,
                                                      MappingTime 
                                                      from (select ROW_NUMBER() OVER(ORDER BY h.MappingTime desc) as RowNum,h.* from {0}) as RowNumberTable", _query);
            if (request.Pagination)
            {
                _queryPage += " where RowNum between @rowbegin and @rowend  ";
                _parameters.Add("@rowbegin", (request.PageIndex - 1) * request.PageSize + 1);
                _parameters.Add("@rowend", request.PageIndex * request.PageSize);
            }
            var list = GetInfos<ProductTypeMapping>(EumDBName.POC, _queryPage.ToString(), _parameters).ToList();
            return list;
        }


        /// <summary>
        /// 判断来源系统的所有产品类别是否映射
        /// </summary>
        /// <returns></returns>
        public bool IsAllMapping()
        {
            int totalItemType = GetInfos<int>(EumDBName.POC, string.Format("  select Count(1)  from (select ProductTypeID from  [dbo].[T_EXT_ItemType] group by ProductTypeID) tb1 "), null).First();
            int totalMapping = GetInfos<int>(EumDBName.POC, string.Format(" select Count(1)  from (select ProductTypeID from  [dbo].[T_POC_ProductTypeMap] group by ProductTypeID) tb1"), null).First();
            return totalItemType <= totalMapping;
        }
    }
}

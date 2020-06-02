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
    public class BasicDataRepository : RepositoryBase, IBasicDataRepository
    {
        public T_EXT_BasicData GetInfoByGuid(int fromSystem, string id)
        {
            return GetInfos<T_EXT_BasicData>("select * from T_EXT_BasicData where FromSystem = @fromSystem and DictId = @dictId",
                new { fromSystem = fromSystem, dictId = id }).FirstOrDefault();
        }

        /// <summary>
        /// 获取基础参数映射列表
        /// </summary>
        /// <param name="request"></param>
        /// <param name="total"></param>
        /// <returns>返回列表</returns>
        public List<BasicDataMap> GetBasicDataMapList(BasicDataMapRequest request, out int total)
        {
            try
            {
                StringBuilder _query = new StringBuilder();
                _query.AppendFormat(@"(select a.Id as SortId,  b.Id,c.SysDictValue as FromSystem,a.OneOrgName,a.DictTypeName,a.DictValue, CASE WHEN  b.BasicDataMapGuid is null   THEN '否'
                                    ELSE '是' END as IsMapping,a.BasicDataGuid,b.UpdateDate,b.CreatedDate,b.BasicDataMapGuid,b.FKDictGuid,	(STUFF((SELECT '>'+ltrim(SysDictValue)
                                    FROM T_SYS_Dictionary  where CAST(DictGuid as varchar(36))  in  (select Code from  [dbo].[f_split](b.SysCatalogTitle,',') )
                                    FOR XML PATH(''),TYPE).value('.','nvarchar(max)'), 1, 1, '') +'|'+b.SysCatalogTitle) as SysCatalogTitle
                                    from T_EXT_BasicData  a  
                                    left join T_POC_BasicDataMap b on a.BasicDataGuid=b.FKBasicDataGuid
                                    inner join T_SYS_Dictionary c on cast(a.Pocsource as nvarchar)=c.SysDictKey where 1=1");
                var _parameters = new DynamicParameters();
                if (request.Source?.Count > 0)
                {
                    _query.AppendFormat(" and a.Pocsource in @Pocsource");
                    _parameters.Add("@Pocsource", request.Source.ToArray());
                }
                if (request.LevelOneOrgID?.Count > 0)
                {
                    _query.AppendFormat(" and a.OneOrgId in @OneOrgId");
                    _parameters.Add("@OneOrgId", request.LevelOneOrgID.ToArray());
                }
                if (request.IsMapping == "0")
                {
                    _query.AppendFormat(" and b.BasicDataMapGuid is null");
                }
                if (request.IsMapping == "1")
                {
                    _query.AppendFormat(" and b.BasicDataMapGuid is not null");

                }
                _query.AppendFormat(" ) h");
                //总数
                total = GetInfos<int>(EumDBName.POC, string.Format("SELECT COUNT(1) FROM  {0} ", _query), _parameters).First();
                //分页
                var _queryPage = string.Format(@"select                Id,
                                                                       FromSystem ,
                                                                       OneOrgName ,
                                                                       DictTypeName ,
                                                                       DictValue ,
                                                                       IsMapping,UpdateDate,CreatedDate,BasicDataMapGuid,FKDictGuid,BasicDataGuid,
                                                                       SysCatalogTitle from (select ROW_NUMBER() OVER(ORDER BY h.SortId desc) as RowNum,h.* from {0}) as RowNumberTable", _query);
                if (request.Pagination)
                {
                    _queryPage += " where RowNum between @rowbegin and @rowend  ";
                    _parameters.Add("@rowbegin", (request.PageIndex - 1) * request.PageSize + 1);
                    _parameters.Add("@rowend", request.PageIndex * request.PageSize);
                }
                var list = GetInfos<BasicDataMap>(EumDBName.POC, _queryPage.ToString(), _parameters).ToList();
                return list;
            }
            catch (Exception ex)
            {
                total = 0;
                _Log4Net.Error("T_POC_ProductTypeRepository下方法GetBasicDataMapList异常：" + ex.Message);
                return null;
            }

        }
        /// <summary>
        /// 根据字典Guid获取产品池的基础数据
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public List<T_EXT_BasicData> GetBasicDataByDictIdMap(List<int> pocSource, List<Guid> fkDictGuid)
        {
            string sql = @"select tb1.[Id] , tb1.[BasicDataGuid] ,tb1.[FromSystem]  , convert(varchar(50),tb1.[OneOrgId])  as 'OneOrgId' ,tb1.[OneOrgName] ,tb1.[DictTypeCode] ,tb1.[DictTypeName]  ,tb1.[DictSort]  ,tb1.[DictId]
         ,tb1.[DictValue]  ,tb1.[DictCreatetime]  ,tb1.[DictUpdatetime]  ,tb1.[DictCode]  ,tb1.[DictIsSys]  ,tb1.[DictStatus] ,tb1.[DictDescribe] ,tb1.[UpdaterUserId] ,tb1.[UpdaterUserName],tb1.[CreatedDate]  ,tb1.[UpdateDate]
        ,tb1.[POCSource], tb2.FKDictGuid from [dbo].[T_EXT_BasicData] tb1 inner join [dbo].[T_POC_BasicDataMap] tb2 on tb1.BasicDataGuid=tb2.FKBasicDataGuid and POCSource in @POCSource and tb2.FKDictGuid in @FKDictGuid";
            return GetInfos<T_EXT_BasicData>(sql, new { POCSource = pocSource.ToArray(), FKDictGuid = fkDictGuid.ToArray() }).ToList();

        }

        public List<BasicDataPage> GetBasicDataInfoPage(BasicDataPageRequest request, out int total)
        {
            StringBuilder _query = new StringBuilder();
            _query.Append(@"select a.*,b.SysDictValue PocSourceName from T_EXT_BasicData a
                            left join T_SYS_Dictionary b on b.SysDictKey = cast(a.POCSource as nvarchar)");

            var _parameters = new DynamicParameters();
            var _where = new StringBuilder(" where 1 = 1");

            if (request.PocSource?.Count > 0)
            {
                _where.Append(" and a.POCSource in @Pocsource");
                _parameters.Add("@Pocsource", request.PocSource.ToArray());
            }
            if (request.OneOrgId?.Count > 0)
            {
                _where.Append(" and a.OneOrgId in @OneOrgId");
                _parameters.Add("@OneOrgId", request.OneOrgId.ToArray());
            }
            if (!string.IsNullOrWhiteSpace(request.DictValue))
            {
                var signs = new List<string> { "'", "[", "%", "_" };
                _where.Append(" and a.DictValue like @DictValue");
                _parameters.Add("@DictValue", $"%{request.DictValue.ReplaceBySigns(signs)}%");
            }

            var countsql = @"select count(1) from T_EXT_BasicData a
                            left join T_SYS_Dictionary b on b.SysDictKey = cast(a.POCSource as nvarchar)" + _where.ToString();

            total = GetInfos<int>(EumDBName.POC, countsql, _parameters).First();

            if (request.IsExport)
                return GetInfos<BasicDataPage>(EumDBName.POC, _query.ToString() + _where.ToString(), _parameters).ToList();

            return GetPageInfos<BasicDataPage>(EumDBName.POC, _query.ToString() + _where.ToString(), "a.Id ASC", request.PageIndex, request.PageSize, _parameters).ToList();
        }

        /// <summary>
        ///  判断产品池的所有基础参数是否全部映射
        /// </summary>
        /// <returns></returns>
        public bool IsAllMapping()
        {
            int totalbaseData = GetInfos<int>(EumDBName.POC, string.Format(" select Count(1) from (select BasicDataGuid  FROM [dbo].[T_EXT_BasicData] group by BasicDataGuid) tb1"), null).First();
            int totalMapping = GetInfos<int>(EumDBName.POC, string.Format("  select Count(1) from (select FKBasicDataGuid  FROM [dbo].[T_POC_BasicDataMap] group by FKBasicDataGuid) tb1"), null).First();
            return totalbaseData <= totalMapping;
        }
    }
}

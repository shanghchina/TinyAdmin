using Tiny.Common.Dapper.Repository;
using Tiny.OPS.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Tiny.OPS.Repository
{
    public class T_POC_ExtractorQueryRepository : RepositoryBase, IT_POC_ExtractorQueryRepository
    {
        /// <summary>
        /// 根据提取器获取头部查询信息
        /// </summary>
        /// <param name="extractorGuid"></param>
        /// <returns></returns>
        public List<T_POC_ExtractorQuery> GetListByExtractorGuid(Guid extractorGuid)
        {
            string _strSql = "select * from [dbo].[T_POC_ExtractorQuery] where FKExtractorGuid=@FKExtractorGuid";
            return GetInfos<T_POC_ExtractorQuery>(_strSql.ToString(), new { FKExtractorGuid = extractorGuid }).ToList();
        }

    }
}


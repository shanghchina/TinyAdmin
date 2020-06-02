using Tiny.Common.Dapper.Repository;
using Tiny.OPS.Domain;
using System;
using System.Collections.Generic;

namespace Tiny.OPS.Repository
{
	public interface  IT_POC_ExtractorQueryRepository :IRepository
    {
        /// <summary>
        /// 根据提取器获取头部查询信息
        /// </summary>
        /// <param name="extractorGuid"></param>
        /// <returns></returns>
        List<T_POC_ExtractorQuery> GetListByExtractorGuid(Guid extractorGuid);


    }
}


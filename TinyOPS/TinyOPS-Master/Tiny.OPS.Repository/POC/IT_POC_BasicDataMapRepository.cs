using Tiny.Common.Dapper.Repository;
using Tiny.OPS.Contract;
using Tiny.OPS.Domain;
using System;
using System.Collections.Generic;

namespace Tiny.OPS.Repository
{
    /// <summary>
    /// 基础数据映射
    /// </summary>
	public interface IT_POC_BasicDataMapRepository : IRepository
    {
        void AddPOC_BasicDataMap(T_POC_BasicDataMap entity);

        void UpdatePOC_BasicDataMap(T_POC_BasicDataMap entity);
    }
}


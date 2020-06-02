using Dapper;
using Tiny.Common.Dapper.Enumeration;
using Tiny.Common.Dapper.Repository;
using Tiny.OPS.Common;
using Tiny.OPS.Contract;
using Tiny.OPS.Domain;
using System;
using System.Collections.Generic;

namespace Tiny.OPS.Repository
{
    /// <summary>
    /// 基础数据映射
    /// </summary>
	public class T_POC_BasicDataMapRepository : RepositoryBase, IT_POC_BasicDataMapRepository
    {
        public void AddPOC_BasicDataMap(T_POC_BasicDataMap entity)
        {
            try
            {
                 Add<T_POC_BasicDataMap>(entity).Commit();
            }
            catch (Exception)
            {
                throw;

            }
        }

        public void UpdatePOC_BasicDataMap(T_POC_BasicDataMap entity)
        {
            try
            {
                Save<T_POC_BasicDataMap>(entity).Commit();
            }
            catch (Exception)
            {
                throw;

            }
        }
    }
}


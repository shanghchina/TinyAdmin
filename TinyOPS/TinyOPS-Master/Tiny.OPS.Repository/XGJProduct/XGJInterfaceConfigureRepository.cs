using Tiny.Common.Dapper.Repository;
using Tiny.OPS.Contract;
using Tiny.OPS.Domain.Enum;
using Tiny.OPS.Domain.XGJProduct;
using System;
using System.Collections.Generic;

namespace Tiny.OPS.Repository
{
    public class XGJInterfaceConfigureRepository : RepositoryBase, IXGJInterfaceConfigureRepository
    {
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public IList<T_SYS_UrlConfigure> GetList(XGJInterfaceConfigureStatus status)
        {
            return GetInfos<T_SYS_UrlConfigure>("SELECT * FROM T_SYS_UrlConfigure WHERE [UrlConfigureStatus]<>@status and [UrlConfigureStatus] = @status2 ORDER BY Category"
                , new { @status = XGJInterfaceConfigureStatus.Forbidden, @status2 = status });
        }

        /// <summary>
        /// 系统来源+事业部获取列表
        /// </summary>
        /// <returns></returns>
        public IList<T_SYS_UrlConfigure> GetListForPocSourceAndLevelOneOrgID(XGJInterfaceConfigureStatus status, JobDataRequest jobData)
        {
            return GetInfos<T_SYS_UrlConfigure>("SELECT * FROM T_SYS_UrlConfigure WHERE [UrlConfigureStatus]<>@status and [UrlConfigureStatus] = @status2 and POCSource in @pocSource and ChargeLevelOneOrgId in @levelOneOrgId ORDER BY Category"
                , new { @status = XGJInterfaceConfigureStatus.Forbidden, @status2 = status, @pocSource= jobData.PocSource.ToArray(), @levelOneOrgId = jobData.LevelOneOrgID.ToArray() });
        }
    }
}

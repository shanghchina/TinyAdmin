using Tiny.Common.Dapper.Repository;
using Tiny.OPS.Contract;
using Tiny.OPS.Domain.Enum;
using Tiny.OPS.Domain.XGJProduct;
using System.Collections.Generic;

namespace Tiny.OPS.Repository
{
    public interface IXGJInterfaceConfigureRepository : IRepository
    {
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        IList<T_SYS_UrlConfigure> GetList(XGJInterfaceConfigureStatus status);

        /// <summary>
        /// 系统来源+事业部获取列表
        /// </summary>
        /// <returns></returns>
        IList<T_SYS_UrlConfigure> GetListForPocSourceAndLevelOneOrgID(XGJInterfaceConfigureStatus status,
            JobDataRequest jobData);

    }
}

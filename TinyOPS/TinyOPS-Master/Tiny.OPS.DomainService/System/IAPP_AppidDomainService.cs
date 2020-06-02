using Tiny.Common.Dapper.Service;
using Tiny.OPS.Domain;

namespace Tiny.OPS.DomainService
{
    public interface IAPP_AppidDomainService : IDomainService
    {
        /// <summary>
        /// 根据IP获取Appid表
        /// </summary>
        T_APP_Appid GetAppidByIp(string ip, string appid, bool? isEnabled = true);

    }
}

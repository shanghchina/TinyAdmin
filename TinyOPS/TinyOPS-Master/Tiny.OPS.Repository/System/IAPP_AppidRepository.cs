using Tiny.Common.Dapper.Repository;
using Tiny.OPS.Domain;

namespace Tiny.OPS.Repository
{
    public interface IAPP_AppidRepository : IRepository
    {
        /// <summary>
        /// 根据IP获取Appid表
        /// </summary>
        T_APP_Appid GetAppidByIp(string ip, string appid, bool? isEnabled = true);
    }
}

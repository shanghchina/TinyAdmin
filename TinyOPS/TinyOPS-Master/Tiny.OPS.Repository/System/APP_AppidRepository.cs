using Tiny.Common.Dapper.Repository;
using Tiny.OPS.Domain;
using System.Linq;

namespace Tiny.OPS.Repository
{
    public class APP_AppidRepository : RepositoryBase, IAPP_AppidRepository
    {
        public T_APP_Appid GetAppidByIp(string ip, string appid, bool? isEnabled = true)
        {
            return GetInfos<T_APP_Appid>(@"select ad.* from T_APP_Appid ad
                                         join T_APP_WhiteList wl on wl.Id = ad.AppWhiteListId and wl.IsEnabled = 1
                                         where wl.Ip = @ip and ad.Appid = @appid and ad.IsEnabled = @isEnabled",
                                         new { ip = @ip, appid = @appid, isEnabled = @isEnabled }).FirstOrDefault();
        }
    }
}

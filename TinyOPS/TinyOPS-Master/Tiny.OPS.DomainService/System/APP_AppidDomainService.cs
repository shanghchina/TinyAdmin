using Tiny.Common.Dapper.DI;
using Tiny.Common.Dapper.Service;
using Tiny.OPS.Domain;
using Tiny.OPS.Repository;

namespace Tiny.OPS.DomainService
{
    public class APP_AppidDomainService : RealDomainService<T_APP_Appid>, IAPP_AppidDomainService
    {
        public IAPP_AppidRepository AppidRepository => IoC.Resolve<IAPP_AppidRepository>();
        public T_APP_Appid GetAppidByIp(string ip, string appid, bool? isEnabled = true)
        {
            return AppidRepository.GetAppidByIp(ip, appid);
        }
    }
}

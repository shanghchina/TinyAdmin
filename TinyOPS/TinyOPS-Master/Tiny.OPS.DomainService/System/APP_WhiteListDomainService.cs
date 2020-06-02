using Tiny.Common.Dapper.DI;
using Tiny.Common.Dapper.Service;
using Tiny.OPS.Domain;
using Tiny.OPS.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tiny.OPS.DomainService
{
    public class APP_WhiteListDomainService : RealDomainService<T_APP_WhiteList>, IAPP_WhiteListDomainService
    {
        public IAPP_WhiteListRepository WhiteListRepository => IoC.Resolve<IAPP_WhiteListRepository>();
        public IList<T_APP_WhiteList> GetSysWhiteListAll(bool? isEnabled = true)
        {
            return WhiteListRepository.GetSysWhiteListAll();
        }

    }
}

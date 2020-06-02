using Tiny.Common.Dapper.Service;
using Tiny.OPS.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tiny.OPS.DomainService
{
    public interface IAPP_WhiteListDomainService : IDomainService
    {
        IList<T_APP_WhiteList> GetSysWhiteListAll(bool? isEnabled = true);
    }
}

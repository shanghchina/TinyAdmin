using Tiny.Common.Dapper.Repository;
using Tiny.OPS.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tiny.OPS.Repository
{
    public interface IAPP_WhiteListRepository : IRepository
    {
        IList<T_APP_WhiteList> GetSysWhiteListAll(bool? isEnabled = true);
    }
}
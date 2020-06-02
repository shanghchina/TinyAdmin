using Tiny.Common.Dapper.Repository;
using Tiny.OPS.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tiny.OPS.Repository
{
    public class APP_WhiteListRepository : RepositoryBase, IAPP_WhiteListRepository
    {
        public IList<T_APP_WhiteList> GetSysWhiteListAll(bool? isEnabled = true)
        {
            return GetInfos<T_APP_WhiteList>("SELECT * FROM T_APP_WhiteList WHERE IsEnabled = @isEnabled", new { isEnabled = @isEnabled });
        }
    }
}

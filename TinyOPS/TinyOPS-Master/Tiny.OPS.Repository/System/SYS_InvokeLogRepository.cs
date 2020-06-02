using Tiny.Common.Dapper.Repository;
using Tiny.OPS.Domain;
using System.Collections.Generic;

namespace Tiny.OPS.Repository
{
    public class SYS_InvokeLogRepository : RepositoryBase, ISYS_InvokeLogRepository
    {
        public IList<T_SYS_InvokeLog> GetInvokeLogs(string func)
        {
            return GetInfos<T_SYS_InvokeLog>(@"select * from T_SYS_InvokeLog where InvokeFunction=@func", new { func = @func });
        }
    }
}

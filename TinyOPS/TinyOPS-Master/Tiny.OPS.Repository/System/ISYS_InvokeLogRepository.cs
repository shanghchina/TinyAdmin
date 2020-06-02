using Tiny.Common.Dapper.Repository;
using Tiny.OPS.Domain;
using System.Collections.Generic;

namespace Tiny.OPS.Repository
{
    public interface ISYS_InvokeLogRepository : IRepository
    {
        IList<T_SYS_InvokeLog> GetInvokeLogs(string func);
    }
}

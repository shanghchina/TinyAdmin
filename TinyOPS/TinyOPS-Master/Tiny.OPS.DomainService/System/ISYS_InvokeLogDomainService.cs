using Tiny.Common.Dapper.Service;
using Tiny.OPS.Domain;
using System.Collections.Generic;

namespace Tiny.OPS.DomainService
{
    public interface ISYS_InvokeLogDomainService : IDomainService
    {
        IList<T_SYS_InvokeLog> GetInvokeLogs(string func);
    }
}

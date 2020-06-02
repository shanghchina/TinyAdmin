using Tiny.Common.Dapper.DI;
using Tiny.Common.Dapper.Service;
using Tiny.OPS.Domain;
using Tiny.OPS.DomainService;
using Tiny.OPS.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tiny.OPS.DomainService
{
    public class SYS_InvokeLogDomainService : RealDomainService<T_SYS_InvokeLog>, ISYS_InvokeLogDomainService
    {
        public ISYS_InvokeLogRepository InvokeLogRepository => IoC.Resolve<ISYS_InvokeLogRepository>();
        public IList<T_SYS_InvokeLog> GetInvokeLogs(string func)
        {
            return InvokeLogRepository.GetInvokeLogs(func);
        }
    }
}

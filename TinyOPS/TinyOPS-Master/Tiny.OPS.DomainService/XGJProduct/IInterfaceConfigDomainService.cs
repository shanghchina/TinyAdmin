using Tiny.Common.Dapper.Service;
using Tiny.OPS.Common.Web.XGJTools;
using Tiny.OPS.Contract;
using Tiny.OPS.Domain.Enum;
using Tiny.OPS.Domain.XGJProduct;

namespace Tiny.OPS.DomainService
{
    public interface IInterfaceConfigDomainService : IDomainService
    {
        PushModel GetInterfaceConfigure(XGJInterfaceConfigureStatus status, string category = XGJInterfaceConfigureCategory.XGJInSideInterface, JobDataRequest jobData = null);
    }
}

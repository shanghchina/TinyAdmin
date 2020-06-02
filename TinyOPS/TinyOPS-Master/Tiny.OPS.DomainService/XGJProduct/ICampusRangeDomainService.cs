using Tiny.Common.Dapper.Service;

namespace Tiny.OPS.DomainService
{
    public interface ICampusRangeDomainService : IDomainService
    {
        void DeleteCampusRange(long course);
    }
}

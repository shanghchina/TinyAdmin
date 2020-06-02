using Tiny.Common.Dapper.Service;

namespace Tiny.OPS.DomainService
{
    public interface ITeachTimeDomainService : IDomainService
    {
        void DeleteTeachTime(long classID);
    }
}

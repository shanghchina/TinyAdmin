using Tiny.Common.Dapper.Repository;

namespace Tiny.OPS.Repository
{
    public interface ITeachTimeRepository : IRepository
    {
        void DeleteTeachTime(long classID);
    }
}

using Tiny.Common.Dapper.Repository;

namespace Tiny.OPS.Repository
{
    public interface ICampusRangeRepository : IRepository
    {
        void DeleteCampusRange(long course);
    }
}

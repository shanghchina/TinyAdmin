using Tiny.Common.Dapper.Repository;
using Tiny.OPS.Domain.XGJProduct;

namespace Tiny.OPS.Repository
{
    public class CampusRangeRepository : RepositoryBase, ICampusRangeRepository
    {
        public void DeleteCampusRange(long course)
        {
            var _result = ExecuteScalar<T_EXT_CourseRange>("delete from [T_EXT_CourseRange] where [ProductCourseID] = @course", new { course = @course });
        }
    }
}

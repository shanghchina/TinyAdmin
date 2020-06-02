using Tiny.Common.Dapper.DI;
using Tiny.Common.Dapper.Service;
using Tiny.OPS.Domain.XGJProduct;
using Tiny.OPS.Repository;

namespace Tiny.OPS.DomainService
{
   public class CampusRangeDomainService : RealDomainService<T_EXT_CourseRange>, ICampusRangeDomainService
    {
        private ICampusRangeRepository CampusRangeRepository => IoC.Resolve<ICampusRangeRepository>();
        public void DeleteCampusRange(long course)
        {
            CampusRangeRepository.DeleteCampusRange(course);
        }
    }
}

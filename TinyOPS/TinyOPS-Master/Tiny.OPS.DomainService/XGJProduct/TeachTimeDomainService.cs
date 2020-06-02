using Tiny.Common.Dapper.DI;
using Tiny.Common.Dapper.Service;
using Tiny.OPS.Domain.XGJProduct;
using Tiny.OPS.Repository;

namespace Tiny.OPS.DomainService
{
    public class TeachTimeDomainService : RealDomainService<T_EXT_ClassTeachTime>, ITeachTimeDomainService
    {
        private ITeachTimeRepository TeachTimeRepository => IoC.Resolve<ITeachTimeRepository>();

        public void DeleteTeachTime(long classID)
        {
            TeachTimeRepository.DeleteTeachTime(classID);
        }
    }
}

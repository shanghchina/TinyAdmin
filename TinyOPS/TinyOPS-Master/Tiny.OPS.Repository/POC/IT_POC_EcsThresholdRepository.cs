using Tiny.Common.Dapper.Repository;
using Tiny.OPS.Domain;

namespace Tiny.OPS.Repository
{
    public interface IT_POC_EcsThresholdRepository: IRepository
    {
        T_EXT_ThresholdValue GetThreshold(int fromSystem, string productid);
    }
}

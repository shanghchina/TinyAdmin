using Tiny.Common.Dapper.Repository;
using Tiny.OPS.Domain;
using System.Linq;

namespace Tiny.OPS.Repository
{
    public class T_POC_EcsThresholdRepository: RepositoryBase, IT_POC_EcsThresholdRepository
    {
        public T_EXT_ThresholdValue GetThreshold(int fromSystem, string productid)
        {
            string sql = "SELECT * FROM [dbo].[T_EXT_ThresholdValue] where FromSystem = @fromSystem and ProductID = @productID";
            return GetInfos<T_EXT_ThresholdValue>(sql, new { fromSystem = fromSystem, productID = productid }).FirstOrDefault();
        }

    }
}

using Tiny.Common.Dapper.Service;
using Tiny.OPS.Contract;
using Tiny.OPS.Domain;


namespace Tiny.OPS.DomainService
{
    /// <summary>
    /// 基础数据映射
    /// </summary>
	public interface IT_POC_BasicDataMapDomainService : IDomainService
    {

        void OperateBasicDataMap(OperateBasicDataMapRequest request);
    }
}


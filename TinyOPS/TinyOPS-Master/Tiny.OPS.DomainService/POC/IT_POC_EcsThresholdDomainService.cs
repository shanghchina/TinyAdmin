using Tiny.Common.Dapper.Service;
using Tiny.OPS.Contract;
using System.Collections.Generic;

namespace Tiny.OPS.DomainService
{
    public interface IT_POC_EcsThresholdDomainService: IDomainService
    {
        BaseResponse AddOrUpdateThresholdValue(List<ThresholdList> thresholds);
    }
}

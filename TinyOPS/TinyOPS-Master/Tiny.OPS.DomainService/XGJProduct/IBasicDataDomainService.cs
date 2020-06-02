using Tiny.Common.Dapper.Service;
using Tiny.OPS.Common.Web.XGJTools;
using Tiny.OPS.Contract;
using Tiny.OPS.Domain.XGJProduct;
using System;
using System.Collections.Generic;

namespace Tiny.OPS.DomainService
{
    public interface IBasicDataDomainService : IDomainService
    {
        void ExecuteTrans(PushConfig config, string dataJson, string dicTypeCode, string dictTypeName);


        /// <summary>
        /// 获取基础参数映射列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns>返回列表</returns>
        /// <returns></returns>
        BasicDataMapResponse GetBasicDataMapList(BasicDataMapRequest request);

        /// <summary>
        /// 根据字典Guid获取产品池的基础数据
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        List<T_EXT_BasicData> GetBasicDataByDictIdMap(List<int> pocSource, List<Guid> fkDictGuid);

        /// <summary>
        /// 获取基础数据分页
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        BasicDataPageResponse GetBasicDataInfoPage(BasicDataPageRequest request);
        /// <summary>
        ///  判断产品池的所有基础参数是否全部映射
        /// </summary>
        /// <returns></returns>
        bool IsAllMapping();

    }
}

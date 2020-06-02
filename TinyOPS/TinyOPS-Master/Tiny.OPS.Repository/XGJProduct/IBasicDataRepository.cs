using Tiny.Common.Dapper.Repository;
using Tiny.OPS.Contract;
using Tiny.OPS.Domain.XGJProduct;
using System;
using System.Collections.Generic;

namespace Tiny.OPS.Repository
{
    public interface IBasicDataRepository : IRepository
    {
        T_EXT_BasicData GetInfoByGuid(int fromSystem, string id);

        /// <summary>
        /// 获取基础参数映射列表
        /// </summary>
        /// <param name="request"></param>
        /// <param name="total"></param>
        /// <returns>返回列表</returns>
        /// <returns></returns>
        List<BasicDataMap> GetBasicDataMapList(BasicDataMapRequest request, out int total);

        List<T_EXT_BasicData> GetBasicDataByDictIdMap(List<int> pocSource, List<Guid> fkDictGuid);

        /// <summary>
        /// 获取基础参数分页
        /// </summary>
        /// <param name="request"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        List<BasicDataPage> GetBasicDataInfoPage(BasicDataPageRequest request, out int total);

        /// <summary>
        /// 判断产品池的所有基础参数是否全部映射
        /// </summary>
        /// <returns></returns>
        bool IsAllMapping();
    }
}

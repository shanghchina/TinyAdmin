using Tiny.Common.Dapper.Repository;
using Tiny.OPS.Contract;
using Tiny.OPS.Domain.XGJProduct;
using System.Collections.Generic;

namespace Tiny.OPS.Repository
{
    public interface IProductCourseRepository : IRepository
    {
        T_EXT_Course GetInfoByGuid(int from, string guid);

        /// <summary>
        /// 根据提取器信息获取课程信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        GetCouseListByExtractorResponse GetCouseListByExtractor(GetCouseListByExtractorRequest request);

        /// <summary>
        /// 获取课程信息分页
        /// </summary>
        /// <param name="request"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        List<ProductCoursePageInfo> GetProductCoursePageInfo(ProductCoursePageRequest request, out int total);

    }
}

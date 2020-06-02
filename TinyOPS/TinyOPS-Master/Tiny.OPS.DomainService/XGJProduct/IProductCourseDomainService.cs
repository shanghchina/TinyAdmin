using Tiny.Common.Dapper.Service;
using Tiny.OPS.Contract;
using Tiny.OPS.Domain.XGJProduct;

namespace Tiny.OPS.DomainService
{
   public interface IProductCourseDomainService : IDomainService
    {
        T_EXT_Course GetInfoByGuid(int from, string guid);
        /// <summary>
        /// 根据提取器信息获取课程信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        GetCouseListByExtractorResponse GetCouseListByExtractor(GetCouseListByExtractorRequest request);

        /// <summary>
        /// 获取产品课程信息分页
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        ProductCoursePageResponse GetProductCoursePageInfo(ProductCoursePageRequest request);

    }
}

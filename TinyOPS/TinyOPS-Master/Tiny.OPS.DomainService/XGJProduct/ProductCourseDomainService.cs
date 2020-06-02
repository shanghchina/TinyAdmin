using Tiny.Common.Dapper.DI;
using Tiny.Common.Dapper.Service;
using Tiny.Common.Extensions;
using Tiny.OPS.Contract;
using Tiny.OPS.Domain;
using Tiny.OPS.Domain.XGJProduct;
using Tiny.OPS.Repository;
using System;

namespace Tiny.OPS.DomainService
{
    public class ProductCourseDomainService : RealDomainService<T_EXT_Course>, IProductCourseDomainService
    {
        private IProductCourseRepository ProductCourseRepository => IoC.Resolve<IProductCourseRepository>();
        public T_EXT_Course GetInfoByGuid(int from, string guid)
        {
            return ProductCourseRepository.GetInfoByGuid(from, guid);
        }
        /// <summary>
        /// 根据提取器信息获取课程信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public GetCouseListByExtractorResponse GetCouseListByExtractor(GetCouseListByExtractorRequest request)
        {
            //根据产品动销阈值定义最后销售时间
            switch (request.thresholdValue)
            {
                case (int)ThresholdValue.NoLimit:
                    request.lastSaleTime = null;
                    break;
                case (int)ThresholdValue.ThreeMonths:
                    request.lastSaleTime = DateTime.Now.AddMonths(-3);
                    break;
                case (int)ThresholdValue.SixMonths:
                    request.lastSaleTime = DateTime.Now.AddMonths(-6);
                    break;
                case (int)ThresholdValue.OneYear:
                    request.lastSaleTime = DateTime.Now.AddYears(-1);
                    break;
                case (int)ThresholdValue.TwoYear:
                    request.lastSaleTime = DateTime.Now.AddYears(-2);
                    break;
                default:
                    request.lastSaleTime = null;
                    break;
            }
            GetCouseListByExtractorResponse response = ProductCourseRepository.GetCouseListByExtractor(request);
            if (response.dataList!=null && response.dataList.Count>0)
            {
                foreach (var item in response.dataList)
                {
                    item.CourseStatusName = EnumExtension.ToEnumTypeDescriptionByID(typeof(CourseStatusEnum), item.CourseStatus.ToString());
                }
            }
         
            return response;
        }

        public ProductCoursePageResponse GetProductCoursePageInfo(ProductCoursePageRequest request)
        {
            var response = new ProductCoursePageResponse();
            response.DataList = ProductCourseRepository.GetProductCoursePageInfo(request, out int total);
            response.Total = total;
            return response;
        }
    }
}

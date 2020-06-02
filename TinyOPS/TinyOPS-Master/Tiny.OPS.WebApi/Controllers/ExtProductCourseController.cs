using Microsoft.AspNetCore.Mvc;
using Tiny.Common.Dapper.DI;
using Tiny.OPS.Common;
using Tiny.OPS.Contract;
using Tiny.OPS.DomainService;
using System;

namespace Tiny.OPS.WebApi.Controllers
{
    /// <summary>
    /// 数据来源产品课程信息
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ExtProductCourseController : BaseController
    {
        private IProductCourseDomainService ProductCourseRepository => IoC.Resolve<IProductCourseDomainService>();

        /// <summary>
        /// 获取产品课程信息分页
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public BaseResponse GetProductCoursePageInfo(ProductCoursePageRequest request)
        {
            try
            {
                ProductCoursePageResponse response = ProductCourseRepository.GetProductCoursePageInfo(request);
                return ApiSuccessResult(response);
            }
            catch (Exception ex)
            {
                _Log4Net.Error("GetEXTCourseByPage--异常信息", ex);
                return ApiErrorResult(ex.Message);
            }
        }

        /// <summary>
        /// 导出产品课程信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult ExportExcle(ProductCoursePageRequest request)
        {
            try
            {
                ProductCoursePageResponse response = ProductCourseRepository.GetProductCoursePageInfo(request);
                foreach (var item in response.DataList)
                {
                    if (item.CourseStatus == "0")
                    {
                        item.CourseStatus = "无效";
                    }
                    else if (item.CourseStatus == "1")
                    {
                        item.CourseStatus = "有效";
                    }
                    else if (item.CourseStatus == "-1")
                    {
                        item.CourseStatus = "已删除";
                    }
                    else
                    {
                        item.CourseStatus = "未知";
                    }
                    if (item.ExtractStatus == "1000")
                    {
                        item.ExtractStatus = "未提取";
                    }
                    else if (item.ExtractStatus == "2000")
                    {
                        item.ExtractStatus = "已提取";
                    }
                    else if (item.ExtractStatus == "3000")
                    {
                        item.ExtractStatus = "已关联";
                    }
                    else
                    {
                        item.ExtractStatus = "未知";
                    }
                }

                var stream = ExcelHelper.SaveExcel(response.DataList);
                return File(stream, "application/vnd.ms-excel", "课程信息.xlsx");
            }
            catch (Exception ex)
            {
                _Log4Net.Error("ExportExcle--异常信息", ex);
                return null;
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tiny.Common.Dapper.DI;
using Tiny.OPS.Common;
using Tiny.OPS.Contract;
using Tiny.OPS.DomainService;

namespace Tiny.OPS.WebApi.Controllers
{
    /// <summary>
    /// 提取器
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class POCExtractorController : BaseController
    {

        private IT_POC_ExtractorDetailDomainService pocExtractorDetailDomainService => IoC.Resolve<IT_POC_ExtractorDetailDomainService>();
        private IProductCourseDomainService productCourseDomainService => IoC.Resolve<IProductCourseDomainService>();

        /// <summary>
        ///分页获取提取器列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public BaseResponse GetExtractorDetaiList(GetExtractorDetaiListRequest request)
        {
            try
            {
                GetExtractorDetailListResponse response = pocExtractorDetailDomainService.GetExtractorDetaiList(request);
                return ApiSuccessResult(response);
            }
            catch (Exception ex)
            {
                _Log4Net.Error("GetExtractorDetaiList--异常信息", ex);
                return ApiErrorResult(ex.Message);
            }
        }
        /// <summary>
        /// 提取器导出
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult ExportExcle(GetExtractorDetaiListRequest request)
        {
            try
            {
                GetExtractorDetailListResponse response = pocExtractorDetailDomainService.GetExtractorDetaiList(request);
                var stream = ExcelHelper.SaveExcel(response.dataList);
                return File(stream, "application/vnd.ms-excel", "提取器.xlsx");
            }
            catch (Exception ex)
            {
                _Log4Net.Error("ExportExcle--异常信息", ex);
                return null;
            }
        }

        /// <summary>
        /// 提取器预览判断
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public BaseResponse PreviewJudge(PreviewJudgeExtractorRequest request)
        {
            try
            {
                PreviewJudgeExtractorResponse response = pocExtractorDetailDomainService.PreviewJudgeExtractor(request);
                return ApiSuccessResult(response);
            }
            catch (Exception ex)
            {
                _Log4Net.Error("PreviewJudge--异常信息", ex);
                return ApiErrorResult(ex.Message);
            }
        }
        /// <summary>
        /// 创建提取器
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public BaseResponse CreateExtractor(CreateExtractorRequest request)
        {
            try
            {
                bool isTrue = pocExtractorDetailDomainService.CreateExtractor(request);
                if (isTrue)
                {
                    return ApiSuccessResult(isTrue);
                }
                else
                {
                    return ApiFailResult(isTrue.ToString());
                }

            }
            catch (Exception ex)
            {
                _Log4Net.Error("PreviewJudge--异常信息", ex);
                return ApiErrorResult(ex.Message);
            }
        }

        /// <summary>
        /// 取消提取器
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public BaseResponse CancleExtractor(CancleExtractorRequest request)
        {
            try
            {
                bool isTrue = pocExtractorDetailDomainService.CancleExtractor(request);
                if (isTrue)
                {
                    return ApiSuccessResult(isTrue);
                }
                else
                {
                    return ApiFailResult(isTrue.ToString());
                }
            }
            catch (Exception ex)
            {
                _Log4Net.Error("PreviewJudge--异常信息", ex);
                return ApiErrorResult(ex.Message);
            }
        }
        /// <summary>
        /// 获取提取器信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public BaseResponse GetExtractorInfo(GetExtractorInfoRequest request)
        {
            try
            {
                GetExtractorInfoResponse response = pocExtractorDetailDomainService.GetExtractorInfo(request);
                return ApiSuccessResult(response);

            }
            catch (Exception ex)
            {
                _Log4Net.Error("PreviewJudge--异常信息", ex);
                return ApiErrorResult(ex.Message);
            }
        }

        /// <summary>
        /// 根据提取器信息获取课程信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public BaseResponse GetCouseListByExtractor(GetCouseListByExtractorRequest request)
        {
            try
            {
                GetCouseListByExtractorResponse response = productCourseDomainService.GetCouseListByExtractor(request);
                return ApiSuccessResult(response);

            }
            catch (Exception ex)
            {
                _Log4Net.Error("PreviewJudge--异常信息", ex);
                return ApiErrorResult(ex.Message);
            }
        }

        /// <summary>
        /// 提取产品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public BaseResponse ExtractProducts(ExtractProductRequest request)
        {
            try
            {
                var isTrue = pocExtractorDetailDomainService.ExtractProducts(request);
                if (isTrue)
                {
                    return ApiSuccessResult(isTrue);
                }
                else
                {
                    return ApiFailResult(isTrue.ToString());
                }

            }
            catch (Exception ex)
            {
                _Log4Net.Error("PreviewJudge--异常信息", ex);
                return ApiErrorResult(ex.Message);
            }
        }


    }
}
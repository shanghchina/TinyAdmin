using Tiny.Common.Dapper.Service;
using Tiny.OPS.Contract;
using Tiny.OPS.Domain;
using System;

namespace Tiny.OPS.DomainService
{
	public interface  IT_POC_ExtractorDetailDomainService :IDomainService
    {
        /// <summary>
        /// 获取提取器列表
        /// </summary>
        /// <returns></returns>
        GetExtractorDetailListResponse GetExtractorDetaiList(GetExtractorDetaiListRequest request);

        /// <summary>
        /// 提取器预览判断
        /// </summary>
        /// <param name="request"></param>
        PreviewJudgeExtractorResponse PreviewJudgeExtractor(PreviewJudgeExtractorRequest request);

        /// <summary>
        /// 创建提取器
        /// </summary>
        /// <param name="request"></param>
        bool CreateExtractor(CreateExtractorRequest request);

        /// <summary>
        ///取消提取器
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        bool CancleExtractor(CancleExtractorRequest request);

        /// <summary>
        /// 获取提取器信息
        /// </summary>
        /// <param name="ExtractorDetailGuid"></param>
        /// <returns></returns>
        GetExtractorInfoResponse GetExtractorInfo(GetExtractorInfoRequest request);

        /// <summary>
        /// 提取产品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        bool ExtractProducts(ExtractProductRequest request);


    }
}


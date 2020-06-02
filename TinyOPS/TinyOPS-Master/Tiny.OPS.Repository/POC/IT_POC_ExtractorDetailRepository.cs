using Tiny.Common.Dapper.Repository;
using Tiny.OPS.Contract;
using Tiny.OPS.Domain;
using System;
using System.Collections.Generic;

namespace Tiny.OPS.Repository
{
	public interface  IT_POC_ExtractorDetailRepository :IRepository
    {
        /// <summary>
        /// 获取提取器列表
        /// </summary>
        /// <returns></returns>
        GetExtractorDetailListResponse GetExtractorDetaiList(GetExtractorDetaiListRequest request);
        /// <summary>
        /// 获取单个提取器
        /// </summary>
        /// <param name="tiem"></param>
        /// <returns></returns>
        ExtractorItem GetSingleExtractor(ExtractorItem tiem);

        /// <summary>
        /// 获取提取器最大的编号
        /// </summary>
        /// <returns></returns>
        string GetMaxExtractorNo();

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
        T_POC_ExtractorDetail GetSingleByGuid(Guid extractorDetailGuid);


    }
}


using Tiny.OPS.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tiny.OPS.Contract
{
    /// <summary>
    /// 获取提取器信息返回信息
    /// </summary>
    public class GetExtractorInfoResponse
    {

        /// <summary>
        /// 头部查询条件
        /// </summary>
        public List<T_POC_ExtractorQuery> queryList { get; set; } = new List<T_POC_ExtractorQuery>();

        /// <summary>
        /// 提取器详情
        /// </summary>
        public T_POC_ExtractorDetail extractorDetail { get; set; } = new T_POC_ExtractorDetail();



    }
}

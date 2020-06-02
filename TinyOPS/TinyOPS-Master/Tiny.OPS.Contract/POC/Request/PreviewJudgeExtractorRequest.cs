using System;
using System.Collections.Generic;
using System.Text;

namespace Tiny.OPS.Contract
{
    /// <summary>
    /// 提取器预览判断请求信息
    /// </summary>
    public class PreviewJudgeExtractorRequest
    {
        /// <summary>
        /// 请求集合
        /// </summary>
        public List<ExtractorItem> dataList { get; set; }
    }
}

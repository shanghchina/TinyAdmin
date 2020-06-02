using System;
using System.Collections.Generic;
using System.Text;

namespace Tiny.OPS.Contract
{
    /// <summary>
    /// 获取提取器信息
    /// </summary>
    public class GetExtractorInfoRequest
    {
        /// <summary>
        ///提取器详细Guid
        /// </summary>
        public Guid extractorDetailGuid { get; set; }
    }
}

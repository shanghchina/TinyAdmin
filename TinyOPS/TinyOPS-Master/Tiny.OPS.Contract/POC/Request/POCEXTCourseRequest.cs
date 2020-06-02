using Tiny.OPS.Domain;
using System;

namespace Tiny.OPS.Contract
{

    /// <summary>
    /// 产品信息-来源系统产品体系--课程信息表 查询条件
    /// </summary>
    public class POCEXTCourseRequest : SearchBase
    {
        /// <summary>
        /// FK产品信息表guid
        /// </summary>
        public Guid FKProductCourseGuid { get; set; } = Guid.Empty;

        /// <summary>
        /// 提取状态 =提取状态1000未提取 2000已提取 3000已关联
        /// </summary>
        public EXTCourseExtractStatusEnum ExtractStatus { get; set; }

    }
}

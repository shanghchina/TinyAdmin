using Tiny.OPS.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tiny.OPS.Contract
{
    /// <summary>
    /// 课程管理页面分页参数
    /// </summary>
   public class ProductCoursePageRequest : BaseRequestPage
    {
        /// <summary>
        /// 系统来源
        /// </summary>
        public List<string> PocSource { get; set; }

        /// <summary>
        /// 事业部Id
        /// </summary>
        public List<Guid> LevelOneOrgID { get; set; }

        /// <summary>
        /// 课程状态
        /// </summary>
        public CourseStatusEnum? CourseStatus { get; set; }

        /// <summary>
        /// 提取状态
        /// </summary>
        public EXTCourseExtractStatusEnum? ExtractStatus { get; set; }

        /// <summary>
        /// 课程名称
        /// </summary>
        public string CourseName { get; set; }

        /// <summary>
        /// 是否导出
        /// </summary>
        public bool IsExport { get; set; }
    }
}

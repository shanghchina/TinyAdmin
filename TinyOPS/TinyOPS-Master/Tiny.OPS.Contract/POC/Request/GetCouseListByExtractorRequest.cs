using System;
using System.Collections.Generic;
using System.Text;

namespace Tiny.OPS.Contract
{
    /// <summary>
    /// 根据提取器信息获取产品课程
    /// </summary>
    public class GetCouseListByExtractorRequest : BaseRequestPage
    {
        /// <summary>
        /// 是否只查总条数
        /// </summary>
        public bool IsCount { get; set; } = false;
        /// <summary>
        /// 来源系统
        /// </summary>
        public List<int> fromSystems { get; set; }

        /// <summary>
        /// 事业部
        /// </summary>
        public List<Guid> oneOrgIds { get; set; }

        /// <summary>
        /// 产品状态
        /// </summary>
        public int courseStatus { get; set; }

        /// <summary>
        /// 产品动销阈值
        /// </summary>
        public int thresholdValue { get; set; }

        /// <summary>
        /// 产品最后销售时间
        /// </summary>
        public DateTime? lastSaleTime { get; set; }

        /// <summary>
        /// 年份
        /// </summary>
        public int courseYear { get; set; }

        /// <summary>
        /// 年级
        /// </summary>
        public List<string> gradeId { get; set; }

        /// <summary>
        /// 所属类型
        /// </summary>
        public List<string> categoryId { get; set; }

        /// <summary>
        /// 科目
        /// </summary>
        public List<string> subjectId { get; set; }

        /// <summary>
        /// 期望值
        /// </summary>
        public List<string> termId { get; set; }

        /// <summary>
        /// 期望值
        /// </summary>
        public List<string> classTypeId { get; set; }

        /// <summary>
        /// 排除的产品
        /// </summary>
        public List<Guid> notInCourseGuid { get; set; }

        /// <summary>
        /// 提取器Guid
        /// </summary>
        public string extractorDetailGuid { get; set; }

        /// <summary>
        /// 产品提取状态
        /// </summary>
        public int extractStatus { get; set; }

    }
}

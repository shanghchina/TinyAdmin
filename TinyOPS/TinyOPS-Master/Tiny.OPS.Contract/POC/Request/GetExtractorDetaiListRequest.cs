using System;
using System.Collections.Generic;
using System.Text;

namespace Tiny.OPS.Contract
{
    /// <summary>
    /// 提取器查询请求信息
    /// </summary>
    public class GetExtractorDetaiListRequest : BaseRequestPage
    {
        /// <summary>
        /// 来源系统集合
        /// </summary>
        public List<string> ExtFromSystem { get; set; }
        /// <summary>
        /// 事业部集合
        /// </summary>
        public List<string> LevelOneGuid { get; set; }

        /// <summary>
        /// 提取器状态
        /// </summary>
        public string ExtractorStatus { get; set; }

        /// <summary>
        /// 所属年级
        /// </summary>

        public List<string> GradeID { get; set; }
        
        /// <summary>
        /// 课程所属类型
        /// </summary>

        public List<string> CategoryID { get; set; }

        /// <summary>
        /// 所属科目
        /// </summary>

        public List<string> SubjectID { get; set; }

        /// <summary>
        /// 所属期望端
        /// </summary>

        public List<string> TermID { get; set; }

        /// <summary>
        /// 所属班型
        /// </summary>
        public List<string> ClassTypeID { get; set; }

        /// <summary>
        /// 提取器编号
        /// </summary>
        public string ExtractorNo { get; set; }


    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Tiny.OPS.Contract
{
    /// <summary>
    /// 请求信息
    /// </summary>
    public class CreateExtractorRequest
    {
        /// <summary>
        /// 请求集合
        /// </summary>
        public List<ExtractorItem> dataList { get; set; }

    }
    /// <summary>
    /// item信息
    /// </summary>
    public class ExtractorItem
    {
        /// <summary>
        ///提取器编号
        /// </summary>
        public string extractorNo { get; set; }

        /// <summary>
        /// 提取器状态
        /// </summary>
        public int extractorStatus { get; set; }

        /// <summary>
        /// 提取器状态名称
        /// </summary>
        public string extractorStatusName { get; set; }

        /// <summary>
        /// 来源系统
        /// </summary>
        public List<Guid> fromSystem { get; set; }

        /// <summary>
        /// 来源系统
        /// </summary>
        public string  fromSystemName { get; set; }

        /// <summary>
        /// 事业部
        /// </summary>
        public List<Guid> oneOrgId { get; set; }

        /// <summary>
        /// 来源系统
        /// </summary>
        public string oneOrgName { get; set; }

        /// <summary>
        /// 产品状态
        /// </summary>
        public int proStatue { get; set; }


        /// <summary>
        /// 产品状态
        /// </summary>
        public string  proStatueName { get; set; }

        /// <summary>
        /// 产品动销阈值
        /// </summary>
        public int thresholdValue { get; set; }

          /// <summary>
        /// 产品动销阈值
        /// </summary>
        public string thresholdName { get; set; }

        /// <summary>
        /// 年份
        /// </summary>
        public int year { get; set; }

        /// <summary>
        /// 年级
        /// </summary>
        public string gradeId { get; set; }

        /// <summary>
        /// 年级
        /// </summary>
        public string gradeName { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public string categoryId { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public string categoryName { get; set; }

        /// <summary>
        /// 科目
        /// </summary>
        public string subjectId { get; set; }

        /// <summary>
        /// 科目
        /// </summary>
        public string subjectName { get; set; }

        /// <summary>
        /// 期望值
        /// </summary>
        public string termId { get; set; }

        /// <summary>
        /// 期望值
        /// </summary>
        public string termName { get; set; }

        /// <summary>
        /// 班型
        /// </summary>
        public string classTypeId { get; set; }
        /// <summary>
        /// 班型
        /// </summary>
        public string classTypeName { get; set; }

        /// <summary>
        /// 操作人
        /// </summary>
        public string UpdateUserId { get; set; }

        // <summary>
        /// 操作人
        /// </summary>
        public string UpdateUserName { get; set; }

        /// <summary>
        ///涉及产品数量
        /// </summary>
        public int productCount { get; set; }
        

    }



}

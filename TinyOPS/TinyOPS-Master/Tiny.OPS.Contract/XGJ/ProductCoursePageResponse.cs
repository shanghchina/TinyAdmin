using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Tiny.OPS.Contract
{
    /// <summary>
    /// 课程管理页面分页返回
    /// </summary>
    public class ProductCoursePageResponse
    {
        /// <summary>
        /// 基础参数集合
        /// </summary>
        public List<ProductCoursePageInfo> DataList { get; set; }
        /// <summary>
        /// 总数
        /// </summary>
        public int Total { get; set; }
    }

    /// <summary>
    /// 课程管理页面分页返回
    /// </summary>
    public class ProductCoursePageInfo
    {
        /// <summary>
        /// 系统来源
        /// </summary>
        [Display(Name = "系统来源")]
        public string PocSource { get; set; }

        /// <summary>
        /// 所属事业部
        /// </summary>
        [Display(Name = "所属事业部")]
        public string LevelOneOrgName { get; set; }

        /// <summary>
        /// 课程名称
        /// </summary>
        [Display(Name = "课程名称")]
        public string CourseName { get; set; }

        /// <summary>
        /// 所属分类
        /// </summary>
        [Display(Name = "所属分类")]
        public string ProductTypeName { get; set; }

        /// <summary>
        /// 课程状态
        /// </summary>
        [Display(Name = "课程状态")]
        public string CourseStatus { get; set; }

        /// <summary>
        /// 提取状态
        /// </summary>
        [Display(Name = "提取状态")]
        public string ExtractStatus { get; set; }

        /// <summary>
        /// 课时单价
        /// </summary>
        [Display(Name = "课时单价")]
        public decimal FeeUnitPrice { get; set; }

        /// <summary>
        /// 课时单位
        /// </summary>
        [Display(Name = "课时单位")]
        public string FeeUnitPriceName { get; set; }

        /// <summary>
        /// 课时数
        /// </summary>
        [Display(Name = "课时数")]
        public decimal TotalClassHour { get; set; }

        /// <summary>
        /// 所属年份
        /// </summary>
        [Display(Name = "所属年份")]
        public int CourseYear { get; set; }

        /// <summary>
        /// 所属年级
        /// </summary>
        [Display(Name = "所属年级")]
        public string GradeName { get; set; }

        /// <summary>
        /// 所属类型
        /// </summary>
        [Display(Name = "所属类型")]
        public string CategoryName { get; set; }

        /// <summary>
        /// 所属科目
        /// </summary>
        [Display(Name = "所属科目")]
        public string SubjectName { get; set; }

        /// <summary>
        /// 所属期段
        /// </summary>
        [Display(Name = "所属期段")]
        public string TermName { get; set; }

        /// <summary>
        /// 所属班型
        /// </summary>
        [Display(Name = "所属班型")]
        public string ClassTypeName { get; set; }

        /// <summary>
        /// 授权数
        /// </summary>
        [Display(Name = "授权校区(个)")]
        public int AuthorizeNum { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        [Display(Name = "创建日期")]
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// 跟新日期
        /// </summary>
        [Display(Name = "更新日期")]
        public DateTime? UpdateDate { get; set; }
    }
}

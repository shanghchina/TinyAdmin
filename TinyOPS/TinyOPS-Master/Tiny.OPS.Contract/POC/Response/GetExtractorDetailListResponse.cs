using Tiny.OPS.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Tiny.OPS.Contract
{
    //提取器返回值
    public class GetExtractorDetailListResponse
    {
        //初始化
        public GetExtractorDetailListResponse()
        {
            totalCount = 0;
            dataList = new List<ExtractorDetailItem>();
        }

        /// <summary>
        /// 总条数
        /// </summary>
        public int totalCount { get; set; }
        /// <summary>
        /// 数据集合
        /// </summary>
        public List<ExtractorDetailItem> dataList { get; set; }
    }
    /// <summary>
    /// 提取器明细
    /// </summary>
    public class ExtractorDetailItem
    {

        /// <summary>
        /// guid
        /// </summary>
        [NoExport]
        public Guid FKExtractorGuid { get; set; }
        /// <summary>
        /// guid
        /// </summary>
        [NoExport]
        public Guid ExtractorDetailGuid { get; set; }
        /// <summary>
        /// 2100待提取 2200 待确认 2300已完成 2400已取消 2500提取中
        /// </summary>
        [NoExport]
        public int ExtractorStatus { get; set; }

        /// <summary>
        /// ExtractorNo
        /// </summary>
        [Display(Name = "提取器编号")]
        public string ExtractorNo { get; set; }

        /// <summary>
        /// 状态名称
        /// </summary>
        [Display(Name = "状态")]
        public string ExtractorStatusName { get; set; }
       

        /// <summary>
        /// 系统来源
        /// </summary>
        [Display(Name = "系统来源")]
        public string ExtFromSystem { get; set; }

        /// <summary>
        /// 所属事业部
        /// </summary>
        [Display(Name = "所属事业部")]
        public string OneOrg { get; set; }
      
        /// <summary>
        /// ProductCount
        /// </summary>
        [Display(Name = "涉及产品数量")]
        public int ProductCount { get; set; }
        /// <summary>
        /// ExtractCount
        /// </summary>
        [Display(Name = "提取数量")]
        public int ExtractCount { get; set; }
        /// <summary>
        /// AssociatedCount
        /// </summary>
        [Display(Name = "关联数量")]
        public int AssociatedCount { get; set; }
        /// <summary>
        /// NotExtractCount
        /// </summary>
        [NoExport]
        public int NotExtractCount { get; set; }
        /// <summary>
        /// Year
        /// </summary>
        [Display(Name = "所属年份")]
        public int Year { get; set; }

        /// <summary>
        /// 班级Id
        /// </summary>
        [NoExport]
        public string GradeID { get; set; }

        /// <summary>
        /// GradeName
        /// </summary>
        [Display(Name = "所属年级")]
        public string GradeName { get; set; }

        /// <summary>
        /// 所属类型ID
        /// </summary>
        [NoExport]
        public string CategoryID { get; set; }

        /// <summary>
        /// CategoryName
        /// </summary>
        [Display(Name = "所属类型")]
        public string CategoryName { get; set; }

        /// <summary>
        /// SubjectID
        /// </summary>
        [NoExport]
        public string SubjectID { get; set; }

        /// <summary>
        /// SubjectName
        /// </summary>
        [Display(Name = "所属科目")]
        public string SubjectName { get; set; }

        /// <summary>
        /// TermID
        /// </summary>
        [NoExport]
        public string TermID { get; set; }

        /// <summary>
        /// TermName
        /// </summary>
        [Display(Name = "所属期段")]
        public string TermName { get; set; }

        /// <summary>
        /// ClassTypeID
        /// </summary>
        [NoExport]
        public string ClassTypeID { get; set; }

        /// <summary>
        /// ClassTypeName
        /// </summary>
        [Display(Name = "所属班型")]
        public string ClassTypeName { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        [Display(Name = "创建日期")]
        public DateTime CreatedDate { get; set; }


        /// <summary>
        /// 修改时间
        /// </summary>
        [Display(Name = "更新日期")]
        public DateTime UpdateDate { get; set; }

      


    }
}

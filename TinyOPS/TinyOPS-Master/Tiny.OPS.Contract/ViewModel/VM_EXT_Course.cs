using System;
using System.ComponentModel.DataAnnotations;

namespace Tiny.OPS.Contract
{

    /// <summary>
    /// VM_EXT_Course
    /// </summary>
    public class VM_EXT_Course : VMEntityBase
    {
        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "系统来源")]
        public int FromSystem { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Guid TeachLevelOneOrgID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string TeachLevelOneOrgName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CourseID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CourseName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ProductTypeOneID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ProductTypeTwoID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int CourseYear { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string TermID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string TermName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string GradeID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string GradeName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ClassTypeID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ClassTypeName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string FlagID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string FlagName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string SubjectID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string SubjectName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CategoryID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CategoryName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal TotalClassHour { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string TotalClassHourName { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public decimal FeeUnitPrice { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string FeeUnitPriceName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int CourseStatus { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime ProductCreatedDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime ProductUpdateDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Describe { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Guid FKProductCourseGuid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int ExtractStatus { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long VersionNum { get; set; } = 0;

        /// <summary>
        /// 产品中心来源系统
        /// </summary>
        public int POCSource { get; set; }

        /// <summary>
        /// 产品大类名称
        /// </summary>
        public string ProductTypeName { get; set; }

        /// <summary>
        /// 授权-校区数
        /// </summary>
        public string CampusCountName { get; set; }
    }
}

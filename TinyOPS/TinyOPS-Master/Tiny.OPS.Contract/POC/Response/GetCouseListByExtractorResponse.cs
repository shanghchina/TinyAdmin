using Tiny.OPS.Domain.XGJProduct;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tiny.OPS.Contract
{
    /// <summary>
    /// 根据提取器信息获取课程信息返回信息
    /// </summary>
    public class GetCouseListByExtractorResponse
    {
        /// <summary>
        /// 总条数
        /// </summary>
        public int totalCount { get; set; }

        /// <summary>
        /// 返回信息
        /// </summary>
        public List<CourseItem> dataList { get; set; }
    }
    /// <summary>
    /// 课程信息信息
    /// </summary>
    public class CourseItem
    {

        public int Id { get; set; }
        public int FromSystem { get; set; }
        public Guid TeachLevelOneOrgID { get; set; }
        public string TeachLevelOneOrgName { get; set; }
        public string CourseID { get; set; }
        public string CourseName { get; set; }
        public string ProductTypeOneID { get; set; }
        public string ProductTypeTwoID { get; set; }
        public int CourseYear { get; set; }
        public string TermID { get; set; }
        public string TermName { get; set; }
        public string GradeID { get; set; }
        public string GradeName { get; set; }
        public string ClassTypeID { get; set; }
        public string ClassTypeName { get; set; }
        public string FlagID { get; set; }
        public string FlagName { get; set; }
        public string SubjectID { get; set; }
        public string SubjectName { get; set; }
        public string CategoryID { get; set; }
        public string CategoryName { get; set; }
        public decimal TotalClassHour { get; set; }
        public string TotalClassHourName { get; set; }
        public decimal FeeUnitPrice { get; set; }
        public string FeeUnitPriceName { get; set; }
        public int CourseStatus { get; set; }
        public string CourseStatusName { get; set; }

        public DateTime ProductCreatedDate { get; set; }
        public DateTime ProductUpdateDate { get; set; }
        public string Describe { get; set; }
        public Guid FKProductCourseGuid { get; set; }
        public int ExtractStatus { get; set; }
        public long VersionNum { get; set; } = 0;
        /// <summary>
        /// 产品中心来源系统
        /// </summary>
        public int POCSource { get; set; }

        /// <summary>
        /// 产品类型名称
        /// </summary>
        public string ProductTypeName { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdateDate { get; set; }

        /// <summary>
        /// 授权校区
        /// </summary>
        public string CampusCountName { get; set; }




    }


}

using Tiny.Common.Dapper.Enumeration;
using Tiny.Common.Dapper.Persistence.Data;
using System;

namespace Tiny.OPS.Domain.XGJProduct
{
    /// <summary>
    /// ProductCourse
    /// </summary>
    [Table("T_EXT_Course", DBName = EumDBName.POC)]
    public class T_EXT_Course : EntityBase
    {
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
        public long SyncHistoryID
        {
            set
            {
                if (History == null)
                    History = new T_EXT_SyncHistory();
                History.AutoIncrementId = value;
            }
            get { return History?.AutoIncrementId ?? 0; }
        }
        [NoMapper]
        public T_EXT_SyncHistory History { get; set; }
    }
}

using Tiny.Common.Dapper.Entity;
using Tiny.Common.Dapper.Enumeration;
using Tiny.Common.Dapper.Persistence.Data;
using System;

namespace Tiny.OPS.Domain.XGJProduct
{
    /// <summary>
    /// ProductCourseCampusRange
    /// </summary>
    [Table("T_EXT_CourseRange", DBName = EumDBName.POC)]
    public class T_EXT_CourseRange : EntityBase
    {
        public long ProductCourseID
        {
            set
            {
                if (ProductCourse == null)
                    ProductCourse = new T_EXT_Course();
                ProductCourse.AutoIncrementId = value;
            }
            get { return ProductCourse?.AutoIncrementId ?? 0; }
        }
        [NoMapper]
        public T_EXT_Course ProductCourse { get; set; }
        public string CourseID { get; set; }
        public string CampusID { get; set; }
        public Guid TeachNetOrgID { get; set; }
        public string TeachNetOrgName { get; set; }
        public int TeachOrgFinaUnitEOSID { get; set; }
        public decimal FeeUnitPrice { get; set; }
        public string FeePriceName { get; set; }
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

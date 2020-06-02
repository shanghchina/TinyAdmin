using Tiny.Common.Dapper.Enumeration;
using Tiny.Common.Dapper.Persistence.Data;
using System;

namespace Tiny.OPS.Domain.XGJProduct
{
    /// <summary>
    /// ProductClass
    /// </summary>
    [Table("T_EXT_Class", DBName = EumDBName.POC)]
    public class T_EXT_Class : EntityBase
    {
        public int FromSystem { get; set; }
        public Guid TeachLevelOneOrgID { get; set; }
        public string TeachLevelOneOrgName { get; set; }
        public string CampusID { get; set; }
        public Guid TeachNetOrgID { get; set; }
        public string TeachNetOrgName { get; set; }
        public int TeachOrgFinaUnitEOSID { get; set; }
        public string ClassID { get; set; }
        public string ClassName { get; set; }
        public string CourseID { get; set; }
        public int ClassYear { get; set; }
        public string TermID { get; set; }
        public string TermName { get; set; }
        public DateTime? OpenDate { get; set; }
        public DateTime? CloseDate { get; set; }
        public int MinStudentAmoun { get; set; }
        public int MaxStudentAmoun { get; set; }
        public decimal TotalClassHour { get; set; }
        public string TotalClassHourName { get; set; }
        public string ClassMasterUserID { get; set; }
        public string ClassMasterUserName { get; set; }
        public int ClassStatus { get; set; }
        public DateTime ProductCreatedDate { get; set; }
        public DateTime ProductUpdateDate { get; set; }
        public string Describe { get; set; }
        public DateTime? CourseStartTime { get; set; }
        public DateTime? CourseEndTime { get; set; }
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

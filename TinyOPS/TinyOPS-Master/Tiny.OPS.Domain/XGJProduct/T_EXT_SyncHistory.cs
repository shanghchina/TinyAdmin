using Tiny.Common.Dapper.Enumeration;
using Tiny.Common.Dapper.Persistence.Data;
using Tiny.OPS.Domain.Enum;
using System;

namespace Tiny.OPS.Domain.XGJProduct
{
    /// <summary>
    /// ProductSynchroHistory
    /// </summary>
    [Table("T_EXT_SyncHistory", DBName = EumDBName.POC)]
    public class T_EXT_SyncHistory : EntityBase
    {
        public int FromSystem { get; set; }
        public Guid TeachLevelOneOrgID { get; set; }
        public string TeachLevelOneOrgName { get; set; }
        public DateTime SynchroDate { get; set; }
        public SynchroDataType DataType { get; set; }
        public string DataAPIPath { get; set; }
        public string DataJson { get; set; }
        public SynchroStatus SyncStatus { get; set; }
        public long VersionNum { get; set; } = 0;
        /// <summary>
        /// 产品中心来源系统
        /// </summary>
        public int POCSource { get; set; }
        public string ErrorMessage { get; set; } = string.Empty;
    }
}

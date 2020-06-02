using Tiny.Common.Dapper.Enumeration;
using Tiny.Common.Dapper.Persistence.Data;
using System;

namespace Tiny.OPS.Domain.XGJProduct
{
    /// <summary>
    /// ProductItemType
    /// </summary>
    [Table("T_EXT_ItemType", DBName = EumDBName.POC)]
    public class T_EXT_ItemType : EntityBase
    {
        public int FromSystem { get; set; }
        public Guid TeachLevelOneOrgID { get; set; }
        public string TeachLevelOneOrgName { get; set; }
        public string ProductTypeID { get; set; }
        public string ProductTypeName { get; set; }
        public int NodeFlag { get; set; }
        public string ParentProductTypeID { get; set; }
        public int ItemTypeStatus { get; set; }
        public DateTime ProductCreatedDate { get; set; }
        public DateTime ProductUpdateDate { get; set; }
        public string Describe { get; set; }
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

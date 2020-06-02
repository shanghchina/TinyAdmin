using System;
using Tiny.Common.Dapper.Enumeration;
using Tiny.Common.Dapper.Persistence.Data;

namespace Tiny.OPS.Domain
{
    /// <summary>
    /// 产品中心--产品阈值表
    /// </summary>
    [Table("T_EXT_ThresholdValue", DBName = EumDBName.POC)]
    public class T_EXT_ThresholdValue : EntityBase
    {
        /// <summary>
        /// 来源系统
        /// </summary>
        public int FromSystem { get; set; }
        /// <summary>
        /// 所属一级组织ID
        /// </summary>
        public Guid ChargeLevelOneOrgID { get; set; }
        /// <summary>
        /// 产品ID
        /// </summary>
        public string ProductID { get; set; }
        /// <summary>
        /// 售卖日期
        /// </summary>
        public DateTime LastSaleDate { get; set; }

        /// <summary>
        /// 版本号
        /// </summary>
        public int VersionNum { get; set; } = 0;
    }
}

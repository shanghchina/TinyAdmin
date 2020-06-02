using Tiny.Common.Dapper.Enumeration;
using Tiny.Common.Dapper.Persistence.Data;
using System;

namespace Tiny.OPS.Domain
{
    /// <summary>
    /// 产品分类表
    /// </summary>
    [Table("T_POC_ProductType", DBName = EumDBName.POC)]
    public class T_POC_ProductType : EntityBase
    {
        /// <summary>
        /// ProductTypeGuid
        /// </summary>
        public Guid ProductTypeGuid { get; set; }
        /// <summary>
        /// ProductTypeName
        /// </summary>
        public string ProductTypeName { get; set; }
        /// <summary>
        /// ProductTypeLevelNo
        /// </summary>
        public int ProductTypeLevelNo { get; set; }
        /// <summary>
        /// ProductTypeLevelName
        /// </summary>
        public string ProductTypeLevelName { get; set; }
        /// <summary>
        /// ProductCount
        /// </summary>
        public int ProductCount { get; set; }
        /// <summary>
        /// ParentGuid
        /// </summary>
        public Guid ParentGuid { get; set; }
        /// <summary>
        /// 是否启用1启用 0 未启用
        /// </summary>
        public bool IsEnabled { get; set; }
        /// <summary>
        /// 校管家用户guid，如果是在ehs不存在的
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 修改人
        /// </summary>
        public string UpdaterUserId { get; set; }
        /// <summary>
        /// 修改人
        /// </summary>
        public string UpdaterUserName { get; set; }

    }
}


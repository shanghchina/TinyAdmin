using Tiny.Common.Dapper.Enumeration;
using Tiny.Common.Dapper.Persistence.Data;
using System;

namespace Tiny.OPS.Domain
{
    /// <summary>
    /// 产品分类映射表
    /// </summary>
    [Table("T_POC_ProductTypeMap", DBName = EumDBName.POC)]
    public class T_POC_ProductTypeMap : EntityBase
    {
        public Guid ProductTypeMapGuid { get; set; }

        public long FKItemTypeId { get; set; }

        public Guid FKProductTypeGuid { get; set; }

        public int ProductTypeLevelNo { get; set; }

        public string ProductTypeID { get; set; }

        public string ProductTypeTitle { get; set; }

        public string Remark { get; set; }

        public string UpdaterUserId { get; set; }

        public string UpdaterUserName { get; set; }
    }
}

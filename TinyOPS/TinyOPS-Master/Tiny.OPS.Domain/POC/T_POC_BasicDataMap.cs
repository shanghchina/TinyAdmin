using System;
using Tiny.Common.Dapper.Enumeration;
using Tiny.Common.Dapper.Persistence.Data;

namespace Tiny.OPS.Domain
{
    [Table("T_POC_BasicDataMap", DBName = EumDBName.POC)]
    public class T_POC_BasicDataMap : EntityBase
    {
        /// <summary>
        /// guid
        /// </summary>
        public Guid BasicDataMapGuid { get; set; }
        /// <summary>
        /// guid
        /// </summary>
        public Guid FKBasicDataGuid { get; set; }
        /// <summary>
        /// guid
        /// </summary>
        public Guid FKDictGuid { get; set; }
        /// <summary>
        /// 是否分类
        /// </summary>
        public bool SysIsCatalog { get; set; }
        /// <summary>
        /// 分类显示名称（名称加guid），eg:课程所属年级>一年级|guid1,guid2
        /// </summary>
        public string SysCatalogTitle { get; set; }
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

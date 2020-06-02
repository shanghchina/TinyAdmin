using Tiny.Common.Dapper.Enumeration;
using Tiny.Common.Dapper.Persistence.Data;
using System;

namespace Tiny.OPS.Domain
{

    [Table("T_POC_Extractor", DBName = EumDBName.POC)]
    public class T_POC_Extractor : EntityBase
    {
        /// <summary>
        /// guid
        /// </summary>
        public Guid ExtractorGuid { get; set; }
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

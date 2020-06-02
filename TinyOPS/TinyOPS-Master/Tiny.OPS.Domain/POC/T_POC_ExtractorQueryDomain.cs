using Tiny.Common.Dapper.Enumeration;
using Tiny.Common.Dapper.Persistence.Data;
using System;

namespace Tiny.OPS.Domain
{

    [Table("T_POC_ExtractorQuery", DBName = EumDBName.POC)]
    public class T_POC_ExtractorQuery : EntityBase
    {
        /// <summary>
        /// guid
        /// </summary>
        public Guid FKExtractorGuid { get; set; }
        /// <summary>
        /// guid
        /// </summary>
        public Guid ExtractorQueryGuid { get; set; }
        /// <summary>
        /// ExtFromSystem、OneOrg、ProductStatus、ThresholdValue
        /// </summary>
        public string SelectFieldTypeCode { get; set; }
        /// <summary>
        /// SelectFieldTypeId
        /// </summary>
        public string SelectFieldTypeId { get; set; }
        /// <summary>
        /// SelectFieldTypeName
        /// </summary>
        public string SelectFieldTypeName { get; set; }
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
	

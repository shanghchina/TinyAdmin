using Tiny.Common.Dapper.Enumeration;
using Tiny.Common.Dapper.Persistence.Data;
using System;

namespace Tiny.OPS.Domain
{

    [Table("T_POC_ExtractorDetail", DBName = EumDBName.POC)]
    public class T_POC_ExtractorDetail : EntityBase
    {
        /// <summary>
        /// guid
        /// </summary>
        public Guid FKExtractorGuid { get; set; }
        /// <summary>
        /// guid
        /// </summary>
        public Guid ExtractorDetailGuid { get; set; }
        /// <summary>
        /// 2100待提取 2200 待确认 2300已完成 2400已取消 2500提取中
        /// </summary>
        public int ExtractorStatus { get; set; }
        /// <summary>
        /// ExtractorNo
        /// </summary>
        public string ExtractorNo { get; set; }
        /// <summary>
        /// ProductCount
        /// </summary>
        public int ProductCount { get; set; }
        /// <summary>
        /// ExtractCount
        /// </summary>
        public int ExtractCount { get; set; }
        /// <summary>
        /// AssociatedCount
        /// </summary>
        public int AssociatedCount { get; set; }
        /// <summary>
        /// NotExtractCount
        /// </summary>
        public int NotExtractCount { get; set; }
        /// <summary>
        /// Year
        /// </summary>
        public int Year { get; set; }
        /// <summary>
        /// GradeID
        /// </summary>
        public string GradeID { get; set; }
        /// <summary>
        /// GradeName
        /// </summary>
        public string GradeName { get; set; }
        /// <summary>
        /// FlagID
        /// </summary>
        public string FlagID { get; set; }
        /// <summary>
        /// FlagName
        /// </summary>
        public string FlagName { get; set; }

        /// <summary>
        /// CategoryID
        /// </summary>
        public string CategoryID { get; set; }
        /// <summary>
        /// CategoryName
        /// </summary>
        public string CategoryName { get; set; }
        /// <summary>
        /// SubjectID
        /// </summary>
        public string SubjectID { get; set; }
        /// <summary>
        /// SubjectName
        /// </summary>
        public string SubjectName { get; set; }
        /// <summary>
        /// TermID
        /// </summary>
        public string TermID { get; set; }
        /// <summary>
        /// TermName
        /// </summary>
        public string TermName { get; set; }
        /// <summary>
        /// ClassTypeID
        /// </summary>
        public string ClassTypeID { get; set; }
        /// <summary>
        /// ClassTypeName
        /// </summary>
        public string ClassTypeName { get; set; }
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


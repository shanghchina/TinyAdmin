using Tiny.OPS.Domain.Enum;
using System;
using System.Collections.Generic;

namespace Tiny.OPS.Contract
{
    public class JobDataRequest
    {
        /// <summary>
        /// 选择日期
        /// </summary>
        public string QueryDate { get; set; }
        /// <summary>
        /// 系统来源
        /// </summary>
        public List<string> PocSource { get; set; }
        /// <summary>
        /// 事业部Id
        /// </summary>
        public List<Guid> LevelOneOrgID { get; set; }

        public SynchroDataType DataType { get; set; }
    }
}

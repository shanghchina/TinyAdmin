using System;
using System.Collections.Generic;

namespace Tiny.OPS.Contract
{
    /// <summary>
    /// 事业部产品池--来源系统产品体系--班级信息表 查询条件
    /// </summary>
    public class EXTClassRequest : SearchBase
    {
        /// <summary>
        /// 系统来源
        /// </summary>
        public List<string> PocSource { get; set; }

        /// <summary>
        /// 事业部Id
        /// </summary>
        public List<Guid> LevelOneOrgID { get; set; }

        /// <summary>
        /// 班级状态(0：无效、1：有效）
        /// </summary>
        public string ClassStatus { get; set; }

        /// <summary>
        /// 班级名称
        /// </summary>
        public string ClassName { get; set; }

        /// <summary>
        /// 是否导出
        /// </summary>
        public bool IsExport { get; set; }

    }
}

using System;
using System.Collections.Generic;

namespace Tiny.OPS.Contract
{
    /// <summary>
    /// 基础参数分页参数
    /// </summary>
    public class BasicDataPageRequest : BaseRequestPage
    {
        /// <summary>
        /// 系统来源
        /// </summary>
        public List<string> PocSource { get; set; }

        /// <summary>
        /// 事业部Id
        /// </summary>
        public List<Guid> OneOrgId { get; set; }

        /// <summary>
        /// 参数值
        /// </summary>
        public string DictValue { get; set; }

        /// <summary>
        /// 是否导出
        /// </summary>
        public bool IsExport { get; set; }
    }
}

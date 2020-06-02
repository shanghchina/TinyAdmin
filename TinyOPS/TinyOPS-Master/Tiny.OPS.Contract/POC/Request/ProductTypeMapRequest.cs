using System;
using System.Collections.Generic;

namespace Tiny.OPS.Contract
{
    /// <summary>
    /// 产品分类映射分页参数
    /// </summary>
    public class ProductTypeMapRequest : BaseRequestPage
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
        /// 是否映射(0是未映射，1是映射)
        /// </summary>
        public string IsMapping { get; set; }
    }
}

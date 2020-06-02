using System;
using System.Collections.Generic;
using System.Text;

namespace Tiny.OPS.Contract
{
    /// <summary>
    /// 校管家产品分类请求类
    /// </summary>
    public class ExtItemTypeRequest : BaseRequestPage
    {
        /// <summary>
        /// 系统来源
        /// </summary>
        public List<string> Source { get; set; }

        /// <summary>
        /// 事业部Id
        /// </summary>
        public List<Guid> LevelOneOrgID { get; set; }

        /// <summary>
        /// 分类状态(0是无效，1是有效)
        /// </summary>
        public string IsEffect { get; set; }

        /// <summary>
        /// 产品分类名称
        /// </summary>
        public string ProductItemName { get; set; }
    }
}

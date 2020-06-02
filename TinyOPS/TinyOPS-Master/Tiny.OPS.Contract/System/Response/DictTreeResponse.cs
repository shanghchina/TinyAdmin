using System.Collections.Generic;

namespace Tiny.OPS.Contract
{
    /// <summary>
    /// 字典表返回树节点内容
    /// </summary>
    public class DictTreeResponse
    {
        /// <summary>
        /// 节点唯一值
        /// </summary>
        public string value { get; set; }

        /// <summary>
        /// 节点显示名称
        /// </summary>
        public string label { get; set; }

        /// <summary>
        /// 子节点
        /// </summary>
        public List<DictTreeResponse> children { get; set; } = new List<DictTreeResponse>();

    }
}

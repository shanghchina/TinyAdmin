using System;
using System.Collections.Generic;
using System.Text;

namespace Tiny.OPS.Contract
{
    /// <summary>
    /// 获取参数请求
    /// </summary>
    public class GetVMDictionaryRequest
    {
        /// <summary>
        /// 子Guid string
        /// </summary>
        public string DictGuid { get; set; } = Guid.Empty.ToString();

        /// <summary>
        /// 父级Guid string
        /// </summary>
        public string ParentGuid { get; set; } = Guid.Empty.ToString();
    }
}

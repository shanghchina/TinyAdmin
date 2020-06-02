using System;
using System.Collections.Generic;
using System.Text;

namespace Tiny.OPS.Contract
{
    /// <summary>
    /// 字典请求信息
    /// </summary>
    public class GetDictionaryRequest
    {
        /// <summary>
        /// 字段Guid
        /// </summary>
        public List<Guid> DictGuid { get; set; } = new List<Guid>();

        /// <summary>
        /// 父级Guid
        /// </summary>
        public List<Guid> ParentGuid { get; set; } = new List<Guid>();
    }
}

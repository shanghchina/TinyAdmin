using System;
using System.Collections.Generic;
using System.Text;

namespace Tiny.OPS.Contract
{
    /// <summary>
    /// 获取当前类型的字典请求信息
    /// </summary>
    public class GetVMDictionaryResponse
    {
        ///// <summary>
        ///// 父级Guid
        ///// </summary>
        //public List<Guid> ParentGuid { get; set; } = new List<Guid>();

        /// <summary>
        /// 字典列表
        /// </summary>
        public List<VM_SYS_Dictionary> ReusltList { get; set; } = new List<VM_SYS_Dictionary>();
    }
}

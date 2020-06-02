using System;
using System.Collections.Generic;
using System.Text;

namespace Tiny.OPS.Contract
{
    /// <summary>
    /// 新增修改字典请求信息
    /// </summary>
    public class SaveVMDictionaryRequest
    {
        /// <summary>
        /// 父级Guid
        /// </summary>
        public string parentGuid { get; set; }

        /// <summary>
        /// 参数类型
        /// </summary>
        public string SysDictValue { get; set; }

        /// <summary>
        /// 参数描述
        /// </summary>
        public string SysDictDesc { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public int SysDictSort { get; set; }

        /// <summary>
        /// 父节点内容
        /// </summary>
        public VM_SYS_Dictionary vmParentDict { get; set; }

        /// <summary>
        /// 请求的字典内容
        /// </summary>
        public List<VM_SYS_Dictionary> selectOptions { get; set; } = new List<VM_SYS_Dictionary>();
    }
}

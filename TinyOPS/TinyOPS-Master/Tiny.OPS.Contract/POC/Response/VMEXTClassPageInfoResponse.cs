using System;
using System.Collections.Generic;
using System.Text;

namespace Tiny.OPS.Contract
{
    /// <summary>
    /// 来源系统产品体系--班级信息viewmodel
    /// </summary>
    public class VMEXTClassPageInfoResponse : SearchBase
    {
        /// <summary>
        /// 产品信息列表信息
        /// </summary>
        public IList<VM_EXT_Class> ReusltList { set; get; } = new List<VM_EXT_Class>();
    }
}

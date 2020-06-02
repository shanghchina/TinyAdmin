using Tiny.OPS.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tiny.OPS.Contract
{

    /// <summary>
    /// 产品信息分页返回信息
    /// </summary>
    public class VMPOCProductPageInfoResponse : SearchBase
    {
        /// <summary>
        /// 产品信息列表信息
        /// </summary>
        public IList<VM_POC_Product> ReusltList { set; get; } = new List<VM_POC_Product>();
    }
}

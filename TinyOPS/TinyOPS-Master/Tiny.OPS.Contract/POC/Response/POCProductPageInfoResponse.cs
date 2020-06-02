using Tiny.OPS.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tiny.OPS.Contract
{

    /// <summary>
    /// 产品信息分页返回信息
    /// </summary>
    public class POCProductPageInfoResponse : SearchBase
    {
        /// <summary>
        /// 产品信息列表信息
        /// </summary>
        public IList<T_POC_Product> ReusltList { set; get; } = new List<T_POC_Product>();
    }
}

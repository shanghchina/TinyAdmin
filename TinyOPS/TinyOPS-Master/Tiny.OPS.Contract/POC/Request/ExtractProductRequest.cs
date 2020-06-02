using System;
using System.Collections.Generic;
using System.Text;
using Tiny.OPS.Domain;

namespace Tiny.OPS.Contract
{
    /// <summary>
    /// 提取产品请求信息
    /// </summary>
    public class ExtractProductRequest
    {
        /// <summary>
        /// 提取的产品集合
        /// </summary>
        public List<T_POC_Product> productList { get; set; }

        /// <summary>
        /// 提取器关联总数量
        /// </summary>
       public int totalCount { get; set; }
             
    }
}

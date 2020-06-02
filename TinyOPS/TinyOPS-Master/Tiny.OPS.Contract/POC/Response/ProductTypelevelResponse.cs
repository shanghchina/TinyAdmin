using System.Collections.Generic;

namespace Tiny.OPS.Contract
{
    /// <summary>
    /// 产品分类层级
    /// </summary>
    public class ProductTypelevelResponse
    {
        /// <summary>
        /// 
        /// </summary>
        public string value { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string label { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<ProductTypelevelResponse> children { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Tiny.OPS.Contract.POC
{
    /// <summary>
    /// 产品分类下拉框请求类
    /// </summary>
    public class ProductTypeSelectListRequest
    {
        /// <summary>
        /// 分类Id
        /// </summary>
        public Guid ProductTypeGuid { get; set; }
    }
}

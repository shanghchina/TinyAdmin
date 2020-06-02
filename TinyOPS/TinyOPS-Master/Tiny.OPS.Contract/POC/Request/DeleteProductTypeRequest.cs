using System;
using System.Collections.Generic;
using System.Text;

namespace Tiny.OPS.Contract
{
    /// <summary>
    /// 删除产品类别请求类
    /// </summary>
    public class DeleteProductTypeRequest
    {
        /// <summary>
        /// 产品分类表Guid
        /// </summary>
        public Guid ProductTypeGuid { get; set; }
    }
}

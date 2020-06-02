using System;
using System.Collections.Generic;
using System.Text;

namespace Tiny.OPS.Contract
{
    /// <summary>
    /// 产品分类映射列表请求类
    /// </summary>
    public class ProductTypeMappingListRequest: BaseRequestPage
    {
        /// <summary>
        /// 产品分类Id
        /// </summary>
        public Guid ProductTypeId { get; set; }
    }
}

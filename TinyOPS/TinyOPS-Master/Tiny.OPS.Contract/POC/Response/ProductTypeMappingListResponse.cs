using System;
using System.Collections.Generic;
using System.Text;

namespace Tiny.OPS.Contract
{
    /// <summary>
    /// 产品分类映射列表返回类
    /// </summary>
    public class ProductTypeMappingListResponse
    {
        /// <summary>
        /// 产品映射集合
        /// </summary>
        public List<ProductTypeMapping> DataList { get; set; }
        /// <summary>
        /// 总数
        /// </summary>
        public int Total { get; set; }
    }
    /// <summary>
    /// 产品分类映射
    /// </summary>
    public class ProductTypeMapping
    {
        /// <summary>
        /// 系统来源
        /// </summary>
        public string FromSystem { get; set; }

        /// <summary>
        /// 事业部名称
        /// </summary>
        public string TeachLevelOneOrgName { get; set; }
        /// <summary>
        /// 产品分类名称
        /// </summary>
        public string ProductTypeName { get; set; }
        /// <summary>
        /// 产品分类级别
        /// </summary>
        public int NodeFlag { get; set; }
        /// <summary>
        /// 映射时间
        /// </summary>
        public string MappingTime { get; set; }
    }
}

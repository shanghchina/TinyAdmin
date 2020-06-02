using System;
using System.Collections.Generic;

namespace Tiny.OPS.Contract
{
    /// <summary>
    /// 产品分类映射列表
    /// </summary>
    public class ProductTypePageInfoResponse
    {
        /// <summary>
        /// 基础参数集合
        /// </summary>
        public List<ProductTypePageInfo> DataList { get; set; }
        /// <summary>
        /// 总数
        /// </summary>
        public int Total { get; set; }
    }

    /// <summary>
    /// 产品分类映射
    /// </summary>
    public class ProductTypePageInfo
    {
        /// <summary>
        /// Id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 产品分类映射表Guid
        /// </summary>
        public Guid ProductTypeMapGuid { get; set; }

        /// <summary>
        /// 来源系统的产品大类id
        /// </summary>
        public string ProductTypeID { get; set; }

        /// <summary>
        /// Poc系统来源
        /// </summary>
        public string PocSource { get; set; }

        /// <summary>
        /// 所属一级事业部
        /// </summary>
        public string LevelOneOrgName { get; set; }

        /// <summary>
        /// 产品分类名称
        /// </summary>
        public string ProductTypeName { get; set; }

        /// <summary>
        /// 分类级数
        /// </summary>
        public int NodeFlag { get; set; }

        /// <summary>
        /// 是否映射
        /// </summary>
        public string IsMapping { get; set; }

        /// <summary>
        /// 映射产品分类名称
        /// </summary>
        public string ProductTypeTitle { get; set; }

        /// <summary>
        /// 映射时间
        /// </summary>
        public DateTime? MappingDateDate { get; set; }

        /// <summary>
        /// 是否显示
        /// </summary>
        public bool Ishow { get; set; } = false;
    }
}

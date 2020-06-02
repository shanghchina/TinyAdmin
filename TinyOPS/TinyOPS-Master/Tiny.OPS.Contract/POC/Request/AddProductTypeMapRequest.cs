using System;

namespace Tiny.OPS.Contract
{
    /// <summary>
    /// 新增或跟新产品分类映射
    /// </summary>
    public class AddProductTypeMapRequest
    {
        /// <summary>
        /// 产品分类映射表Guid
        /// </summary>
        public Guid ProductTypeMapGuid { get; set; }

        /// <summary>
        /// 产品分类来源表Id
        /// </summary>
        public long FKItemTypeId { get; set; }

        /// <summary>
        /// 产品分类表Guid
        /// </summary>
        public Guid FKProductTypeGuid { get; set; }

        /// <summary>
        /// 分类级别数
        /// </summary>
        public int ProductTypeLevelNo { get; set; }

        /// <summary>
        /// 来源系统的产品大类id
        /// </summary>
        public string ProductTypeID { get; set; }

        /// <summary>
        /// 映射显示名称
        /// </summary>
        public string ProductTypeTitle { get; set; }

        /// <summary>
        /// 修改人Id
        /// </summary>
        public string UpdaterUserId { get; set; }

        /// <summary>
        /// 修改人姓名
        /// </summary>
        public string UpdaterUserName { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedDate { get; set; }
    }
}

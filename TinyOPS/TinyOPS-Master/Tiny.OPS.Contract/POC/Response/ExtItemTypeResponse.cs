using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Tiny.OPS.Contract
{
    /// <summary>
    /// 校管家产品分类返回类
    /// </summary>
    public class ExtItemTypeResponse
    { /// <summary>
      /// 校管家产品分类集合
      /// </summary>
        public List<ItemType> DataList { get; set; }
        /// <summary>
        /// 总数
        /// </summary>
        public int Total { get; set; }
    }

    /// <summary>
    /// 校管家产品分类
    /// </summary>
    public class ItemType
    {
        /// <summary>
        /// 系统来源
        /// </summary>
        [Display(Name = "系统来源")]
        public string PocSourceName { get; set; }
        /// <summary>
        /// 所属事业部
        /// </summary>
        [Display(Name = "所属事业部")]
        public string TeachLevelOneOrgName { get; set; }
        /// <summary>
        /// 产品分类名称
        /// </summary>
        [Display(Name = "产品分类名称")]
        public string ProductTypeName { get; set; }
        /// <summary>
        /// 分类级数
        /// </summary>
        [Display(Name = "分类级数")]
        public int NodeFlag { get; set; }
        /// <summary>
        /// 状态(0：无效、1：有效）
        /// </summary>
        [Display(Name = "产品分类状态")]
        public string ItemTypeStatus { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [Display(Name = "创建时间")]
        public DateTime CreatedDate { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        [Display(Name = "更新时间")]
        public DateTime UpdateDate { get; set; }
    }
}

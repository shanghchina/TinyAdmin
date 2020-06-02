using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Tiny.OPS.Contract
{
    /// <summary>
    /// 基础参数分页返回值
    /// </summary>
    public class BasicDataPageResponse
    {
        /// <summary>
        /// 基础参数集合
        /// </summary>
        public List<BasicDataPage> DataList { get; set; }
        /// <summary>
        /// 总数
        /// </summary>
        public int Total { get; set; }
    }

    /// <summary>
    /// 基础参数分页返回值
    /// </summary>
    public class BasicDataPage
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
        public string OneOrgName { get; set; }
        /// <summary>
        /// 参数类型
        /// </summary>
        [Display(Name = "参数类型")]
        public string DictTypeName { get; set; }
        /// <summary>
        /// 参数值
        /// </summary>
        [Display(Name = "参数值")]
        public string DictValue { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [Display(Name = "创建时间")]
        public DateTime DictCreatetime { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        [Display(Name = "更新时间")]
        public DateTime DictUpdatetime { get; set; }
    }
}

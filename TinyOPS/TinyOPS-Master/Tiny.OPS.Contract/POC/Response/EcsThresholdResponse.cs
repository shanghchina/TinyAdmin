using System;
using System.Collections.Generic;

namespace Tiny.OPS.Contract
{
    /// <summary>
    /// 阈值响应
    /// </summary>
    public class EcsThresholdResponse
    {
        /// <summary>
        /// 接收是否成功状态
        /// </summary>
        public bool IsSuccess { get; set; }
        /// <summary>
        /// 反馈信息具体内容
        /// </summary>
        public string AlertMessage { get; set; }
        /// <summary>
        /// 符合条件的数据列表
        /// </summary>
        public List<ThresholdList> ResultInfo { get; set; }
    }

    /// <summary>
    /// ThresholdList
    /// </summary>
    public class ThresholdList
    {
        /// <summary>
        /// 系统来源ID
        /// </summary>
        public string FromSystem { get; set; }
        /// <summary>
        /// 所属一级组织ID 
        /// </summary>
        public Guid ChargeLevelOneOrgID { get; set; }
        /// <summary>
        /// 产品ID 
        /// </summary>
        public string ProductID { get; set; }
        /// <summary>
        /// 最后一次销售日期 
        /// </summary>
        public DateTime LastSaleDate { get; set; }
    }
}

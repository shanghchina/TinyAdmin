using System.ComponentModel;

namespace Tiny.OPS.Domain
{
    /// <summary>
    /// 一般状态（产品分类\班级状态）
    /// </summary>
    public enum GeneralState
    {
        /// <summary>
        /// 有效
        /// </summary>
        [Description("有效")]
        Effective = 1000,
        /// <summary>
        /// 无效
        /// </summary>
        [Description("无效")]
        Invalid = 2000,
        /// <summary>
        /// 全部
        /// </summary>
        [Description("全部")]
        All = 3000
    }

    /// <summary>
    /// 提取器状态
    /// </summary>
    public enum ExtractorState
    {
        /// <summary>
        /// 待提取 
        /// </summary>
        [Description("待提取")]
        ToBeExtracted = 2100,
        ///// <summary>
        ///// 待确认
        ///// </summary>
        //[Description("待确认")]
        //ToBeConfirmed = 2000,
        /// <summary>
        /// 提取完成
        /// </summary>
        [Description("提取完成")]
        Extracted = 2300,
        /// <summary>
        /// 已取消
        /// </summary>
        [Description("已取消")]
        Cancelled = 2400
    }

    /// <summary>
    /// 提取状态
    /// </summary>
    public enum ExtractState
    {
        /// <summary>
        /// 未提取
        /// </summary>
        [Description("未提取")]
        Unextracted = 1000,
        /// <summary>
        /// 已提取
        /// </summary>
        [Description("已提取")]
        Extracted = 2000,
        /// <summary>
        /// 已关联
        /// </summary>
        [Description("已关联")]
        Associated = 3000
    }

    /// <summary>
    /// 产品动销阈值
    /// </summary>
    public enum ThresholdValue
    {
        /// <summary>
        /// 不限
        /// </summary>
        [Description("不限")]
        NoLimit = 1000,
        /// <summary>
        /// 三个月内
        /// </summary>
        [Description("三个月内")]
        ThreeMonths = 2000,
        /// <summary>
        /// 6个月内
        /// </summary>
        [Description("6个月内")]
        SixMonths = 3000,
        /// <summary>
        /// 一年内
        /// </summary>
        [Description("一年内")]
        OneYear = 4000,
        /// <summary>
        /// 两年内
        /// </summary>
        [Description("两年内")]
        TwoYear = 5000
    }
}

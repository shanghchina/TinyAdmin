using System.ComponentModel;

namespace Tiny.OPS.Domain
{
    /// <summary>
    /// 来源系统产品体系--课程信息表 提取状态
    /// 提取状态 1000未提取 2000已提取 3000已关联
    /// </summary>
    public enum EXTCourseExtractStatusEnum
    {
        [Description("未提取")]
        WaitExtract = 1000,
        [Description("已提取")]
        HasExtract = 2000,
        [Description("已关联")]
        HasAssociated = 3000
    }
}

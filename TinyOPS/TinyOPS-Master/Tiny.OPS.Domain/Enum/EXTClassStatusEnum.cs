using System.ComponentModel;

namespace Tiny.OPS.Domain
{
    /// <summary>
    /// 班级状态(0：无效、1：有效）
    /// </summary>
    public enum EXTClassStatusEnum
    {
        [Description("无效")]
        Disabled = 0,
        [Description("有效")]
        Enabled = 1
    }
}

using System.ComponentModel;

namespace Tiny.OPS.Domain
{
    /// <summary>
    /// 课程状态(-1：已删除、0：无效、1：有效）
    /// </summary>
    public enum CourseStatusEnum
    {
        [Description("已删除")]
        Deleted = -1,
        [Description("无效")]
        Disabled = 0,
        [Description("有效")]
        Enabled = 1,
        [Description("全部")]
        All = -2,
    }
}

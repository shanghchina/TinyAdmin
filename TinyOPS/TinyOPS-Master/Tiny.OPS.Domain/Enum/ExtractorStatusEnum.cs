using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Tiny.OPS.Domain
{
    public enum ExtractorStatusEnum
    {
        [Description("待提取")]
        WaitExtract = 2100,
        //[Description("待确认")]
        //WaitConfirm = 2200,
        [Description("已完成")]
        Completed = 2300,
        [Description("已取消")]
        Cancelled = 2400,
        //[Description("提取中")]
        //Extracting = 2500
    }
}

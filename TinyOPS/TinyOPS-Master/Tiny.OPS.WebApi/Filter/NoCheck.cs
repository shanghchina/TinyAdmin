using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tiny.OPS.WebApi.Filter
{
    /// <summary>
    /// 无需验证,加在方法上，可以不验证此方法
    /// </summary>
    public class NoCheck : Attribute
    {
    }
}

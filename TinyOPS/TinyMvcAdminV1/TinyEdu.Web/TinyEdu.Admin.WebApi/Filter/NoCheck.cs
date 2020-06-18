using System;

namespace TinyEdu.Admin.WebApi
{
    /// <summary>
    /// 无需验证,加在方法上，可以不验证此方法
    /// </summary>
    public class NoCheck : Attribute
    {
    }
}

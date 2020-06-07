using System;
using System.Collections.Generic;
using System.Text;

namespace Tiny.OPS.Contract
{
    /// <summary>
    /// 返回
    /// </summary>
    public class Rsp_Auth_User
    {
        /// <summary>
        /// user
        /// </summary>
        public Vm_Sys_User user { get; set; } = new Vm_Sys_User();

        /// <summary>
        /// token
        /// </summary>
        public string token { get; set; }
    }
}

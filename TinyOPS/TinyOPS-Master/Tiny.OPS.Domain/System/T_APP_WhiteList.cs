using Tiny.Common.Dapper.Enumeration;
using System;
using System.Collections.Generic;
using System.Text;
using Tiny.Common.Dapper.Persistence.Data;

namespace Tiny.OPS.Domain
{
    /// <summary>
    /// 系统白名单表
    /// </summary>
    [Table("T_APP_WhiteList", DBName = EumDBName.POC)]
    public class T_APP_WhiteList : EntityBase
    {
        /// <summary>
        /// Ip地址
        /// </summary>
        public string Ip { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnabled { get; set; } = true;
    }
}

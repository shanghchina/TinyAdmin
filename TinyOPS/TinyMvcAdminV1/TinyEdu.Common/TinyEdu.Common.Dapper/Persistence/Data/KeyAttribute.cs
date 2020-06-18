using System;
using System.Collections.Generic;
using System.Text;

namespace TinyEdu.Common.Dapper.Persistence.Data
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class KeyAttribute : Attribute
    {
        /// <summary>
        /// 是否忽略主键字段
        /// </summary>
        public bool IsIgnore { get; set; } = true;
    }
}

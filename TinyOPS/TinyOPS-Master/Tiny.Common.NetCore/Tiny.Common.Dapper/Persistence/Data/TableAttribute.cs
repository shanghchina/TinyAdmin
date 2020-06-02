using Tiny.Common.Dapper.Enumeration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tiny.Common.Dapper.Persistence.Data
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class TableAttribute : Attribute
    {
        public TableAttribute(string name)
        {
            TableName = name;
        }
        /// <summary>
        /// 表名
        /// </summary>
        public string TableName { set; get; }

        /// <summary>
        /// 当前实体类对应的 数据库名
        /// </summary>
        public EumDBName DBName { set; get; }
    }
}

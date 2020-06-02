using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Tiny.Common.Dapper.Persistence.Data
{
    /// <summary>
    /// 参数对象
    /// </summary>
    public class DataParameterInfo
    {
        /// <summary>
        /// 输入、输出、双向还是返回值
        /// </summary>
        public ParameterDirection Direction { set; get; }
        /// <summary>
        /// 参数类型
        /// </summary>
        public DbType DbType { set; get; }
        /// <summary>
        /// 是否SqlDbType
        /// </summary>
        public bool IsSqlDbType { set; get; } = false;
        /// <summary>
        /// Sql参数类型
        /// </summary>
        public SqlDbType SqlDbType { set; get; }
        /// <summary>
        /// 参数名
        /// </summary>
        public string ParameterName { set; get; }
        /// <summary>
        /// 值
        /// </summary>
        public object Value { set; get; }
        /// <summary>
        /// 是否可接受Null
        /// </summary>
        public bool IsNullable { set; get; }
        /// <summary>
        /// 精度
        /// </summary>
        public byte Precision { set; get; }
        /// <summary>
        /// 小数精度
        /// </summary>
        public byte Scale { set; get; }
        /// <summary>
        /// 参数大小
        /// </summary>
        public int Size { set; get; }
    }
}

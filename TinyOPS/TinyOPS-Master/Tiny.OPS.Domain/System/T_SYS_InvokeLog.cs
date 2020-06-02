using Tiny.Common.Dapper.Enumeration;
using Tiny.Common.Dapper.Persistence.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tiny.OPS.Domain
{
    /// <summary>
    /// 调用日志
    /// </summary>
    [Table("T_SYS_InvokeLog", DBName = EumDBName.POC)]
    public class T_SYS_InvokeLog : EntityBase
    {
        /// <summary>
        /// 调用方IP
        /// </summary>
        public string InvokeIP { get; set; }
        /// <summary>
        /// 调用链接
        /// </summary>
        public string InvokeURL { get; set; }
        /// <summary>
        /// 调用方法
        /// </summary>
        public string InvokeFunction { get; set; }
        /// <summary>
        /// 调用对象
        /// </summary>
        public string InvokeJson { get; set; }
        /// <summary>
        /// 返回对象
        /// </summary>
        public string ReturnJson { get; set; } = string.Empty;
        /// <summary>
        /// 耗时(毫秒）
        /// </summary>
        public double UseTime { get; set; }

        [NoMapper]
        private Dictionary<int, T_SYS_InvokeLog> _LogList { get; set; } = new Dictionary<int, T_SYS_InvokeLog>();
        public T_SYS_InvokeLog GetLog(int HashCode)
        {
            if (!_LogList.ContainsKey(HashCode))
                _LogList.Add(HashCode, new T_SYS_InvokeLog());
            return _LogList[HashCode];
        }

        public void DeleteLog(int HashCode)
        {
            _LogList.Remove(HashCode);
        }
    }
}

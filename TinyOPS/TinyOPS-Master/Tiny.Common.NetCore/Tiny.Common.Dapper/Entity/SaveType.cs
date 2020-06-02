using System;
using System.Collections.Generic;
using System.Text;

namespace Tiny.Common.Dapper.Entity
{
    /// <summary>
    /// 操作类型
    /// </summary>
    public enum SaveType
    {
        /// <summary>
        /// 无操作
        /// </summary>
        None = 0,
        /// <summary>
        /// 添加
        /// </summary>
        Add = 1,
        /// <summary>
        /// 修改
        /// </summary>
        Modify = 2,
        /// <summary>
        /// 移除
        /// </summary>
        Remove = 4,
        /// <summary>
        /// 执行
        /// </summary>
        Excute = 8,
        /// <summary>
        /// 执行实体参数
        /// </summary>
        ExcuteT = 16,
    }
}

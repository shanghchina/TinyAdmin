using System;
using System.Collections.Generic;
using System.Text;

namespace Tiny.OPS.Contract
{
    /// <summary>
    /// 产品分类启用开关请求类
    /// </summary>
    public class OperateSwitchRequest
    {
        /// <summary>
        /// 产品分类表Guid
        /// </summary>
        public Guid ProductTypeGuid { get; set; }

        /// <summary>
        /// 启用状态
        /// </summary>
        public bool IsEnabled { get; set; }
    }
}

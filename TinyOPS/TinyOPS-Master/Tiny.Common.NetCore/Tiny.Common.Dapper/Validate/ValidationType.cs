using System;
using System.Collections.Generic;
using System.Text;

namespace Tiny.Common.Dapper.Validate
{
    public enum ValidationType
    {
        /// <summary>
        /// 添加时验证
        /// </summary>
        Add = 1,
        /// <summary>
        /// 修改时验证
        /// </summary>
        Modify = 2,
        /// <summary>
        /// 删除时验证
        /// </summary>
        Remove = 4
    }
}

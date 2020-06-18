using System;
using System.Collections.Generic;
using System.Text;

namespace TinyEdu.Common.Dapper.Validate
{
    [Serializable]
    public class ValidationInfo
    {
        /// <summary>
        /// 属性名称
        /// </summary>
        public virtual string PropertName { get; set; }
        /// <summary>
        /// 正则表达式验证
        /// </summary>
        public virtual IList<RuleInfo> Rules { get; set; }
        /// <summary>
        /// 错误提示
        /// </summary>
        public virtual string Message { get; set; }
    }
}

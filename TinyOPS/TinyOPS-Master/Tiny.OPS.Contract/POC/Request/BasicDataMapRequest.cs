using System;
using System.Collections.Generic;
using System.Text;

namespace Tiny.OPS.Contract
{
    /// <summary>
    /// 基础参数映射请求类
    /// </summary>
    public class BasicDataMapRequest:BaseRequestPage
    {
        /// <summary>
        /// 系统来源
        /// </summary>
        public List<string> Source { get; set; }

        /// <summary>
        /// 事业部Id
        /// </summary>
        public List<Guid> LevelOneOrgID { get; set; }

        /// <summary>
        /// 是否映射(0是未映射，1是映射)
        /// </summary>
        public string IsMapping { get; set; }
    }
}

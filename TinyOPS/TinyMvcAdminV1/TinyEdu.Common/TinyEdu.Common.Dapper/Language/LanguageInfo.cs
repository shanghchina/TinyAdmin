using System;
using System.Collections.Generic;
using System.Text;

namespace TinyEdu.Common.Dapper.Language
{
    [Serializable]
    public class LanguageInfo
    {
        /// <summary>
        /// 枚举值，非枚举返回0
        /// </summary>
        public int Value { set; get; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; }

    }
}

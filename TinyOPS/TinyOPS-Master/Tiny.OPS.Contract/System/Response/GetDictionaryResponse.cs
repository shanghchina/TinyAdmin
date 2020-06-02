using System;
using System.Collections.Generic;
using System.Text;

namespace Tiny.OPS.Contract
{
    /// <summary>
    /// 字段返回值
    /// </summary>
    public class GetDictionaryResponse
    {
        /// <summary>
        /// 字段集合
        /// </summary>
        public List<DictionaryItem> dataList { get; set; }

    }
    /// <summary>
    /// 字段data
    /// </summary>
    public class DictionaryItem
    {
        /// <summary>
        /// Guid
        /// </summary>
        public Guid DictGuid { get; set; }

        /// <summary>
        /// 父级Guid
        /// </summary>
        public Guid ParentGuid { get; set; }

        /// <summary>
        /// 字典key
        /// </summary>
        public string SysDictKey { get; set; }

        /// <summary>
        ///字典Value
        /// </summary>
        public string SysDictValue { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string SysDictDesc { get; set; }
    }
}

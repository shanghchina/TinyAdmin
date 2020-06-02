using System;
using System.Collections.Generic;
using System.Text;

namespace Tiny.OPS.Contract
{
    /// <summary>
    /// 查询条件基类
    /// </summary>
    public class SearchBase
    {
        /// <summary>
        /// 当前页
        /// </summary>
        public int PageIndex { get; set; } = 1;

        /// <summary>
        /// 每页行数
        /// </summary>
        public int PageSize { get; set; } = 20;

        /// <summary>
        /// 总共行数
        /// </summary>
        public int RecordCount = 0;

        /// <summary>
        ///表唯一ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 关键字
        /// </summary>
        public string KeyWords { get; set; }

        /// <summary>
        /// 开始创建日期
        /// </summary>
        public string StartTime { get; set; }

        /// <summary>
        /// 结束 创建日期
        /// </summary>
        public string EndTime { get; set; }

        /// <summary>
        /// 排序字段
        /// </summary>
        public string SortName { get; set; }

        /// <summary>
        /// 排序顺序
        /// </summary>
        public string SortOrder { get; set; } = "ASC";
    }
}

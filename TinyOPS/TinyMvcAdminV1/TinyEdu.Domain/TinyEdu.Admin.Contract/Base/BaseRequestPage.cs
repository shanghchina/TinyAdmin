using System;
using System.Collections.Generic;
using System.Text;

namespace TinyEdu.Admin.Contract
{
    /// <summary>
    /// 
    /// </summary>
    public class BaseRequestPage
    {
        /// <summary>
        /// 是否分页
        /// </summary>
        public bool Pagination { get; set; } = true;
        /// <summary>
        /// 页码
        /// </summary>
        public int PageIndex { get; set; } = 1;
        /// <summary>
        /// 每页行数
        /// </summary>
        public int PageSize { get; set; } = 20;

        /// <summary>
        /// 排序
        /// </summary>
        public string OrderBy { get; set; }
    }
}

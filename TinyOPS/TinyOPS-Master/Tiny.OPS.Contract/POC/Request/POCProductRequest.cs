using Tiny.OPS.Domain;
using System;
using System.Collections.Generic;

namespace Tiny.OPS.Contract
{

    /// <summary>
    /// 产品信息查询条件
    /// </summary>
    public class POCProductRequest : SearchBase
    {
        /// <summary>
        /// 产品分类Guid
        /// </summary>
        public Guid ProductTypeGuid { get; set; } = Guid.Empty;
        
        ///// <summary>
        ///// 选择中的产品分类列表
        ///// </summary>
        //public List<DictTreeResponse> CheckedTreeNodes { get; set; } = new List<DictTreeResponse>();

        /// <summary>
        /// 选择中的产品分类的guid值
        /// </summary>
        public List<string> CheckedNodeGuids { get; set; }

        /// <summary>
        /// 产品状态 =课程状态(-1：已删除、0：无效、1：有效）
        /// </summary>
        public CourseStatusEnum CourseStatus { get; set; }

        /// <summary>
        /// 产品名称=来源系统的课程名称
        /// </summary>
        public string CourseName { get; set; }
    }
}

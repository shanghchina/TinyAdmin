using System;
using System.Collections.Generic;
using System.Text;

namespace Tiny.OPS.Contract
{
    /// <summary>
    /// 产品分类映射层级详情
    /// </summary>
    public class ProductTypeLevelAllInfoResponse
    {
        /// <summary>
        /// Id
        /// </summary>
        public long id { get; set; }
        /// <summary>
        /// 产品分类Id
        /// </summary>
        public Guid productTypeGuid { get; set; }
        /// <summary>
        /// 分类名称
        /// </summary>
        public string className { get; set; }
        /// <summary>
        /// 级别名称
        /// </summary>
        public string level { get; set; }
        /// <summary>
        /// 级别序号（后续前端判断是否末级，末级不能添加子级，末级是3）
        /// </summary>
        public int levelSort { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool isStart { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string remark { get; set; }
        /// <summary>
        /// 上级分类Id
        /// </summary>

        public Guid parentGuid { get; set; }
        /// <summary>
        /// 父级名称
        /// </summary>
        public string parentName { get; set; }

        /// <summary>
        /// 层级显示
        /// </summary>

        public List<string> showLevel { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>

        public DateTime createdDate { get; set; }
        /// <summary>
        /// 子级
        /// </summary>
        public List<ProductTypeLevelAllInfoResponse> children { get; set; }
    }
}

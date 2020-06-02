using System;

namespace Tiny.OPS.Contract
{
    /// <summary>
    /// 
    /// </summary>
    public class VMEntityBase 
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        public long Id { set; get; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedDate { set; get; } = DateTime.Now;
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? UpdateDate { set; get; }
    }
}

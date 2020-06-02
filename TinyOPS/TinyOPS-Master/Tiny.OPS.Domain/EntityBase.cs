using System;
using Tiny.Common.Dapper.Entity;
using Tiny.Common.Dapper.Persistence.Data;

namespace Tiny.OPS.Domain
{
    public class EntityBase : BaseInfo
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        [Key]
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

using System;
using System.Collections.Generic;
using System.Text;
using TinyEdu.Common.Dapper.Entity;
using TinyEdu.Common.Dapper.Persistence.Data;

namespace TinyEdu.Admin.Domain
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

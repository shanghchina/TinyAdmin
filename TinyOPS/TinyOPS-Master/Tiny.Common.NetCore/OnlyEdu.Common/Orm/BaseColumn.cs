using System;

namespace OnlyEdu.Common.Orm
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class BaseColumn
    {
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime TimeCreate { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime TimeUpdate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        protected BaseColumn ()
        {
            this.TimeCreate=DateTime.Now;
            this.TimeUpdate=DateTime.Now;
            
        }
    }
}

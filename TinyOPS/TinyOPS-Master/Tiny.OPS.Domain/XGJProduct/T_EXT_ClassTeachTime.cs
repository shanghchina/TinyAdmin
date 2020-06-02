using Tiny.Common.Dapper.Enumeration;
using Tiny.Common.Dapper.Persistence.Data;
using System;

namespace Tiny.OPS.Domain.XGJProduct
{
    /// <summary>
    /// ProductClassTeachTime
    /// </summary>
    [Table("T_EXT_ClassTeachTime", DBName = EumDBName.POC)]
    public class T_EXT_ClassTeachTime : EntityBase
    {
        public long ProductClassID
        {
            set
            {
                if (ProductClass == null)
                    ProductClass = new T_EXT_Class();
                ProductClass.AutoIncrementId = value;
            }
            get { return ProductClass?.AutoIncrementId ?? 0; }
        }
        [NoMapper]
        public T_EXT_Class ProductClass { get; set; }
        public string ClassID { get; set; }
        public int WeekDay { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public Guid TeacherUserID { get; set; }
        public string TeacherUserName { get; set; }
        public string ClassroomID { get; set; }
        public string ClassroomName { get; set; }
        public long VersionNum { get; set; } = 0;
        /// <summary>
        /// 产品中心来源系统
        /// </summary>
        public int POCSource { get; set; }
        public long SyncHistoryID
        {
            set
            {
                if (History == null)
                    History = new T_EXT_SyncHistory();
                History.AutoIncrementId = value;
            }
            get { return History?.AutoIncrementId ?? 0; }
        }
        [NoMapper]
        public T_EXT_SyncHistory History { get; set; }
    }
}

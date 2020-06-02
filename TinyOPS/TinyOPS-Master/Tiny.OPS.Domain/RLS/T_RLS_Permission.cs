using Tiny.Common.Dapper.Enumeration;
using Tiny.Common.Dapper.Persistence.Data;
using Tiny.Common.Types;

namespace Tiny.OPS.Domain
{
    [Table("T_RLS_Permission",DBName = EumDBName.POC)]
    public class T_RLS_Permission : EntityBase
    {
        /// <summary>
        /// 权限点ID
        /// </summary>
        public long PermissionsID { get; set; }
        /// <summary>
        /// 所属系统ID
        /// </summary>
        public DomainType DomainTypeID { get; set; }
        /// <summary>
        ///权限点名称
        /// </summary>
        public string PermissionsName { get; set; }
        /// <summary>
        /// 权限点父级ID
        /// </summary>
        public long PermissionsParentID { get; set; }
        /// <summary>
        /// 状态：1，有效，2无效
        /// </summary>
        public RowStateType RowStateID { get; set; }
        /// <summary>
        /// 自定义属性
        /// </summary>
        public string PermissionsAttributes { get; set; }
    }
}

using Tiny.Common.Dapper.Enumeration;
using Tiny.Common.Dapper.Persistence.Data;
using Tiny.Common.Types;
namespace Tiny.OPS.Domain
{
    [Table("T_RLS_RolePermission",DBName = EumDBName.POC)]
    public class T_RLS_RolePermission : EntityBase
    {
        /// <summary>
        /// 角色ID
        /// </summary>
        public long RoleID { get; set; }
        /// <summary>
        /// 权限ID
        /// </summary>
        public long PermissionsID { get; set; }
        /// <summary>
        /// 权限授予状态：1000 授予, 2000拒绝，3000 移除，取值为1000的数据
        /// </summary>
        public AuthorizationStateType AuthorizationStateID { get; set; }
        /// <summary>
        /// 权限点
        /// </summary>
        [NoMapper]
        public T_RLS_Permission Permission { get; set; }
        /// <summary>
        /// 权限Code
        /// </summary>
        [NoMapper]
        public string PermissionsAttributes
        {
            get
            {
                return Permission.PermissionsAttributes;
            }
        }
    }
}

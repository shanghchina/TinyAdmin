using Tiny.Common.Dapper.Enumeration;
using Tiny.Common.Dapper.Persistence.Data;
using Tiny.Common.Types;
using System;

namespace Tiny.OPS.Domain
{
    [Table("T_RLS_UserRole",DBName = EumDBName.POC)]
    public class T_RLS_UserRole : EntityBase
    {
        /// <summary>
        /// 权限系统用户ID
        /// </summary>
        public int UserID { get; set; }
        /// <summary>
        /// 人事系统用户ID
        /// </summary>
        public Guid UserGuid { get; set; }
        /// <summary>
        /// 角色来源ID
        /// </summary>
        public int RoleSourceID { get; set; }
        /// <summary>
        /// 权限授予状态：1000 授予, 2000拒绝，3000 移除，取值为1000的数据
        /// </summary>
        public AuthorizationStateType AuthorizationStateID { get; set; }
        /// <summary>
        /// 职位ID
        /// </summary>
        public Guid PositionTemplateID { get; set; }
        /// <summary>
        /// 岗位ID
        /// </summary>
        public Guid UserPositionID { get; set; }
        /// <summary>
        /// 角色ID
        /// </summary>
        public long RoleID { get; set; }
        /// <summary>
        /// 所属系统ID
        /// </summary>
        public DomainType DomainTypeID { get; set; }
    }
}

using Tiny.Common.Dapper.Enumeration;
using Tiny.Common.Dapper.Persistence.Data;
using Tiny.Common.Types;
using System;

namespace Tiny.OPS.Domain
{
    [Table("T_RLS_UserRoleRange",DBName = EumDBName.POC)]
    public class T_RLS_UserRoleRange : EntityBase
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserID { get; set; }
        /// <summary>
        /// 角色来源ID
        /// </summary>
        public RoleSourceType RoleSourceID { get; set; }
        /// <summary>
        /// 角色ID
        /// </summary>
        public long RoleID { get; set; }
        /// <summary>
        /// 所属系统ID
        /// </summary>
        public DomainType DomainTypeID { get; set; }
        /// <summary>
        /// 权限授予状态：1000 授予, 2000拒绝，3000 移除，取值为1000的数据
        /// </summary>
        public AuthorizationStateType AuthorizationStateID { get; set; }
        /// <summary>
        /// 角色范围ID
        /// </summary>
        public long RoleRangeID { get; set; }
        /// <summary>
        /// 角色范围类型ID
        /// Org = 1001,
        ///ORG_EOS = 1011,
        ///Finance = 2001,
        ///DCS_FileCategory = 3001
        /// </summary>
        public RoleRangeType RoleRangeTypeID { get; set; }
        /// <summary>
        /// 角色范围值，实际是OrgID
        /// </summary>
        public string RoleRangeValue { get; set; }
        /// <summary>
        /// 角色范围父类值
        /// </summary>
        public string ParentRoleRangeValue { get; set; }
        /// <summary>
        /// 角色范围父类ID
        /// </summary>
        public long ParentRoleRangeID { get; set; }
        /// <summary>
        /// 状态：1，有效，2无效
        /// </summary>
        public RowStateType RowStateID { get; set; }
        /// <summary>
        /// 事业部ID
        /// </summary>
        public Guid OneOrgId { get; set; }
        /// <summary>
        /// 事业部Code
        /// </summary>
        public string OneOrgCode { get; set; }
        /// <summary>
        /// 事业部名称
        /// </summary>
        public string OneOrgName { get; set; }
    }
}

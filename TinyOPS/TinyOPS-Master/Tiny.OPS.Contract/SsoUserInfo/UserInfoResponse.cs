using Tiny.OPS.Domain;
using System;
using System.Collections.Generic;

namespace Tiny.OPS.Contract
{
    /// <summary>
    /// 响应用户信息
    /// </summary>
    public class UserInfoResponse
    {

        /// <summary>
        /// 用户guid
        /// </summary>
        public Guid UserGuid { get; set; }

        /// <summary>
        /// 用户姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 用户性别
        /// </summary>
        public string Sex { get; set; }

        /// <summary>
        /// 用户所属一级组织Guid
        /// </summary>
        public Guid LevelOneOrgId { get; set; }

        /// <summary>
        /// 用户所属一级组织名称
        /// </summary>
        public string LevelOneOrgName { get; set; }

        /// <summary>
        /// 用户所属组织机构Guid
        /// </summary>
        public Guid OrganizationId { get; set; }

        /// <summary>
        /// 用户所属组织机构
        /// </summary>
        public string Organization { get; set; }

        /// <summary>
        /// 用户手机号码
        /// </summary>
        public string phone { get; set; }

        /// <summary>
        /// 用户邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 令牌
        /// </summary>
        public string AccessToken { get; set; }
        /// <summary>
        /// 动态密钥
        /// </summary>
        public string Secrectkey { get; set; }
        
        /// <summary>
        /// 用户权限点集合
        /// </summary>
        public List<string> PermissionPoints { get; set; }


    }
}

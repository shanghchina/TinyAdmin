using System;

namespace Tiny.OPS.Contract
{
    /// <summary>
    /// Sso用户信息
    /// </summary>
    public class SsoUserInfo
    {
        /// <summary>
        /// LoginId
        /// </summary>
        public Guid LoginId { get; set; }

        /// <summary>
        /// Sso用户详情
        /// </summary>
        public UserDictionary UserDictionary { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Ttl { get; set; }
        
        /// <summary>
        /// 到期时间
        /// </summary>
        public DateTime Expires { get; set; }
    }

    /// <summary>
    /// Sso用户详情
    /// </summary>
    public class UserDictionary
    {
        /// <summary>
        /// 集团用户姓名
        /// </summary>
        public string OnlyDisplayName { get; set; }
        /// <summary>
        /// 集团员工编号
        /// </summary>
        public string OnlyEmployeeNumber { get; set; }
        /// <summary>
        /// 集团最后修改时间
        /// </summary>
        public string OnlyLastModifyDate { get; set; }
        /// <summary>
        /// 集团用户手机号码
        /// </summary>
        public string OnlyMobile { get; set; }
        /// <summary>
        /// 集团一级组织事业部名称
        /// </summary>
        public string OnlyLevelOneUserOrgName { get; set; }
        /// <summary>
        /// 集团用户所属全级组织名称
        /// </summary>
        public string OnlyUserOrgFullName { get; set; }
        /// <summary>
        /// 集团用户所属组织机构
        /// </summary>
        public string OnlyOrganization { get; set; }
        /// <summary>
        /// 集团用户手机号码
        /// </summary>
        public string OnlyTelephone { get; set; }
        /// <summary>
        /// 集团用户邮箱
        /// </summary>
        public string OnlyPersonalEmail { get; set; }
        /// <summary>
        /// 集团用户所属组织机构Guid
        /// </summary>
        public Guid OnlyOrganizationId { get; set; }
        /// <summary>
        /// 集团用户职位guid
        /// </summary>
        public Guid OnlyPositionId { get; set; }
        /// <summary>
        /// 集团用户职位名称
        /// </summary>
        public string OnlyPositionName { get; set; }
        /// <summary>
        /// 集团用户职位模板guid
        /// </summary>
        public Guid OnlyPositionTemplateId { get; set; }
        /// <summary>
        /// 集团用户职位模板名称
        /// </summary>
        public string OnlyPositionTemplateName { get; set; }
        /// <summary>
        /// 集团用户Id
        /// </summary>
        public string OnlyUserID { get; set; }
        /// <summary>
        /// 集团用户guid
        /// </summary>
        public Guid OnlyUserGuid { get; set; }
        /// <summary>
        /// 集团用户性别
        /// </summary>
        public string OnlySex { get; set; }
        /// <summary>
        /// 集团用户更新时间
        /// </summary>
        public DateTime OnlyTimeUpdate { get; set; }
        /// <summary>
        /// 集团用户街道地址
        /// </summary>
        public string StreetAddress { get; set; }
        /// <summary>
        /// 版本
        /// </summary>
        public string Version { get; set; }
        /// <summary>
        /// 系统
        /// </summary>
        public string System { get; set; }
    }


    /// <summary>
    /// 网关
    /// </summary>
    public class GatewayResponse
    {
        public GwResultinfo resultinfo { get; set; }
        public string code { get; set; }
        public string message { get; set; }


    }

    public class GwResultinfo
    {
        public string access_token { get; set; }
        public string secrect_key { get; set; }
        public string token_type { get; set; }
        public string timestamp { get; set; }
        public string expires_in { get; set; }
        public string refresh_token { get; set; }
    }

}

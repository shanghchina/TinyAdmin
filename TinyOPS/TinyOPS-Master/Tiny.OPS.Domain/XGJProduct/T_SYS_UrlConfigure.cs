using Tiny.Common.Dapper.Enumeration;
using Tiny.Common.Dapper.Persistence.Data;
using Tiny.OPS.Domain.Enum;
using System.Collections.Generic;

namespace Tiny.OPS.Domain.XGJProduct
{
    /// <summary>
    /// XGJInterfaceConfigure校管家接口配置 
    /// </summary>
    [Table("T_SYS_UrlConfigure", DBName = EumDBName.POC)]
    public class T_SYS_UrlConfigure : EntityBase
    {
        /// <summary>
        /// 类别(XGJ-内部接口 XGJSec-对外接口)
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// 来源系统
        /// </summary>
        public int FromSystem { get; set; }

        /// <summary>
        /// 事业部Id
        /// </summary>
        public string ChargeLevelOneOrgId { get; set; }

        /// <summary>
        /// 事业部
        /// </summary>
        public string ChargeLevelOneOrgName { get; set; }

        /// <summary>
        /// TokenUrl
        /// </summary>
        public string TokenUrl { get; set; }

        /// <summary>
        /// ErrorUrl
        /// </summary>
        public string ErrorUrl { get; set; }

        /// <summary>
        /// AppId
        /// </summary>
        public string AppId { get; set; }

        /// <summary>
        /// AppSecret
        /// </summary>
        public string AppSecret { get; set; }

        /// <summary>
        /// EnableTokenCache
        /// </summary>
        public bool EnableTokenCache { get; set; }

        /// <summary>
        /// 配置功能
        /// </summary>
        public string ConfigureFunction { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string CreatedUserId { get; set; }

        public long VersionNum { get; set; } = 0;
        /// <summary>
        /// 产品中心来源系统
        /// </summary>
        public int POCSource { get; set; }

        /// <summary>
        /// 状态(7000-禁用  1000-启用)
        /// </summary>
        public XGJInterfaceConfigureStatus UrlConfigureStatus { get; set; }
    }

    /// <summary>
    /// 校管家接口类型
    /// </summary>
    public static class XGJInterfaceConfigureCategory
    {
        public static Dictionary<string, string> GetCategory()
        {
            return new Dictionary<string, string>
            {
                { XGJInSideInterface, "对内接口" },
                { XGJOutSideInterface, "对外接口" }
            };
        }

        /// <summary>
        /// 校管家内部接口
        /// </summary>
        public const string XGJInSideInterface = "XGJInSideInterface";
        /// <summary>
        /// 校管家外部接口
        /// </summary>
        public const string XGJOutSideInterface = "XGJOutSideInterface";
    }
}

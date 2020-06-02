using Tiny.Common.Dapper.Enumeration;
using Tiny.Common.Dapper.Persistence.Data;

namespace Tiny.OPS.Domain
{
    /// <summary>
    /// 来源系统appid表
    /// </summary>
    [Table("T_APP_Appid", DBName = EumDBName.POC)]
    public class T_APP_Appid : EntityBase
    {
        /// <summary>
        /// ip白名单表APP_WhiteList的id
        /// </summary>
        public int AppWhiteListId { get; set; }

        /// <summary>
        /// appid
        /// </summary>
        public string Appid { get; set; }

        /// <summary>
        /// 密钥
        /// </summary>
        public string SecretKey { get; set; }

        /// <summary>
        /// 来源系统code
        /// </summary>
        public FromSystem FromSystem { get; set; }

        /// <summary>
        /// 来源系统
        /// </summary>
        public string FromSystemName { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnabled { get; set; }
    }
}

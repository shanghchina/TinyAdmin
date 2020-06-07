using System;
using Tiny.Common.Dapper.Enumeration;
using Tiny.Common.Dapper.Persistence.Data;

namespace Tiny.OPS.Domain
{
    [Table("t_sys_token", DBName = EumDBName.POC)]
    public class T_Sys_Token : EntityBase
    {
        /// <summary>
        /// guid
        /// </summary>
        public Guid tb_guid { get; set; }


        /// <summary>
        ///app_name
        /// </summary>
        public string app_name { get; set; }

        /// <summary>
        ///app_id
        /// </summary>
        public string app_id { get; set; }

        /// <summary>
        ///app_secret
        /// </summary>
        public string app_secret        { get; set; }

        /// <summary>
        ///access_token
        /// </summary>
        public string access_token { get; set; }

        /// <summary>
        ///begin_datetime
        /// </summary>
        public DateTime? begin_datetime { get; set; }

        /// <summary>
        ///expried_datetime
        /// </summary>
        public DateTime? expried_datetime { get; set; }

        /// <summary>
        /// 时间戳秒
        /// </summary>
        public int time_stamp { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Text;
using Tiny.Common.Dapper.Enumeration;
using Tiny.Common.Dapper.Persistence.Data;

namespace Tiny.OPS.Domain
{
    [Table("t_sys_user", DBName = EumDBName.POC)]
    public class T_Sys_User : EntityBase
    {
        /// <summary>
        /// guid
        /// </summary>
        public string tb_guid { get; set; }

        /// <summary>
        ///avatar_id
        /// </summary>
        public long? avatar_id { get; set; }

        /// <summary>
        ///email
        /// </summary>
        public string email { get; set; }

        /// <summary>
        ///is_admin
        /// </summary>
        public bool is_admin { get; set; }

        /// <summary>
        ///is_enabled
        /// </summary>
        public int is_enabled { get; set; }
        
        /// <summary>
        ///password
        /// </summary>
        public string password { get; set; }

        /// <summary>
        ///username
        /// </summary>
        public string username { get; set; }

        /// <summary>
        /// dept_id
        /// </summary>
        public int? dept_id { get; set; }

        /// <summary>
        ///phone
        /// </summary>
        public string phone { get; set; }

        /// <summary>
        /// post_id
        /// </summary>
        public int? post_id { get; set; }

        /// <summary>
        /// last_password_reset_time
        /// </summary>
        public DateTime? last_password_reset_time { get; set; }

        /// <summary>
        ///nick_name
        /// </summary>
        public string nick_name { get; set; }

        /// <summary>
        ///sex
        /// </summary>
        public string sex { get; set; }


    }
}

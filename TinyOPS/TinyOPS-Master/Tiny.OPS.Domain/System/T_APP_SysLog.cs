using Tiny.Common.Dapper.Enumeration;
using Tiny.Common.Dapper.Persistence.Data;
using System.ComponentModel.DataAnnotations;

namespace Tiny.OPS.Domain
{
    /// <summary>
    /// 系统日志记录表
    /// 系统日志记录表，记录异常，正常日志信息，编译数据库中查看日志
    /// </summary>
    [Table("T_APP_SysLog", DBName = EumDBName.POC)]
    public class T_APP_SysLog : EntityBase
    {
        /// <summary>
        /// 日志类型error,info,Warning
        /// </summany>
        [MaxLength(50)]
        public string LogType { get; set; }

        /// <summary>
        /// 日志标题说明
        /// </summany>
        [MaxLength(500)]
        public string LogDesc { get; set; }

        /// <summary>
        /// 日志参数
        /// </summany>
        public string LogParam { get; set; }

        /// <summary>
        /// 日志内容
        /// </summany>
        public string LogInfo { get; set; }

        /// <summary>
        /// 修改人
        /// </summany>
        [MaxLength(50)]
        public string UpdaterUserId { get; set; }

        /// <summary>
        /// 修改人
        /// </summany>
        [MaxLength(50)]
        public string UpdaterUserName { get; set; }

    }
}

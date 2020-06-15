using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TinyEdu.Admin.WebApi
{
    /// <summary>
    /// appsetting对应配置
    /// </summary>
    public class ApiAuthorize
    {
        /// <summary>
        /// appid
        /// </summary>
        public string Appid { get; set; }

        /// <summary>
        ///AppSecret
        /// </summary>
        public string AppSecret { get; set; }
    }
}

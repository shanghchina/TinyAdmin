using System;
using System.Collections.Generic;
using System.Text;

namespace TinyEdu.Common.Dapper.Middleware
{
    public class OnlyDapperOptions
    {
        /// <summary>
        /// Ioc注入的xml文件地址
        /// </summary>
        public string IoCXmlPath { get; set; }

        /// <summary>
        /// 模型校验的xml文件地址
        /// </summary>
        public string ValidateXmlPath { get; set; }
        /// <summary>
        /// 转描述的xml文件地址
        /// </summary>
        public string LanguageXmlPath { get; set; }
        /// <summary>
        /// 数据库读地址
        /// </summary>
        public string ReadDbConnection { get; set; }
        /// <summary>
        /// 数据库写地址
        /// </summary>
        public string WriteDbConnection { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Tiny.Common.Dapper.DI
{
    public class XmlIoCInfo
    {
        public string Type { get; set; }

        public string MapTo { get; set; }

        /// <summary>
        /// transient(默认)、scoped、singleton
        /// </summary>
        public string Lifetime { get; set; }
    }
}

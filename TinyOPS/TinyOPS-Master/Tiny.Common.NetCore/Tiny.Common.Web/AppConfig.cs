using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace Tiny.Common.Web
{
    public class AppConfig
    {
        public static IConfigurationRoot Configuration { get; set; }

        public static IConfigurationSection GetSection(string name)
        {
            return Configuration?.GetSection(name);
        }

        public static T GetSection<T>(string name)
        {
            var section=Configuration.GetSection(name);
            if (section != null)
                return section.Get<T>();
            return default(T);
        }
    }
}

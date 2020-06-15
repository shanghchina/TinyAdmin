using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TinyEdu.Admin.WebApi
{
    /// <summary>
    /// 读取配置文件
    /// </summary>
    public class ConfigurationHelper
    {
        /// <summary>
        /// 
        /// </summary>
        public IConfiguration config { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ConfigurationHelper()
        {
            IHostingEnvironment env = MyServiceProvider.ServiceProvider.GetRequiredService<IHostingEnvironment>();
            config = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables()
                .Build();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T GetAppSettings<T>(string key) where T : class, new()
        {
            var appconfig = new ServiceCollection()
                .AddOptions()
                .Configure<T>(config.GetSection(key))
                .BuildServiceProvider()
                .GetService<IOptions<T>>()
                .Value;
            return appconfig;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class MyServiceProvider
    {
        /// <summary>
        /// 
        /// </summary>
        public static IServiceProvider ServiceProvider { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Tiny.Common.Dapper.Language;
using Tiny.Common.Dapper.Persistence.Context;
using Tiny.Common.Dapper.Persistence.Mapper;
using Tiny.Common.Dapper.Repository;
using Tiny.Common.Dapper.Validate;

namespace Tiny.Common.Dapper.DI
{
    /// <summary>
    /// 提供依赖注入的功能
    /// </summary>
    public class DapperDIProvider
    {
        /// <summary>
        /// 依赖注入
        /// </summary>
        /// <param name="serviceDescriptors">IServiceCollection</param>
        /// <param name="IocPath">配置依赖注入的路劲(.xml)</param>
        /// <param name="validatePath">模型校验文件的路径(.xml)</param>
        /// <param name="languagePath">配置名称的路径(.xml)</param>
        public static void Register(IServiceCollection serviceDescriptors, string IocPath = null, string validatePath = null, string languagePath = null)
        {
            serviceDescriptors.AddSingleton<IValidation, XmlValidation>();
            serviceDescriptors.AddSingleton<ILanguage, XmlLanguage>();
            serviceDescriptors.AddSingleton<IXmlIoC, XmlIoC>();
            serviceDescriptors.AddScoped<IContext, Context>();
            serviceDescriptors.AddSingleton<IMapper, SQLMapper>();
            serviceDescriptors.AddScoped<IContextStorage, ThreadContextStorage>();
            serviceDescriptors.AddScoped<IRepository, RepositoryBase>();

            RegisterTypes(serviceDescriptors, IocPath);

            IoC.InitializeWith(serviceDescriptors);

            if (!String.IsNullOrEmpty(validatePath))
                IoC.Resolve<IValidation>().ConfigFile = validatePath;
            if (!String.IsNullOrEmpty(languagePath))
                IoC.Resolve<ILanguage>().ConfigFile = languagePath;
        }

        private static void RegisterTypes(IServiceCollection serviceDescriptors, string configFile)
        {
            if (String.IsNullOrEmpty(configFile))
                return;
            var ioc = new XmlIoC();
            ioc.ConfigFile = configFile;

            foreach (KeyValuePair<string, IList<XmlIoCInfo>> temp in ioc.GetAll())
            {
                foreach (var item in temp.Value)
                {
                    if (String.IsNullOrEmpty(item.Type))
                        throw new InvalidOperationException($"类型不能为空{item.Type}");
                    if (String.IsNullOrEmpty(item.MapTo))
                        throw new InvalidOperationException($"映射类型不能为空{item.MapTo}");
                    Type type = Type.GetType(item.Type);
                    Type mapTo = Type.GetType(item.MapTo);
                    if (type == null || mapTo == null)
                        throw new InvalidOperationException($"未找到所对应的命名空间:{item.Type}-{item.MapTo}");
                    switch (item.Lifetime.ToLower())
                    {
                        case "transient":
                            serviceDescriptors.AddTransient(type, mapTo);
                            break;
                        case "scoped":
                            serviceDescriptors.AddScoped(type, mapTo);
                            break;
                        case "singleton":
                            serviceDescriptors.AddSingleton(type, mapTo);
                            break;
                        default:
                            serviceDescriptors.AddTransient(type, mapTo);
                            break;
                    }
                }
            }
        }
    }
}

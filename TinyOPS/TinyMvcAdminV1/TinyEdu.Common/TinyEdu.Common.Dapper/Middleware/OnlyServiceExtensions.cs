using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using TinyEdu.Common.Dapper.DI;
using TinyEdu.Common.Dapper.Persistence.Data;

namespace TinyEdu.Common.Dapper.Middleware
{
    public static class OnlyServiceExtensions
    {
        public static IServiceCollection AddOnlyDapper(this IServiceCollection services, OnlyDapperOptions options)
        {
            DapperDIProvider.Register(services,
                options.IoCXmlPath, options.ValidateXmlPath, options.LanguageXmlPath);
            DataBaseHelper.ReaderConnectString = options.ReadDbConnection;
            DataBaseHelper.WriterConnectString = options.WriteDbConnection;
            return services;
        }
    }
}

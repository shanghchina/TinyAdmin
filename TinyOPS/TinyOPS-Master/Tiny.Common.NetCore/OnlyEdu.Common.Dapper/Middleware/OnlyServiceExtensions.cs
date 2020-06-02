using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using OnlyEdu.Common.Dapper.DI;
using OnlyEdu.Common.Dapper.Persistence.Data;

namespace OnlyEdu.Common.Dapper.Middleware
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

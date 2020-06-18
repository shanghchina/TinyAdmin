using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace TinyEdu.Common.Dapper.DI
{
    public static class IoC
    {
        public static IServiceCollection ServiceCollection { get; set; }
        private static ServiceProvider ServiceProvider { get; set; }

        public static void InitializeWith(IServiceCollection serviceDescriptors)
        {
            ServiceCollection = serviceDescriptors;
            ServiceProvider = ServiceCollection.BuildServiceProvider();
        }

        public static void InitializeWith(IServiceCollection serviceDescriptors, IServiceProvider serviceProvider)
        {
            ServiceCollection = serviceDescriptors;
            ServiceProvider = (ServiceProvider)serviceProvider;
        }

        public static bool IsExist()
        {
            return ServiceCollection != null;
        }

        public static T Resolve<T>()
        {
            return ServiceProvider.GetService<T>();
        }

        public static IEnumerable<T> ResolveAll<T>()
        {
            return ServiceProvider.GetServices<T>();
        }

        public static void Reset()
        {
            if (ServiceCollection != null)
            {
                ServiceCollection.Clear();
                ServiceProvider.Dispose();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace Tiny.Common.Web.Authentication.Web
{
    public static class OnlyAuthenticationServicesExtensions
    {
        public static IServiceCollection AddOnlyAuthentication(this IServiceCollection services)
        {
            if (services==null)
            {
                throw new ArgumentNullException(nameof(services));
            }
            return services;
        }
        public static IServiceCollection AddOnlyAuthentication(this IServiceCollection services,Action<OnlyAuthenticationOptions> OnlyAuthenticationOptions)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }
            if(OnlyAuthenticationOptions == null)
            {
                throw new ArgumentNullException(nameof(OnlyAuthenticationOptions));
            }
            services.Configure(OnlyAuthenticationOptions);
            return services;
        }
    }
}

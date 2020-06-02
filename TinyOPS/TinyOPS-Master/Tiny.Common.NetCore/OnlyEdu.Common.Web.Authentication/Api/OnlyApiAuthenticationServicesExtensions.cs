using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace OnlyEdu.Common.Web.Authentication.Api
{
    public static class OnlyApiAuthenticationServicesExtensions
    {
        public static IServiceCollection AddOnlyApiAuthentication(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }
            return services;
        }
        public static IServiceCollection AddOnlyApiAuthentication(this IServiceCollection services, Action<OnlyApiAuthenticationOptions> OnlyAuthenticationOptions)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }
            if (OnlyAuthenticationOptions == null)
            {
                throw new ArgumentNullException(nameof(OnlyAuthenticationOptions));
            }
            services.Configure(OnlyAuthenticationOptions);
            return services;
        }
    }
}

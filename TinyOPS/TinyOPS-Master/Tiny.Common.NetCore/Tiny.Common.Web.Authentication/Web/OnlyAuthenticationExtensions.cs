
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;
using System;

namespace Tiny.Common.Web.Authentication.Web
{
    public static class OnlyAuthenticationExtensions
    {
        public static IApplicationBuilder UseOnlyAuthentication(this IApplicationBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return builder.UseMiddleware<OnlyAuthenticationMiddleware>();
        }

        public static IApplicationBuilder UseOnlyAuthentication(this IApplicationBuilder builder, OnlyAuthenticationOptions options)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            return builder.UseMiddleware<OnlyAuthenticationOptions>(Options.Create(options));
        }
    }
}


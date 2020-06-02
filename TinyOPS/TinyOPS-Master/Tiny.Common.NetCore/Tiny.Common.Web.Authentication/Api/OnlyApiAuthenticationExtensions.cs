using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;

namespace Tiny.Common.Web.Authentication.Api
{
    public static class OnlyApiAuthenticationExtensions
    {
        public static IApplicationBuilder UseOnlyApiAuthentication(this IApplicationBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return builder.UseMiddleware<OnlyApiAuthenticationMiddleware>();
        }

        public static IApplicationBuilder UseOnlyApiAuthentication(this IApplicationBuilder builder, OnlyApiAuthenticationOptions options)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            return builder.UseMiddleware<OnlyApiAuthenticationOptions>(Options.Create(options));
        }
    }
}

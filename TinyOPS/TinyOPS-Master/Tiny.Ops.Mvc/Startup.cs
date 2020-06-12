using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;
using System.IO;
using Tiny.Common.Dapper.DI;
using Tiny.Common.Dapper.Middleware;
using Tiny.Common.Dapper.Persistence.Data;
using Tiny.Ops.Mvc.Filter;
using Tiny.OPS.Common;

namespace Tiny.Ops.Mvc
{
    public class Startup
    {
        public const string CookieScheme = "Cookies";


        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            // 配置日志log4net
            services.AddLogging(logConfig =>
            {
                _Log4Net.LoadLogger();
            });

            services.AddAuthentication(CookieScheme)
                    .AddCookie(CookieScheme, option =>
                    {
                        // 登录路径：这是当用户试图访问资源但未经过身份验证时，程序将会将请求重定向到这个相对路径。
                        option.LoginPath = new PathString("/Account/Login");
                        // 禁止访问路径：当用户试图访问资源时，但未通过该资源的任何授权策略，请求将被重定向到这个相对路径
                        option.AccessDeniedPath = new PathString("/Error/Error404");
                    });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            #region 添加DDD dapper支持
            //注入数据库
            string basePath = Path.GetDirectoryName(typeof(Program).Assembly.Location);
            string iocConfigFileName = $@"{basePath}/Config/DI/IoC.xml";


            services.AddOnlyDapper(new OnlyDapperOptions()
            {
                IoCXmlPath = iocConfigFileName,
            });
            IoC.InitializeWith(services);
            var builder = new ConfigurationBuilder()
                     .SetBasePath(Directory.GetCurrentDirectory())
                     .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            var configuration = builder.Build();
            services.AddSingleton(new DataBaseHelper(configuration));
            #endregion

            // 添加全局异常捕获
            services.AddMvc(option =>
            {
                //option.Filters.Add<InvokeLogAttribute>(); //记录日志
                //option.Filters.Add<ApiAuthorizeAttribute>(); //api验证
                //option.Filters.Add<ModelValidationFilterAttribute>();//模型验证
                option.Filters.Add<HttpGlobalExceptionFilter>();
            }).AddJsonOptions(options => { options.SerializerSettings.ContractResolver = new DefaultContractResolver(); });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();//增加登录授权

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}

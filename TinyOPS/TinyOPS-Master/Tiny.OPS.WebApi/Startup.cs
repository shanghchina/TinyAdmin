using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Tiny.Common.Dapper.Middleware;
using Tiny.Common.Dapper.Persistence.Data;
using Tiny.OPS.Common;
using Microsoft.Extensions.PlatformAbstractions;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.AspNetCore.Http;
using Tiny.Common.Dapper.DI;
using Newtonsoft.Json.Serialization;
using Tiny.OPS.WebApi.Filter;
using AutoMapper;
using Tiny.OPS.Contract;

namespace Tiny.OPS.WebApi
{
    /// <summary>
    /// 
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// 
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
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

            //配置跨域处理，允许所有来源：
            services.AddCors(options =>
            {
                // Policy 名稱 CorsPolicy 是自訂的，可以自己改
                options.AddPolicy("core", policy =>
                {
                    // 設定允許跨域的來源，有多個的話可以用 `,` 隔開
                    policy.WithOrigins(ConfigVal.CrossDomainUrl)
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                            .AllowCredentials();
                });
            });

            ////.net core webApi 默认开启模型验证,当 SuppressModelStateInvalidFilter 属性设置为 true 时，会禁用默认行为。我们用自定义模型验证返回信息
            //services.Configure<ApiBehaviorOptions>(options =>
            //{
            //    options.SuppressModelStateInvalidFilter = true;
            //});
            services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new Info { Title = "POC API", Version = "v1" });
                    var path = PlatformServices.Default.Application.ApplicationBasePath;
                    var xmlPath = Path.Combine(path, "Tiny.OPS.WebApi.xml");
                    var xmlModelPath = Path.Combine(path, "Tiny.OPS.Contract.xml");
                    c.IncludeXmlComments(xmlModelPath);
                    c.IncludeXmlComments(xmlPath);
                    c.DocumentFilter<AuthApplyTagDescriptions>();
                });
            services.AddHttpContextAccessor();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // 配置日志log4net
            services.AddLogging(logConfig =>
            {
                //log4net.Config.XmlConfigurator.Configure(Common.Logger.LoggerRepository);
                _Log4Net.LoadLogger();
            });


            services.AddAutoMapper();
            Mapper.Initialize(cfg => cfg.AddProfiles(typeof(ServiceProfiles)));

            // 添加全局异常捕获
            services.AddMvc(option =>
            {
                option.Filters.Add<InvokeLogAttribute>(); //记录日志
                option.Filters.Add<ApiAuthorizeAttribute>(); //api验证
                option.Filters.Add<ModelValidationFilterAttribute>();//模型验证
                option.Filters.Add<HttpGlobalExceptionFilter>();
                // option.Filters.Add(typeof(PermissionFilterAttribute));
            }).AddJsonOptions(options => { options.SerializerSettings.ContractResolver = new DefaultContractResolver(); });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            _Log4Net.Info("Configure begin");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "POC API V1");
            });
            app.UseHttpsRedirection();
            app.UseCors("core");
            app.UseMvc();
            _Log4Net.Info("Configure end");
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extras.DynamicProxy;
using CSRedis;
using log4net;
using log4net.Config;
using log4net.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Redis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using YouGe.Core.Common.Helper;
using YouGe.Core.Commons;
using YouGe.Core.Models.System;

namespace YouGe.Core.ManagerApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            repository = LogManager.CreateRepository("CoreLogRepository");
            XmlConfigurator.Configure(repository, new FileInfo("log4net.config"));
            Log4NetRepository.loggerRepository = repository;
        }
        public static ILoggerRepository repository { get; set; }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();

            #region swagger ui
            //使用自身的
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "jonny",
                    ValidAudience = "jonny",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("secretsecretsecret"))
                };
            });

            //使用identityserver 

            //services.AddAuthentication("Bearer")
            //   .AddJwtBearer("Bearer", options =>
            //   {
            //       options.Authority = "http://localhost:5000";
            //       options.RequireHttpsMetadata = false;
            //       options.Audience = "user_api";
            //   });


            services.AddSwaggerGen(options =>
            {
                var scheme = new OpenApiSecurityScheme()
                {
                    Scheme = JwtBearerDefaults.AuthenticationScheme,
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    //头名称
                    Name = "Authorization", //这个不能动，一定要一幕一样
                    Type = SecuritySchemeType.ApiKey,
                   
                    Description = "Bearer （Token） Bearer {token}注意有空格 "
                };
                options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, scheme);
                options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                 {
                     {
                         new OpenApiSecurityScheme
                         {
                             Reference = new OpenApiReference
                             {
                                 Type = ReferenceType.SecurityScheme,
                                 Id = "Bearer"
                               
                             }
                            
                         },
                         new string[] {}
                     }
                 }); 
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "有个 Core", Version = "v1" });
                //xml 文件
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                // xml
                var xmlPath = System.IO.Path.Combine(AppContext.BaseDirectory, xmlFile);                 
                options.IncludeXmlComments(xmlPath, true);

                
            });
            #endregion

            // eg 1.单个redis实现 普通模式
            //CSRedisClient csredis = new CSRedisClient("127.0.0.1:6379,password=,defaultDatabase=csredis,prefix=csredis-example");
            //eg 2.单个redis，使用appsettings.json中的配置项
            IConfigurationSection configurationSection = Configuration.GetSection("CsRedisConfig:DefaultConnectString");
            CSRedisClient csredis = new CSRedisClient(configurationSection.Value);
            //初始化 RedisHelper
            YouGeRedisHelper.Initialization(csredis);
            //注册mvc分布式缓存
            services.AddSingleton<IDistributedCache>(new CSRedisCache(RedisHelper.Instance));
            services.Configure<GateWayDbContextOption>(options =>
            {
                options.TagName = "db2";
                options.ConnectionString = Configuration.GetConnectionString("PayGatewayDB");
                options.ModelAssemblyName = "Ehome.GateWay.Pay.DBEntitys";//这里必须是数据库实体类所在的项目
                options.IsOutputSql = false;
            }
          );


        }
        /// <summary>
        /// called by autofac 
        /// </summary>
        /// <param name="builder"></param>
        public void ConfigureContainer(ContainerBuilder builder)
        {
            var basePath = AppContext.BaseDirectory;

            #region 带有接口层的服务注入
            var servicesDllFile = Path.Combine(basePath, "YouGe.Core.Services.dll");
            var repositoryDllFile = Path.Combine(basePath, "YouGe.Core.Repositorys.dll");

            if (!(File.Exists(servicesDllFile) && File.Exists(repositoryDllFile)))
            {
                throw new Exception("Repositorys.dll和services.dll 丢失，因为项目解耦了，所以需要先F6编译，再F5运行，请检查 bin 文件夹，并拷贝。");
            }
            // AOP 开关，如果想要打开指定的功能，只需要在 appsettigns.json 对应对应 true 就行。
            var cacheType = new List<Type>();

            // 获取 Service.dll 程序集服务，并注册
            var assemblysServices = Assembly.LoadFrom(servicesDllFile);
            builder.RegisterAssemblyTypes(assemblysServices)
                      .AsImplementedInterfaces()
                      .InstancePerDependency()
                      .EnableInterfaceInterceptors()//引用Autofac.Extras.DynamicProxy;
                      .InterceptedBy(cacheType.ToArray());//允许将拦截器服务的列表分配给注册。

            // 获取 Repository.dll 程序集服务，并注册
            var assemblysRepository = Assembly.LoadFrom(repositoryDllFile);
            builder.RegisterAssemblyTypes(assemblysRepository)
                   .AsImplementedInterfaces()
                   .InstancePerDependency();

            #endregion

        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            // 开启swagger ui

            app.UseRouting();
            //身份认证中间件(踩坑：授权中间件必须在认证中间件之前)
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "有个 Core后台API");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

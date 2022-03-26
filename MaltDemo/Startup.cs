using Malt.Core;
using MaltDemo.App_Code.AbstractClass;
using MaltDemoModelCommon;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaltDemo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// 默认日志记录器
        /// </summary>
        public static ILogger DefaultLogger { get; private set; }

        /// <summary>
        /// 是否为开发模式
        /// </summary>
        public static bool IsDevelopment { get; private set; }

        /// <summary>
        /// 配置
        /// </summary>
        public static IConfiguration Configuration { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            #region 跨域访问

            var urls = Configuration["Cors:Urls"].Split(',');
            services.AddCors(options =>
            {
                options.AddPolicy("CORS", builder =>
                {
                    builder.WithOrigins(urls) // 允许部分站点跨域请求
                    .AllowAnyMethod() // 允许所有请求方法
                    .AllowAnyHeader() // 允许所有请求头 
                    .AllowCredentials(); // 允许Cookie信息
                });
            });

            #endregion 跨域访问

            //设置Json
            services.AddControllers().AddNewtonsoftJson(options =>
            {
                //忽略循环引用
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                //不使用驼峰样式的key
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                //设置时间格式
                options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MaltDemo", Version = "v1" });
            });
            //票据管理器
            services.AddSingleton<ITokenManager>(serviceProvider => new TokenManager<UserToken>() { DefaultCookieName = "cookieName", DefaultDomain = ".aimiyun.com" });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerProvider loggerProvider)
        {
            //默认日志记录器
            DefaultLogger = loggerProvider.CreateLogger("DefaultLogger");
            IsDevelopment = env.IsDevelopment();
            if (IsDevelopment)
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MaltDemo v1"));
            }

            //处理状态编号
            app.UseStatusCodePages(builder => builder.Run(StatusCodeManager.Instance.StatusCodeHandler));

            //处理异常
            app.UseExceptionHandler(builder => builder.Run(ExceptionManager.Instance.ExceptionHandler));

            //Https设置
            //app.UseHttpsRedirection();

            app.UseRouting();

            //默认文件
            DefaultFilesOptions defaultFilesOptions = new DefaultFilesOptions();
            defaultFilesOptions.DefaultFileNames.Clear();
            defaultFilesOptions.DefaultFileNames.Add("index.html");
            app.UseDefaultFiles(defaultFilesOptions);
            //开启静态文件
            app.UseStaticFiles();

            //跨域
            app.UseCors("CORS");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

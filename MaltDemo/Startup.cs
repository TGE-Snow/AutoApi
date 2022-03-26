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
        /// Ĭ����־��¼��
        /// </summary>
        public static ILogger DefaultLogger { get; private set; }

        /// <summary>
        /// �Ƿ�Ϊ����ģʽ
        /// </summary>
        public static bool IsDevelopment { get; private set; }

        /// <summary>
        /// ����
        /// </summary>
        public static IConfiguration Configuration { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            #region �������

            var urls = Configuration["Cors:Urls"].Split(',');
            services.AddCors(options =>
            {
                options.AddPolicy("CORS", builder =>
                {
                    builder.WithOrigins(urls) // ������վ���������
                    .AllowAnyMethod() // �����������󷽷�
                    .AllowAnyHeader() // ������������ͷ 
                    .AllowCredentials(); // ����Cookie��Ϣ
                });
            });

            #endregion �������

            //����Json
            services.AddControllers().AddNewtonsoftJson(options =>
            {
                //����ѭ������
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                //��ʹ���շ���ʽ��key
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                //����ʱ���ʽ
                options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MaltDemo", Version = "v1" });
            });
            //Ʊ�ݹ�����
            services.AddSingleton<ITokenManager>(serviceProvider => new TokenManager<UserToken>() { DefaultCookieName = "cookieName", DefaultDomain = ".aimiyun.com" });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerProvider loggerProvider)
        {
            //Ĭ����־��¼��
            DefaultLogger = loggerProvider.CreateLogger("DefaultLogger");
            IsDevelopment = env.IsDevelopment();
            if (IsDevelopment)
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MaltDemo v1"));
            }

            //����״̬���
            app.UseStatusCodePages(builder => builder.Run(StatusCodeManager.Instance.StatusCodeHandler));

            //�����쳣
            app.UseExceptionHandler(builder => builder.Run(ExceptionManager.Instance.ExceptionHandler));

            //Https����
            //app.UseHttpsRedirection();

            app.UseRouting();

            //Ĭ���ļ�
            DefaultFilesOptions defaultFilesOptions = new DefaultFilesOptions();
            defaultFilesOptions.DefaultFileNames.Clear();
            defaultFilesOptions.DefaultFileNames.Add("index.html");
            app.UseDefaultFiles(defaultFilesOptions);
            //������̬�ļ�
            app.UseStaticFiles();

            //����
            app.UseCors("CORS");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

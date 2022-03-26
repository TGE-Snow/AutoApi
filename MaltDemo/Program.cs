using Malt.Core;
using Malt.Logger;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using TBKBusiness.OrderLogic;

namespace MaltDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //System.Timers.Timer timer = new System.Timers.Timer();
            //timer.Enabled = true;
            //timer.Interval = 20000; //ִ�м��ʱ��,��λΪ����; ����ʵ�ʼ��Ϊ10����
            //timer.Start();
            //timer.Elapsed += new System.Timers.ElapsedEventHandler(test);

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
           Host.CreateDefaultBuilder(args)
                .ConfigureMaltWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }

    public static class ConfigureHelper
    {
        public static IHostBuilder ConfigureMaltWebHostDefaults(this IHostBuilder @this, Action<IWebHostBuilder> configure)
        {
            //����ʾHosting environment����־��Ϣ
            //@this.UseConsoleLifetime(opts => opts.SuppressStatusMessages = true);
            //�������
            @this.UseDefaultServiceProvider((context, options) =>
            {
                //������֤
                bool flag = context.HostingEnvironment.IsDevelopment();
                options.ValidateScopes = flag;
                options.ValidateOnBuild = flag;
                //��ȡ������url
                string urls = context.Configuration["urls"];
                if (!string.IsNullOrEmpty(urls))
                {
                    string[] urlArray = urls.Split(";", StringSplitOptions.RemoveEmptyEntries);
                    foreach (var item in urlArray)
                    {
                        Console.WriteLine($"Now listening on: {item}");
                    }
                }
            });
            //������־
            @this.ConfigureLogging((hostingContext, loggingBuilder) =>
            {
                loggingBuilder.ClearProviders();
                if (!hostingContext.HostingEnvironment.IsDevelopment())
                {
                    loggingBuilder.AddFilter<LoggerProvider>(level => level >= LogLevel.Information);
                }
                loggingBuilder.AddLogger();
            });
            //����Ĭ����Ϣ
            @this.ConfigureWebHostDefaults(webBuilder =>
            {
                //����ʾrespone�е�server
                //webBuilder.UseKestrel(options => options.AddServerHeader = false);
                configure(webBuilder);
            });
            return @this;
        }
    }
}
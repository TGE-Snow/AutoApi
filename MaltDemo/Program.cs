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
            //timer.Interval = 20000; //执行间隔时间,单位为毫秒; 这里实际间隔为10分钟
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
            //不显示Hosting environment等日志信息
            //@this.UseConsoleLifetime(opts => opts.SuppressStatusMessages = true);
            //输出监听
            @this.UseDefaultServiceProvider((context, options) =>
            {
                //设置验证
                bool flag = context.HostingEnvironment.IsDevelopment();
                options.ValidateScopes = flag;
                options.ValidateOnBuild = flag;
                //获取监听的url
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
            //设置日志
            @this.ConfigureLogging((hostingContext, loggingBuilder) =>
            {
                loggingBuilder.ClearProviders();
                if (!hostingContext.HostingEnvironment.IsDevelopment())
                {
                    loggingBuilder.AddFilter<LoggerProvider>(level => level >= LogLevel.Information);
                }
                loggingBuilder.AddLogger();
            });
            //设置默认信息
            @this.ConfigureWebHostDefaults(webBuilder =>
            {
                //不显示respone中的server
                //webBuilder.UseKestrel(options => options.AddServerHeader = false);
                configure(webBuilder);
            });
            return @this;
        }
    }
}
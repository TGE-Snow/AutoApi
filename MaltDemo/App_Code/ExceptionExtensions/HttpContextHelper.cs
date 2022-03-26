using Malt.Logger;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MaltDemo.App_Code.AbstractClass
{
    /// <summary>
    /// Http上下文帮助类
    /// </summary>
    public static class HttpContextHelper
    {
        /// <summary>
        /// 获取Http上下文相关信息
        /// </summary>
        /// <param name="this">Http上下文</param>
        /// <returns>上下文信息</returns>
        public static async Task<HttpContextInfo> GetInfo(this HttpContext @this)
        {
            try
            {
                string url = $"{@this.Request.Scheme}://{@this.Request.Host.Value}{@this.Request.Path}{@this.Request.QueryString.Value}";
                string method = @this.Request.Method;
                //cookie
                List<string> cookies = new List<string>();
                foreach (var item in @this.Request.Cookies)
                {
                    cookies.Add($"{item.Key}:{item.Value}");
                }
                //header
                List<string> headers = new List<string>();
                foreach (var item in @this.Request.Headers)
                {
                    headers.Add($"{item.Key}:{item.Value}");
                }
                //body
                //@this.Request.EnableBuffering();
                string body;
                try
                {
                    using StreamReader sr = new StreamReader(@this.Request.Body);
                    Task<string> readTask = sr.ReadToEndAsync();
                    using var timeoutCancellationTokenSource = new CancellationTokenSource();
                    //超时时间为1000毫秒
                    var completedTask = await Task.WhenAny(readTask, Task.Delay(1000, timeoutCancellationTokenSource.Token));
                    if (completedTask == readTask)
                    {
                        timeoutCancellationTokenSource.Cancel();
                        body = await readTask;  // Very important in order to propagate exceptions
                    }
                    else
                    {
                        body = "读取body数据超时";
                    }
                }
                catch (Exception ex)
                {
                    body = $"读取body数据失败({ex.Message})";
                }
                //获取IP
                string ip;
                try
                {
                    ip = @this.Connection.RemoteIpAddress.ToString();
                }
                catch
                {
                    ip = null;
                }
                return new HttpContextInfo()
                {
                    IP = ip,
                    Url = url,
                    Method = method,
                    Cookies = cookies,
                    Headers = headers,
                    Body = body
                };
            }
            catch (Exception ex)
            {
                Startup.DefaultLogger.WriteCritical(ex, "GetHttpContextInfo");
                return null;
            }
        }
    }
}

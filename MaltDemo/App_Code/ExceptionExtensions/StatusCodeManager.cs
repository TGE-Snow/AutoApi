using Malt.Logger;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaltDemo.App_Code.AbstractClass
{
    /// <summary>
    /// 状态编号管理器
    /// </summary>
    public class StatusCodeManager
    {
        #region 单例模式

        /// <summary>
        /// 单例模式
        /// </summary>
        private static readonly Lazy<StatusCodeManager> lazy = new Lazy<StatusCodeManager>(() => new StatusCodeManager());

        /// <summary>
        /// 单例模式
        /// </summary>
        public static StatusCodeManager Instance { get { return lazy.Value; } }

        /// <summary>
        /// 构造函数
        /// </summary>
        private StatusCodeManager() { }

        #endregion 单例模式

        /// <summary>
        /// 拦截不同状态信息并处理
        /// 正常情况不会到这个地方
        /// 客户端错误：表示因客户端提供不正确的请求信息而导致服务器不能正常处理请求，响应状态码范围在400~499之间。
        /// 服务端错误：表示服务器在处理请求过程中因自身的问题而发生错误，响应状态码在500~599之间。
        /// </summary>
        /// <param name="context">上下文</param>
        /// <returns>处理任务</returns>
        public async Task StatusCodeHandler(HttpContext context)
        {
            ErrorInfo errorInfo = context.Response.StatusCode switch
            {
                400 => new ErrorInfo { StatusCode = context.Response.StatusCode, Message = "[Error]错误请求" },
                401 => new ErrorInfo { StatusCode = context.Response.StatusCode, Message = "[Error]身份验证错误" },
                404 => new ErrorInfo { StatusCode = context.Response.StatusCode, Message = "[Error]访问的页面不存在" },
                500 => new ErrorInfo { StatusCode = context.Response.StatusCode, Message = "[Error]服务器内部错误" },
                _ => new ErrorInfo { StatusCode = context.Response.StatusCode, Message = "[Error]未知错误" }
            };
            context.Response.StatusCode = 200;
            context.Response.ContentType = "application/json; charset=utf-8";
            await context.Response.WriteAsync(JsonConvert.SerializeObject(errorInfo), Encoding.UTF8);

            #region 写日志

            var feature = context.Features.Get<IExceptionHandlerFeature>();
            Exception ex = feature?.Error;
            //如果错误不为空
            if (ex != null)
            {
                Startup.DefaultLogger.WriteCritical(ex, await context.GetInfo(), errorInfo);
            }
            else
            {
                Startup.DefaultLogger.WriteCritical(await context.GetInfo(), errorInfo);
            }

            #endregion 写日志
        }
    }
}
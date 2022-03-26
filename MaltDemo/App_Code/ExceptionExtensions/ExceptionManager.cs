using Malt.Core;
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
    /// 异常管理器
    /// </summary>
    public class ExceptionManager
    {
        #region 单例模式
        /// <summary>
        /// 单例模式
        /// </summary>
        private static readonly Lazy<ExceptionManager> lazy = new Lazy<ExceptionManager>(() => new ExceptionManager());

        /// <summary>
        /// 单例模式
        /// </summary>
        public static ExceptionManager Instance { get { return lazy.Value; } }

        /// <summary>
        /// 构造函数
        /// </summary>
        private ExceptionManager() { }
        #endregion

        /// <summary>
        /// 拦截异常并处理
        /// </summary>
        /// <param name="context">上下文</param>
        /// <returns>处理任务</returns>
        public async Task ExceptionHandler(HttpContext context)
        {
            var feature = context.Features.Get<IExceptionHandlerFeature>();
            context.Response.StatusCode = 200;
            Exception ex = feature?.Error;
            ErrorInfo errorInfo;
            //如果是一般错误(可控的)
            if (ex is ErrorException)
            {
                ErrorException error = ex as ErrorException;
                errorInfo = new ErrorInfo { Message = error.Message, Data = error.Data.Count > 0 ? error.Data : null };
                //数据验证错误
                if (error is DataValidationException)
                {
                    errorInfo.StatusCode = 400;
                }
                //票据验证错误
                else if (error is TokenValidationException)
                {
                    errorInfo.StatusCode = 401;
                }
                //数据不存在错误
                else if (error is NotFoundException)
                {
                    errorInfo.StatusCode = 404;
                }
                //写日志
                if (error.CanWriteLog)
                {
                    error.WriteLog(await context.GetInfo(), errorInfo);
                }
                else
                {
                    error.WriteLog(logger: Startup.DefaultLogger, await context.GetInfo(), errorInfo);
                }
            }
            else
            //如果是非一般错误(意外错误)
            {
                errorInfo = new ErrorInfo { StatusCode = 500, Message = "服务器内部错误" };
                Startup.DefaultLogger.WriteCritical(ex, await context.GetInfo(), errorInfo);
            }
            context.Response.ContentType = "application/json; charset=utf-8";
            await context.Response.WriteAsync(JsonConvert.SerializeObject(errorInfo), Encoding.UTF8);
        }
    }
}

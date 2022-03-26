using Malt.Core;
using Malt.Logger;
using System;
using System.Threading.Tasks;

namespace MaltDemoFactoryCommon
{
    /// <summary>
    /// 广播日志接收器
    /// </summary>
    public class LogBroadcastReceiver : IBroadcastReceiver
    {
        /// <summary>
        /// 日志类型
        /// </summary>
        private readonly string _logType;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="logType">日志类型</param>
        public LogBroadcastReceiver(string logType)
        {
            _logType = logType;
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
        }

        /// <summary>
        /// 执行逻辑（以异步执行）
        /// </summary>
        /// <param name="content">广播接收内容</param>
        /// <returns></returns>
        public Task OnReceiveAsync(IBroadcastReceiverContent content)
        {
            content.BlContext.Logger.WriteWarning($"执行日志广播[{_logType}]");
            return null;
        }
    }
}
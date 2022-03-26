using Malt.Logger;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MaltDemo.App_Code.AbstractClass
{
    /// <summary>
    /// 上下文信息
    /// </summary>
    public class HttpContextInfo
    {
        /// <summary>
        /// IP地址
        /// </summary>
        public string IP { get; set; }

        /// <summary>
        /// 方法类型
        /// </summary>
        public string Method { get; set; }

        /// <summary>
        /// Url信息
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 传输信息
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// Cookies信息
        /// </summary>
        public List<string> Cookies { get; set; }

        /// <summary>
        /// 头信息
        /// </summary>
        public List<string> Headers { get; set; }
    }
}

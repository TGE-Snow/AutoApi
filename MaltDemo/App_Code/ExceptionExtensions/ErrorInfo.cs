using Malt.Core;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaltDemo.App_Code.AbstractClass
{
    /// <summary>
    /// 错误信息
    /// </summary>
    public class ErrorInfo : DefaultOutputData
    {
        /// <summary>
        /// 错误数据
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public IDictionary Data { get; set; } = null;
    }
}

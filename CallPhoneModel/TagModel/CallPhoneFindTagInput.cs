using Malt.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CallPhoneModel.TagModel
{
    public class CallPhoneFindTagInput : IInputData
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public Guid OUerID { get; set; }
    }
}

using Malt.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CallPhoneModel.GetUserInfoModel
{
    public class GetUserInfoInput : IInputData
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public Guid? OUerID { get; set; }

        /// <summary>
        /// 微信ID
        /// </summary>
        public string OpenID { get; set; }
    }
}

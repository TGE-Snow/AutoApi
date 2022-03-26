using CallPhoneModel.GetUserInfoModel;
using CallPhoneModel.TagModel;
using Malt.Core;
using MaltDemo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoApi.Controllers.CallPhone
{
    public class CallPhoneController : BusinessController
    {
        [HttpGet("UserInfo")]
        public IOutputData GetHot(string openid)
        {

            BlContext.InputData = new GetUserInfoInput()
            {
                OpenID = openid
            };

            CallPhoneFactory.Factory.RunForUserInfo(BlContext);

            return BlContext.OutputData;
        }

        [HttpGet("CallPhoneTag")]
        public IOutputData GetHot(Guid OUerID)
        {

            BlContext.InputData = new CallPhoneFindTagInput()
            {
                OUerID = OUerID
            };

            CallPhoneFactory.Factory.RunForFindTag(BlContext);

            return BlContext.OutputData;
        }


    }
}

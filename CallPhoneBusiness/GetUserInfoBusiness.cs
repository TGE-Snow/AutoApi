using CallPhoneModel.GetUserInfoModel;
using Malt.Core;
using MaltDemoBusinessCommon;
using System;

namespace CallPhoneBusiness
{
    public class GetUserInfoBusiness : BusinessManager
    {
        public GetUserInfoBusiness(IBusinessContext blContext) : base(blContext)
        {

        }

        public override void MainLogic()
        {
            GetUserInfoInput inputModel = (GetUserInfoInput)BlContext.InputData;
            GetUserInfoOutput outputModel = new();

            var keyValues = DbContext.GetFirstRow("CallPhoneUserInfo", "OUerID,OpenID,Lv", "OpenID=@OpenID", DbContext.NewParameter("@OpenID", inputModel.OpenID));

            if (keyValues != null || keyValues.Count > 0)
            {
                outputModel = new GetUserInfoOutput()
                {
                    OUerID = Guid.Parse(keyValues["OUerID"].ToString()),
                    OpenID = keyValues["OpenID"].ToString(),
                    Lv = int.Parse(keyValues["Lv"].ToString())
                };
            }
            else
            {
                outputModel = new GetUserInfoOutput()
                {
                    OpenID = inputModel.OpenID,
                    OUerID = Guid.NewGuid(),
                    Lv = 0
                };

                DbContext.Insert("CallPhoneUserInfo", outputModel);
            }
        }
    }
}

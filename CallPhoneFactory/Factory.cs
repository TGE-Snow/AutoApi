using CallPhoneBusiness;
using Malt.Core;
using System;

namespace CallPhoneFactory
{
    public static class Factory
    {
        public static void RunForUserInfo(IBusinessContext blContext)
        {
            using IBusinessManager businessManager = new GetUserInfoBusiness(blContext);
            businessManager.Run();
        }
        public static void RunForFindTag(IBusinessContext blContext)
        {
            using IBusinessManager businessManager = new CallPhoneTagBusiness(blContext);
            businessManager.Run();
        }
    }

}

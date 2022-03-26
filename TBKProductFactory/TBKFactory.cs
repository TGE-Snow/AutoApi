using Malt.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBKBusiness.CreatTklLogic;
using TBKBusiness.GetOrderLogic;
using TBKBusiness.OrderLogic;
using TBKProductBusiness.HotLogic;
using TBKProductBusiness.TklLogic;

namespace TBKProductFactory
{
    public static class TBKFactory
    {
        public static void RunForTkl(IBusinessContext blContext)
        {
            using IBusinessManager businessManager = new TklBusiness(blContext);
            businessManager.Run();
        }

        public static void RunForHot(IBusinessContext blContext)
        {
            using IBusinessManager businessManager = new HotBusiness(blContext);
            businessManager.Run();
        }

        public static void RunForOrder(IBusinessContext blContext)
        {
            using IBusinessManager businessManager = new OrderBusiness(blContext);
            businessManager.Run();
        }

        public static void RunForGetOrder(IBusinessContext blContext)
        {
            using IBusinessManager businessManager = new GetOrderBusiness(blContext);
            businessManager.Run();
        }

        public static void RunForCreatTkl(IBusinessContext blContext)
        {
            using IBusinessManager businessManager = new CreatTklBusiness(blContext);
            businessManager.Run();
        }

    }
}

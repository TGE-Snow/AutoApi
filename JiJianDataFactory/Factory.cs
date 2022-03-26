using JiJianDataBusiness.ProdutCostLogic;
using Malt.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiJianDataFactory
{
    public static class Factory
    {
        public static void RunForProductCost(IBusinessContext blContext)
        {
            using IBusinessManager businessManager = new ProductCostBusiness(blContext);
            businessManager.Run();
        }

        public static void RunForGetCost(IBusinessContext blContext)
        {
            using IBusinessManager businessManager = new GetCostBusiness(blContext);
            businessManager.Run();
        }

        public static void RunForChangeCost(IBusinessContext blContext)
        {
            using IBusinessManager businessManager = new ChangeCostBusiness(blContext);
            businessManager.Run();
        }
    }
}

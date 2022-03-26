using AiMiYun.Data;
using JiJianDataModel.ProductCostModel;
using Malt.Core;
using MaltDemoBusinessCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiJianDataBusiness.ProdutCostLogic
{
    public class ChangeCostBusiness : BusinessManager
    {
        public ChangeCostBusiness(IBusinessContext blContext) : base(blContext)
        {
        }

        public override void MainLogic()
        {
            ChangeCostInput inputModel = BlContext.InputData as ChangeCostInput;

            foreach (var item in inputModel.CostLists)
            {
                switch (item.OrderType)
                {
                    case 1:
                        dynamic inmodel = new DynamicModel();
                        inmodel.PID = Guid.NewGuid();
                        inmodel.ProductName = item.ProductName;
                        inmodel.Cost = item.Cost;
                        DbContext.Insert("ProductCost", inmodel);
                        break;

                    case 2:
                        dynamic upmodel = new DynamicModel();
                        upmodel.ProductName = item.ProductName;
                        upmodel.Cost = item.Cost;
                        DbContext.Update("ProductCost", upmodel, "PID=@PID", DbContext.NewParameter("@PID", item.PID));
                        break;

                    case 3:
                        DbContext.Delete("ProductCost", "PID=@PID", DbContext.NewParameter("@PID", item.PID));
                        break;
                }
            }

            BlContext.OutputData = new DefaultOutputData();
        }
    }
}

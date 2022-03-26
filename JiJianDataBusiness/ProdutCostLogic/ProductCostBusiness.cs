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
    public class ProductCostBusiness : BusinessManager
    {
        public ProductCostBusiness(IBusinessContext blContext) : base(blContext)
        {
        }

        public override void MainLogic()
        {
            ProductCostInput inputModel = BlContext.InputData as ProductCostInput;

            ProductCostOutput outputModel = new ProductCostOutput();

            outputModel.List = new List<Pro>();

            outputModel.Sum = 0;

            foreach (Product item in inputModel.Product)
            {
                var cost = DbContext.GetFirstCell("ProductCost", "Cost", "ProductName=@ProductName", DbContext.NewParameter("@ProductName", item.Name));

                if (cost != null && cost is decimal)
                {
                    decimal n = decimal.Parse(cost.ToString());

                    decimal allCost = n * item.Quantity;
                    outputModel.List.Add(new Pro
                    {
                        Name = item.Name,
                        Quantity = item.Quantity,
                        Cost = n,
                        AllCost = allCost
                    });
                    outputModel.Sum += allCost;
                }

            }

            BlContext.OutputData = outputModel;
        }
    }

}

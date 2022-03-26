using JiJianDataFactory;
using JiJianDataModel.ProductCostModel;
using Malt.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaltDemo.Controllers.JiJianData
{
    public class ProductController : BusinessController
    {

        [HttpPost("ProductCost")]
        public IOutputData PostProductCost(ProductCostInput inputModel)
        {
            BlContext.InputData = inputModel;

            Factory.RunForProductCost(BlContext);

            return BlContext.OutputData;
        }

        [HttpGet("Cost")]
        public IOutputData GetCost()
        {
            Factory.RunForGetCost(BlContext);
            return BlContext.OutputData;
        }

        [HttpPost("Cost")]
        public IOutputData PostCost([FromBody] ChangeCostInput inputModel)
        {
            BlContext.InputData = inputModel;
            Factory.RunForChangeCost(BlContext);
            return BlContext.OutputData;
        }
    }
}

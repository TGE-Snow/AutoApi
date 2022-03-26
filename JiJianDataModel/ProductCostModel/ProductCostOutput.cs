using Malt.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace JiJianDataModel.ProductCostModel
{
    public class ProductCostOutput : DefaultOutputData
    {
        public List<Pro> List { get; set; }

        public decimal Sum { get; set; }
    }

    public class Pro
    {
        public string Name { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public int Quantity { get; set; }

        public decimal Cost { get; set; }

        public decimal AllCost { get; set; }
    }
}

using Malt.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiJianDataModel.ProductCostModel
{
    public class ChangeCostInput : IInputData
    {
        public List<CostList> CostLists { get; set; }
    }

    public class CostList
    {
        public string ProductName { get; set; }

        public decimal Cost { get; set; }

        public Guid PID { get; set; }

        public int OrderType { get; set; }
    }
}

using Malt.Core;
using MaltDemoBusinessCommon;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiJianDataBusiness.ProdutCostLogic
{
    public class GetCostBusiness : BusinessManager
    {
        public GetCostBusiness(IBusinessContext blContext) : base(blContext)
        {
        }

        public override void MainLogic()
        {
            CcList outputModel = new CcList();

            outputModel.List = new List<CostList>();

            DataTable dataTable = DbContext.GetDataTable("SELECT * from ProductCost");

            foreach (DataRow item in dataTable.Rows)
            {
                outputModel.List.Add(new CostList
                {
                    ProductName = item.Field<string>("ProductName"),
                    Cost = item.Field<decimal>("Cost"),
                    PID = item.Field<Guid>("PID"),
                });
            }

            BlContext.OutputData = outputModel;
        }
    }

    public class CcList : DefaultOutputData
    {
        public List<CostList> List { get; set; }
    }

    public class CostList
    {
        public string ProductName { get; set; }

        public decimal Cost { get; set; }

        public Guid PID { get; set; }
    }
}
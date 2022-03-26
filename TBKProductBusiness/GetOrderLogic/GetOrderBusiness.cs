using Malt.Core;
using MaltDemoBusinessCommon;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBKModel.GetOrderModel;

namespace TBKBusiness.GetOrderLogic
{
    public class GetOrderBusiness : BusinessManager
    {
        public GetOrderBusiness(IBusinessContext blContext) : base(blContext)
        {
        }

        public override void MainLogic()
        {
            GetOrderInput inputModel = BlContext.InputData as GetOrderInput;
            GetOrderOutput outputModel = new GetOrderOutput();

            List<DbParameter> dbs = new List<DbParameter>();

            StringBuilder str = new StringBuilder();
            str.Append("SELECT itemImg,itemTitle,sellerShopTitle,tkPaidTime,alimamaRate,tkEarningTime,flowSource,tkStatus,tkTotalRate,tkStatusText,alipayTotalPrice,pubSharePreFee,totalCommissionRate,payPrice,totalCommissionFee FROM TBK_Order");
            if (inputModel.tkStatus != null)
            {
                str.Append("  WHERE tkStatus = @tkStatus ");
                dbs.Add(DbContext.NewParameter("@tkStatus", inputModel.tkStatus));
            }

            str.Append(" ORDER BY tkCreateTime OFFSET((@PageNo -1) *20) ROWS FETCH NEXT 20 ROWS ONLY ");
            dbs.Add(DbContext.NewParameter("@PageNo", inputModel.PageNo));

            DataTable order = DbContext.GetDataTable(str.ToString(), dbs.ToArray());

            if (order != null && order.Rows.Count > 0)
            {
                outputModel.Orders = new List<Order>();
                foreach (DataRow item in order.Rows)
                {
                    //抽成比
                    decimal commission = (95 - item.Field<decimal>("alimamaRate")) / 100;

                    //预估返利
                    decimal pubSharePreFee = ToDecimal(item.Field<decimal>("pubSharePreFee") * commission);

                    //佣金比
                    decimal tkTotalRate = ToDecimal(item.Field<decimal>("tkTotalRate") * commission);



                    decimal? totalCommissionFee = null;

                    //if (item.Field<long>("tkStatus") == 255)
                    //{
                    totalCommissionFee = ToDecimal(item.Field<decimal>("totalCommissionFee") * commission);
                    //}

                    Order od = new Order()
                    {
                        itemImg = item.Field<string>("itemImg"),
                        itemTitle = item.Field<string>("itemTitle"),
                        sellerShopTitle = item.Field<string>("sellerShopTitle"),
                        flowSource = item.Field<string>("flowSource"),
                        tkStatus = item.Field<long>("tkStatus"),
                        tkStatusText = item.Field<string>("tkStatusText"),
                        alipayTotalPrice = item.Field<decimal>("alipayTotalPrice"),
                        tkTotalRate = tkTotalRate,
                        pubSharePreFee = pubSharePreFee,
                        payPrice = item.Field<decimal?>("payPrice"),
                        totalCommissionFee = totalCommissionFee,
                        tkPaidTime = item.Field<DateTime>("tkPaidTime"),
                        tkEarningTime = item.Field<DateTime?>("tkEarningTime")
                    };

                    outputModel.Orders.Add(od);
                }
            }

            BlContext.OutputData = outputModel;
        }
    }
}

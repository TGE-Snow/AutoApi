using Malt.Core;
using MaltDemoBusinessCommon;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBKModel.OrderModel;
using Top.Api;
using Top.Api.Request;
using Top.Api.Response;

namespace TBKBusiness.OrderLogic
{
    public class OrderBusiness : BusinessManager
    {
        public OrderBusiness(IBusinessContext blContext) : base(blContext) { }

        public override void MainLogic()
        {
            OrderInput inputModel = BlContext.InputData as OrderInput;

            DateTime unrecordedDate = DateTime.Now.AddDays(1 - DateTime.Now.Day).AddDays(-1);

            if (inputModel.tkEarningTime != null && inputModel.tkEarningTime < unrecordedDate)
            {
                inputModel.tkStatus = 255;
                inputModel.tkStatusText = "已结算";
            }

            switch (inputModel.tkStatus)
            {
                case 14:
                    inputModel.tkStatusText = "已到货";
                    break;
                case 3:
                    inputModel.tkStatusText = "已收货";
                    break;
            }


            Dictionary<string, object> Order = DbContext.GetFirstRow("TBK_Order", "OrderID,UserID,tge_Amount,tkStatus", "tradeParentId =@tradeParentId", DbContext.NewParameter("@tradeParentId", inputModel.tradeParentId));

            if (Order == null || Order["UserID"] == null)
            {
                DataTable token = DbContext.GetDataTable("SELECT access_token,UserID,TbUserID FROM TaoBao INNER JOIN [User] ON [User].TaoBaoID = TaoBao.TaoBaoID WHERE [User].UserID IN ( SELECT DISTINCT UserID FROM tbk_Commodity_Cache WHERE ED < @ED AND Pid = @Pid)", DbContext.NewParameter("@ED", inputModel.tkCreateTime.AddDays(1)), DbContext.NewParameter("Pid", inputModel.itemId));

                if (token != null)
                {

                    for (int i = 0; i < token.Rows.Count; i++)
                    {
                        DataRow item = token.Rows[i];
                        ITopClient client = new DefaultTopClient("https://eco.taobao.com/router/rest", AppKey.ToString(), AppSecret);
                        OpenuidGetBytradeRequest req = new OpenuidGetBytradeRequest();
                        req.Tid = long.Parse(inputModel.tradeId);
                        OpenuidGetBytradeResponse rsp = client.Execute(req, item["access_token"].ToString());
                        if (!string.IsNullOrWhiteSpace(rsp.OpenUid) && rsp.OpenUid == item["TbUserID"].ToString())
                        {
                            inputModel.UserID = Guid.Parse(item["UserID"].ToString());
                            break;
                        }
                    }
                }
            }

            if (Order == null)
            {
                inputModel.OrderID = Guid.NewGuid();
                DbContext.Insert("TBK_Order", inputModel);
            }
            else
            {
                if (Order["tkStatus"].ToString() != "255")
                {
                    if (inputModel.tkStatus == 255)
                    {
                        decimal amount = Math.Truncate(((inputModel.totalCommissionFee - inputModel.alimamaShareFee) * (decimal)0.1) * 100) / 100;

                        inputModel.tge_Amount = amount;
                        DbContext.ExecuteQuantity("UPDATE [User] SET ShouldAmount += @Amount,UnrecordedAmount -= @Amount WHERE UserID = @UserID", DbContext.NewParameter("@UserID", Order["UserID"].ToString()), DbContext.NewParameter("@Amount", amount));
                    }

                    inputModel.OrderID = Guid.Parse(Order["OrderID"].ToString());
                    inputModel.UserID = Guid.Parse(Order["UserID"].ToString());
                    DbContext.Update("TBK_Order", inputModel);

                }
            }
            BlContext.OutputData = new DefaultOutputData();
        }
    }
}

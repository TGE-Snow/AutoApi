using Malt.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TBKModel.GetOrderModel
{
    public class GetOrderOutput : DefaultOutputData
    {
        public List<Order> Orders { get; set; }
    }

    public class Order
    {
        public string itemImg { get; set; }
        public string itemTitle { get; set; }
        public string sellerShopTitle { get; set; }
        public string flowSource { get; set; }
        public long tkStatus { get; set; }
        public string tkStatusText { get; set; }
        public decimal alipayTotalPrice { get; set; }
        public decimal pubSharePreFee { get; set; }
        public decimal tkTotalRate { get; set; }
        public decimal totalCommissionRate { get; set; }
        public decimal? payPrice { get; set; }
        public decimal? totalCommissionFee { get; set; }
        public DateTime tkPaidTime { get; set; }
        public DateTime? tkEarningTime { get; set; }
    }
}

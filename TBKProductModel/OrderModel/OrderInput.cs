using Malt.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TBKModel.OrderModel
{
    public class OrderInput : IInputData
    {
        /// <summary>
        ///订单id
        /// </summary>
        public Guid OrderID { get; set; }

        /// <summary>
        ///用户id
        /// </summary>
        public Guid? UserID { get; set; }

        /// <summary>
        ///平台抽成
        /// </summary>
        public decimal? tge_Amount { get; set; }

        /// <summary>
        ///买家确认收货的付款金额（不包含运费金额）
        /// </summary>
        public decimal payPrice { get; set; }

        /// <summary>
        ///订单付款的时间，该时间同步淘宝，可能会略晚于买家在淘宝的订单创建时间
        /// </summary>
        public DateTime tkPaidTime { get; set; }

        /// <summary>
        ///结算预估收入=结算金额*提成。以买家确认收货的付款金额为基数，预估您可能获得的收入。因买家退款、您违规推广等原因，可能与您最终收入不一致。最终收入以月结后您实际收到的为准
        /// </summary>
        public decimal pubShareFee { get; set; }

        /// <summary>
        ///订单确认收货后且商家完成佣金支付的时间
        /// </summary>
        public DateTime? tkEarningTime { get; set; }

        /// <summary>
        ///预估内容专项服务费：内容场景专项技术服务费，内容推广者在内容场景进行推广需要支付给阿里妈妈专项的技术服务费用。专项服务费＝付款金额＊专项服务费率。
        /// </summary>
        public decimal tkCommissionPreFeeForMediaPlatform { get; set; }

        /// <summary>
        ///	预售时期，用户对预售商品支付定金的付款时间，可能略晚于在淘宝付定金时间
        /// </summary>
        public string tkDepositTime { get; set; }

        /// <summary>
        ///提成=收入比率*分成比率。指实际获得收益的比率
        /// </summary>
        public decimal tkTotalRate { get; set; }

        /// <summary>
        ///商品所属的一级类目名称
        /// </summary>
        public string itemCategoryName { get; set; }

        /// <summary>
        ///掌柜旺旺
        /// </summary>
        public string sellerNick { get; set; }

        /// <summary>
        ///推广者赚取佣金后支付给阿里妈妈的技术服务费用的比率
        /// </summary>
        public decimal alimamaRate { get; set; }

        /// <summary>
        ///平台出资方，如天猫、淘宝、或聚划算等
        /// </summary>
        public string subsidyType { get; set; }

        /// <summary>
        ///付款预估收入=付款金额*提成。指买家付款金额为基数，预估您可能获得的收入。因买家退款等原因，可能与结算预估收入不一致
        /// </summary>
        public decimal pubSharePreFee { get; set; }

        /// <summary>
        ///买家拍下付款的金额（不包含运费金额）
        /// </summary>
        public decimal alipayTotalPrice { get; set; }

        /// <summary>
        ///补贴金额=结算金额*补贴比率
        /// </summary>
        public decimal subsidyFee { get; set; }

        /// <summary>
        ///技术服务费=结算金额*收入比率*技术服务费率。推广者赚取佣金后支付给阿里妈妈的技术服务费用
        /// </summary>
        public decimal alimamaShareFee { get; set; }

        /// <summary>
        ///订单所属平台类型，包括天猫、淘宝、聚划算等
        /// </summary>
        public string orderType { get; set; }

        /// <summary>
        ///订单创建的时间，该时间同步淘宝，可能会略晚于买家在淘宝的订单创建时间
        /// </summary>
        public DateTime tkCreateTime { get; set; }

        /// <summary>
        ///通过推广链接达到商品、店铺详情页的点击时间
        /// </summary>
        public DateTime clickTime { get; set; }

        /// <summary>
        ///成交平台
        /// </summary>
        public string terminalType { get; set; }

        /// <summary>
        ///	商品单价
        /// </summary>
        public decimal itemPrice { get; set; }

        /// <summary>
        ///	预售时期，用户对预售商品支付的定金金额
        /// </summary>
        public decimal depositPrice { get; set; }

        /// <summary>
        ///营销类型：该字段中视订单情况有单个或多个值。 例如：淘礼金（自助充值），特价版客户端染色，特价版客户端锁粉，特价版客户端推广。
        /// </summary>
        public string marketingType { get; set; }

        /// <summary>
        ///	已付款：指订单已付款，但还未确认收货 已收货：指订单已确认收货，但商家佣金未支付 已结算：指订单已确认收货，且商家佣金已支付成功 已失效：指订单关闭/订单佣金小于0.01元，订单关闭主要有：1）买家超时未付款； 2）买家付款前，买家/卖家取消了订单；3）订单付款后发起售中退款成功；3：订单结算，12：订单付款， 13：订单失效，14：订单成功
        /// </summary>
        public int tkStatus { get; set; }

        /// <summary>
        ///结算内容专项服务费：内容场景专项技术服务费，内容推广者在内容场景进行推广需要支付给阿里妈妈专项的技术服务费用。专项服务费＝结算金额＊专项服务费率。
        /// </summary>
        public decimal tkCommissionFeeForMediaPlatform { get; set; }

        /// <summary>
        ///商品链接
        /// </summary>
        public string itemLink { get; set; }

        /// <summary>
        ///媒体管理下的ID，同时也是pid=mm_1_2_3中的“2”这段数字
        /// </summary>
        public string siteId { get; set; }

        /// <summary>
        ///状态文本
        /// </summary>
        public string tkStatusText { get; set; }

        /// <summary>
        ///内容专项服务费率：内容场景专项技术服务费率，内容推广者在内容场景进行推广需要按结算金额支付一定比例给阿里妈妈作为内容场景专项技术服务费，用于提供与内容平台实现产品技术对接等服务。
        /// </summary>
        public decimal tkCommissionRateForMediaPlatform { get; set; }

        /// <summary>
        ///淘宝客订单角色文本
        /// </summary>
        public string tkOrderRoleText { get; set; }

        /// <summary>
        ///	订单在淘宝拍下付款的时间
        /// </summary>
        public DateTime tbPaidTime { get; set; }

        /// <summary>
        ///买家通过购物车购买的每个商品对应的订单编号，此订单编号并未在淘宝买家后台透出
        /// </summary>
        public string tradeId { get; set; }

        /// <summary>
        ///淘宝客订单角色
        /// </summary>
        public int tkOrderRole { get; set; }

        /// <summary>
        ///	推广位管理下的推广位名称对应的ID，同时也是pid=mm_1_2_3中的“3”这段数字
        /// </summary>
        public string adzoneId { get; set; }

        /// <summary>
        ///	从结算佣金中分得的收益比率
        /// </summary>
        public decimal pubShareRate { get; set; }

        /// <summary>
        ///	预售时期，用户对预售商品支付定金的付款时间
        /// </summary>
        public string tbDepositTime { get; set; }

        /// <summary>
        ///平台类型文本
        /// </summary>
        public string itemPlatformTypeText { get; set; }

        /// <summary>
        ///维权标签，0 含义为非维权 1 含义为维权订单
        /// </summary>
        public int refundTag { get; set; }

        /// <summary>
        ///预收(注释不足)
        /// </summary>
        public int preSell { get; set; }

        /// <summary>
        ///平台给与的补贴比率，如天猫、淘宝、聚划算等
        /// </summary>
        public decimal subsidyRate { get; set; }

        /// <summary>
        ///	推广者的会员id
        /// </summary>
        public string pubId { get; set; }

        /// <summary>
        ///商品图片
        /// </summary>
        public string itemImg { get; set; }

        /// <summary>
        ///订单是否为激励池订单 1，表征是 0，表征否
        /// </summary>
        public int isLx { get; set; }

        /// <summary>
        ///商品标题
        /// </summary>
        public string itemTitle { get; set; }

        /// <summary>
        ///媒体管理下的对应ID的自定义名称
        /// </summary>
        public string siteName { get; set; }

        /// <summary>
        ///商品数量
        /// </summary>
        public int itemNum { get; set; }

        /// <summary>
        ///(注释不足)
        /// </summary>
        public int tkBizTag { get; set; }

        /// <summary>
        ///买家在淘宝后台显示的订单编号
        /// </summary>
        public string tradeParentId { get; set; }

        /// <summary>
        ///	产品类型
        /// </summary>
        public string flowSource { get; set; }

        /// <summary>
        ///订单更新时间
        /// </summary>
        public DateTime modifiedTime { get; set; }

        /// <summary>
        ///商品id
        /// </summary>
        public string itemId { get; set; }

        /// <summary>
        ///推广位管理下的自定义推广位名称
        /// </summary>
        public string adzoneName { get; set; }

        /// <summary>
        ///	佣金比率
        /// </summary>
        public decimal totalCommissionRate { get; set; }

        /// <summary>
        ///店铺名称
        /// </summary>
        public string sellerShopTitle { get; set; }

        /// <summary>
        ///(注释不足)
        /// </summary>
        public bool supportItemClick { get; set; }

        /// <summary>
        ///订单结算的佣金比率+平台的补贴比率
        /// </summary>
        public decimal incomeRate { get; set; }

        /// <summary>
        ///佣金金额=结算金额*佣金比率
        /// </summary>
        public decimal totalCommissionFee { get; set; }
    }

}
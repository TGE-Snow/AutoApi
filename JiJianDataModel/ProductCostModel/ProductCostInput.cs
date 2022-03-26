using Malt.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace JiJianDataModel.ProductCostModel
{
    public class ProductCostInput : IInputData
    {
        /// <summary>
        /// 商品
        /// </summary>
        public List<Product> Product { get; set; }


    }

    public class Product
    {

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public int Quantity { get; set; }
    }
}

using Malt.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TBKModel.GetOrderModel
{
    public class GetOrderInput : IInputData
    {
        public int PageNo { get; set; }

        public int? tkStatus { get; set; }
    }
}

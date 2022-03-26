using Malt.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TBKProductModel.HotModel
{
    public class HotInput : IInputData
    {
        public long PageNo { get; set; }

        public long MaterialId { get; set; }
    }
}

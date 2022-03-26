using Malt.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Top.Api.Response;

namespace TBKProductModel.HotModel
{
    public class HotOutput : DefaultOutputData
    {
        public TbkDgOptimusMaterialResponse tbkDgOptimusMaterial { get; set; }
    }
}

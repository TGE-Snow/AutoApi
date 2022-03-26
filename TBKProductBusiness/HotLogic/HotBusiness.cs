using Malt.Core;
using MaltDemoBusinessCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBKProductModel.HotModel;
using Top.Api;
using Top.Api.Request;
using Top.Api.Response;

namespace TBKProductBusiness.HotLogic
{
    public class HotBusiness : BusinessManager
    {
        public HotBusiness(IBusinessContext blContext) : base(blContext) { }

        public override void MainLogic()
        {
            HotInput inputModel = BlContext.InputData as HotInput;
            HotOutput outputModel = new HotOutput();

            ITopClient client = new DefaultTopClient("https://eco.taobao.com/router/rest", AppKey.ToString(), AppSecret);
            TbkDgOptimusMaterialRequest req = new TbkDgOptimusMaterialRequest();
            req.PageSize = 20L;
            req.AdzoneId = 111426350068L;
            req.PageNo = inputModel.PageNo;
            req.MaterialId = inputModel.MaterialId;
            TbkDgOptimusMaterialResponse rsp = client.Execute(req);
            outputModel.tbkDgOptimusMaterial = rsp;

            BlContext.OutputData = outputModel;
        }
    }
}

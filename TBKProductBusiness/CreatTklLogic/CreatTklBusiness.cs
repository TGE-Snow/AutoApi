using AiMiYun.Data;
using Malt.Core;
using MaltDemoBusinessCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBKModel.CreatTklModel;

namespace TBKBusiness.CreatTklLogic
{
    public class CreatTklBusiness : BusinessManager
    {
        public CreatTklBusiness(IBusinessContext blContext) : base(blContext)
        {
        }

        public override void MainLogic()
        {
            CreatTklInput inputModel = BlContext.InputData as CreatTklInput;

            dynamic model = new DynamicModel();
            model.UserID = inputModel.UID;
            model.Pid = inputModel.PID;
            model.ED = inputModel.ED;

            DbContext.Insert("tbk_Commodity_Cache", model);

            BlContext.OutputData = new DefaultOutputData();
        }
    }
}

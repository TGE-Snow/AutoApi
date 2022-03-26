using CallPhoneModel.TagModel;
using Malt.Core;
using MaltDemoBusinessCommon;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CallPhoneBusiness
{
    public class CallPhoneTagBusiness : BusinessManager
    {
        public CallPhoneTagBusiness(IBusinessContext blContext) : base(blContext)
        {

        }

        public override void MainLogic()
        {
            CallPhoneFindTagInput inputModel = (CallPhoneFindTagInput)BlContext.InputData;
            CallPhoneTagOutput outputModel = new();

            DataTable keyValues = DbContext.GetDataTable("SELECT OUerID,Name,Color,Value FROM CallPhoneTag WHERE OUerID=@OUerID", DbContext.NewParameter("@OUerID", inputModel.OUerID));

            outputModel.Tags = new();
            outputModel.Tags.Add(new Tag()
            {
                Name = "已拨",
                Color = "0000ff",
                Value = 1
            });
            outputModel.Tags.Add(new Tag()
            {
                Name = "未拨",
                Color = "DC143C",
                Value = 2
            });

            if (keyValues != null || keyValues.Rows.Count > 0)
            {
                foreach (DataRow item in keyValues.Rows)
                {
                    outputModel.Tags.Add(new Tag()
                    {
                        Name = item.Field<string>("Name"),
                        Color = item.Field<string>("Color"),
                        Value = item.Field<int>("Value")
                    });
                }
            }
        }

    }
}

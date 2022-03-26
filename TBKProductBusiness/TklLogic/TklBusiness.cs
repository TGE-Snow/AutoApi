using AiMiYun.Data;
using Malt.Core;
using MaltDemo.Controllers;
using MaltDemoBusinessCommon;
using System;
using System.Text.RegularExpressions;
using TBKProductModel.TklModel;

namespace TBKProductBusiness.TklLogic
{
    public class TklBusiness : BusinessManager
    {
        Help help = null;
        public TklBusiness(IBusinessContext blContext) : base(blContext)
        {
            help = new Help(AppKey, AppSecret);
        }

        public override void MainLogic()
        {
            TklInput inputModel = BlContext.InputData as TklInput;
            TklOutput outputModel = new TklOutput();

            Regex reg = new Regex(@"[a-zA-z]+://[^\s]*");
            Match match = reg.Match(inputModel.Text);
            if (match.Value.Contains("m.tb.cn"))
            {
                outputModel.DataDomain = new Top.Api.Response.TbkDgMaterialOptionalResponse.MapDataDomain();
                var item = help.GetNumids(match.Value); ;

                outputModel.DataDomain = item.mapDataDomain;
                outputModel.Model = item.tbkTpwdCreateResponse.Data.Model;
                outputModel.PasswordSimple = item.tbkTpwdCreateResponse.Data.PasswordSimple;


                Regex reg1 = new Regex(@"(https?|ftp|file)://[-A-Za-z0-9+&@#/%?=~_|!:,.;]+[-A-Za-z0-9+&@#/%=~_|]");
                Match match1 = reg1.Match(item.tbkTpwdCreateResponse.Data.Model);
                Regex r = new Regex(@"^ *(https|http|ftp|rtsp|mms):", RegexOptions.IgnoreCase);
                outputModel.ModelUrl = r.Replace(match1.Value, "");
            }
            else
            {
                throw new DataValidationException("淘口令格式错误", BlContext.Logger);
            }

            dynamic model = new DynamicModel();
            model.UserID = inputModel.UserID;
            model.ED = DateTime.Now;
            model.Pid = outputModel.DataDomain.ItemId;
            DbContext.Insert("tbk_Commodity_Cache", model);

            BlContext.OutputData = outputModel;
        }
    }
}
using Malt.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TBKModel.CreatTklModel;
using TBKModel.GetOrderModel;
using TBKModel.OrderModel;
using TBKProductFactory;
using TBKProductModel.HotModel;
using TBKProductModel.TklModel;

namespace MaltDemo.Controllers.TBKProduct
{
    public class TBKController : BusinessController
    {
        [HttpGet("hot")]
        public IOutputData GetHot(long? pageno, long? materialId)
        {
            HotInput inputModel = new HotInput();
            if (pageno != null)
            {
                inputModel.PageNo = (long)pageno;
            }
            else
            {
                inputModel.PageNo = 1;
            }
            if (materialId != null)
            {
                inputModel.MaterialId = (long)materialId;
            }
            else
            {
                inputModel.MaterialId = 27446;
            }

            BlContext.InputData = inputModel;
            TBKFactory.RunForHot(BlContext);

            return BlContext.OutputData;
        }

        [HttpGet("tkl")]
        public IOutputData GetTkl(string text, Guid pass)
        {

            if (string.IsNullOrWhiteSpace(text))
            {
                throw new DataValidationException("淘口令格式错误", BlContext.Logger);
            }

            if (pass == Guid.Empty)
            {
                throw new DataValidationException("淘口令链接格式错误", BlContext.Logger);
            }

            TklInput inputModel = new TklInput();

            inputModel.Text = text;
            inputModel.UserID = pass;
            BlContext.InputData = inputModel;
            TBKFactory.RunForTkl(BlContext);

            return BlContext.OutputData;
        }

        [HttpPost("Order")]
        public IOutputData PostOrder([FromBody] OrderInput inputModel)
        {
            if (inputModel != null)
            {
                BlContext.InputData = inputModel;
                TBKFactory.RunForOrder(BlContext);
            }
            else
            {
                throw new DataValidationException("参数有误", BlContext.Logger);
            }
            return BlContext.OutputData;
        }

        [HttpGet("order")]
        public IOutputData GetOrder(int? pageNo, int? tkStatus)
        {
            GetOrderInput inputModel = new GetOrderInput();

            if (pageNo == null)
            {
                pageNo = 1;
            }
            inputModel.PageNo = (int)pageNo;
            inputModel.tkStatus = tkStatus;

            BlContext.InputData = inputModel;

            TBKFactory.RunForGetOrder(BlContext);

            return BlContext.OutputData;
        }


        [HttpPost("creattkl")]
        public IOutputData PostCreatTkl([FromBody] CreatTklInput inputModel)
        {
            if (inputModel.ED == null)
            {
                inputModel.ED = DateTime.Now;
            }

            BlContext.InputData = inputModel;

            TBKFactory.RunForCreatTkl(BlContext);

            return BlContext.OutputData;
        }
    }
}

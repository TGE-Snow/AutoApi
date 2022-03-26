using Malt.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Top.Api.Request;
using Top.Api.Response;
using static Top.Api.Response.TbkDgMaterialOptionalResponse;

namespace TBKProductModel.TklModel
{
    public class TklOutput : DefaultOutputData
    {
        public MapDataDomain DataDomain { get; set; }

        public string Model { get; set; }

        public string ModelUrl { get; set; }

        public string PasswordSimple { get; set; }
    }
}

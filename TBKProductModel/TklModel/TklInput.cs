using Malt.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TBKProductModel.TklModel
{
    public class TklInput : IInputData
    {
        public string Text { get; set; }

        public Guid UserID { get; set; }
    }
}

using Malt.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CallPhoneModel.TagModel
{
    public class CallPhoneInsterTag : IInputData
    {
        public string Name { get; set; }

        public string Color { get; set; }

        public int? Value { get; set; }
    }
}

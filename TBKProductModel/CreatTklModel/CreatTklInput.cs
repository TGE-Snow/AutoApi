using Malt.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TBKModel.CreatTklModel
{
    public class CreatTklInput : IInputData
    {
        public Guid UID { get; set; }

        public long PID { get; set; }

        public DateTime? ED { get; set; }
    }
}

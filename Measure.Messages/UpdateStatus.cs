using NServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Measure.Messages
{
    public class UpdateStatus: ICommand
    {
        public int MeasureID { get; set; }
        public bool Success { get; set; }
    }
}

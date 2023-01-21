using NServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Measure.Messages.Events;

public class MeasureAdded
{
    public int MeasureId { get; set; }
    public int CardId { get; set; }
    public float Weight { get; set; }
    public DateTime Date { get; set; }
    public string Comment { get; set; }
}

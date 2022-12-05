using Measure.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Measure.Services.Interfaces
{
    public interface IMeasureService
    {
        Task<int> AddMeasure(MeasureModel newMeasure);
        Task UpdateStatus(int measureID, Status status);
    }
}

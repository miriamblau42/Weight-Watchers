using Measure.Services.Models;

namespace Measure.Services;

public interface IMeasureService
{
    public Task<int> AddNewMeasure(MeasureModel newMeasure);

    void UpdateMeasureStatus(int measureId, bool succeeded);

}
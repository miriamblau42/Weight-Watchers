using Measure.Data.Entities;

namespace Measure.Data
{
    public interface IMeasureData
    {
        Task<int> AddNewMeasure(Entities.Measure newMeasure);

        Task UpdateMeasureStatus(int measureId, EStatus status);

    }
}
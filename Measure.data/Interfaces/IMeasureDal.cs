
namespace Measure.Data.Interfaces
{
    public interface IMeasureDal
    {
        Task<int> AddMeasure(Entities.Measure measure);
        Task UpdateStatus(int iD, Entities.Status status);
    }
}

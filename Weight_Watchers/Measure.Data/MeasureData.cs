using Measure.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Measure.Data;

public class MeasureData : IMeasureData
{
    private IDbContextFactory<MeasureContext> _factory;

    public MeasureData(IDbContextFactory<MeasureContext> factory)
    {
        _factory = factory ?? throw new ArgumentNullException(nameof(factory));
    }

    public async Task<int> AddNewMeasure(Entities.Measure newMeasure)
    {
        try
        {
            using var db = _factory.CreateDbContext();
            await db.Measures.AddAsync(newMeasure);
            await db.SaveChangesAsync();
            return newMeasure.Id;
        }
        catch(Exception ex)
        {
            throw new Exception("Server error when trying to read data from db",ex);
        }
    }
    public async Task UpdateMeasureStatus(int measureId, EStatus status)
    {
        using var db = _factory.CreateDbContext();
        Entities.Measure measure = await db.Measures.FirstOrDefaultAsync(m => m.Id == measureId);
        if(measure == null)
            throw new KeyNotFoundException(nameof(measure));
        db.Entry(measure).CurrentValues.SetValues(measure.Status=status);
    }

}

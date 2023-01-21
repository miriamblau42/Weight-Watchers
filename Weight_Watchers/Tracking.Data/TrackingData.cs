using Microsoft.EntityFrameworkCore;
using Tracking.Data.Entities;

namespace Tracking.Data;

public class TrackingData : ITrackingData
{
    private readonly IDbContextFactory<TrackingContext> _factory;

    public TrackingData(IDbContextFactory<TrackingContext> factory)
    {
        _factory = factory ?? throw new ArgumentNullException(nameof(factory));
    }

    public async Task<int> AddNewTrack(Track newTrack)
    {
        try
        {
            using var db = _factory.CreateDbContext();

            await db.Tracks.AddAsync(newTrack);
            await db.SaveChangesAsync();
            return newTrack.Id;

        }
        catch
        {
            throw new Exception("Server error when trying to read data from db");
        }
    }

    public async Task<List<Track>> GetTracksByCardId(int cardId)
    {
        try
        {
            using var db = _factory.CreateDbContext();
            return db.Tracks.Where(t => t.CardId == cardId).ToList();
        }
        catch
        {
            throw new Exception("Server error when trying to read data from db");
        }
    }

    public float GetPreviousWeight(int cardId)
    {
        using var db = _factory.CreateDbContext();
        if(db.Tracks.Count() == 0) return 0;
        Track? t = db.Tracks.OrderByDescending(t => t.Date).First();
        return t.Weight;
    }

    
}

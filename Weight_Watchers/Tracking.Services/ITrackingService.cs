using Tracking.Data.Entities;
using Tracking.Services.Models;

namespace Tracking.Services
{
    public interface ITrackingService
    {
        Task<int> AddNewTrack(TrackModel newTrack);
        Task<List<TrackModel>> GetTracksByCardId(int cardId);
    }
}
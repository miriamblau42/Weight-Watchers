using Tracking.Data.Entities;

namespace Tracking.Data
{
    public interface ITrackingData
    {
        Task<int> AddNewTrack(Track newTrack);
        Task<List<Track>> GetTracksByCardId(int cardId);

        float GetPreviousWeight(int cardId);
    }
}
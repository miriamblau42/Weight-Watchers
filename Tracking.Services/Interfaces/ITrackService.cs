using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tracking.Services.Models;

namespace Tracking.Services.Interfaces
{
    public interface ITrackService
    {
        Task<TackModel> Get(int cardId);
        Task<int> Post(TackModel trackModel);
        Task<List<TackModel>> GetTrackings(int cardID, int page, int records);
        public Task<int> GetNumOfTrackings(int cardID);
    }
}

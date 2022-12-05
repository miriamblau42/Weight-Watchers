using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracking.Data.Interfaces
{
    public interface ITrackDal
    {
        Task<Tracking.Data.Entities.Tracking> Get(int cardID);
        Task<int> Post(Tracking.Data.Entities.Tracking tracking);
        Task<List<Tracking.Data.Entities.Tracking>> GetTrackings(int id, int page, int records);
        Task<int> GetNumOfTrackings(int id);
    }
}

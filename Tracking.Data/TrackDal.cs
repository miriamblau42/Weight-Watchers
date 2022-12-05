using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tracking.Data.Entities;
using Tracking.Data.Interfaces;

namespace Tracking.Data
{
    public class TrackDal : ITrackDal
    {
        private TrackContext _Trackcontext;

        public TrackDal(TrackContext TrackContext)
        {
            _Trackcontext = TrackContext;
        }
        public async Task<Entities.Tracking> Get(int cardID)
        {
            // using var TrackContext = _Trackcontext.CreateDbContext();
            var Track = await _Trackcontext.Trackings.Where(t => t.CardID == cardID).OrderByDescending(tracking => tracking.Date).FirstOrDefaultAsync();
            return Track;
        }

        public async Task<int> Post(Entities.Tracking tracking)
        {
            // using var TrackContext = _Trackcontext.CreateDbContext();
            await _Trackcontext.Trackings.AddAsync(tracking);
            await _Trackcontext.SaveChangesAsync();
            return tracking.ID;
        }
        public async Task<List<Tracking.Data.Entities.Tracking>> GetTrackings(int cardID, int page, int records)
        {


            var position = page * records;

            var nextPage = from track in _Trackcontext.Trackings
                           where track.CardID == cardID
                           orderby track.Date
                           select new Tracking.Data.Entities.Tracking()
                           {
                               ID = track.ID,
                               CardID = track.CardID,
                               Weight = track.Weight,
                               Trend = track.Trend,
                               BMI = track.BMI,
                               Date = track.Date
                           };
            try
            {
                var operations = await nextPage.Skip(position)
                                .Take(records)
                                .ToListAsync();
                return operations;
            }
            catch
            {
                return null;
            }
        }
        public async Task<int> GetNumOfTrackings(int cardID)
        {
            try
            {
                return await _Trackcontext.Trackings.Where(op => op.CardID == cardID).CountAsync();
            }
            catch
            {
                return -1;
            }
        }
    }
}

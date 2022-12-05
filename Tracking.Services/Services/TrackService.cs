using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tracking.Data.Entities;
using Tracking.Data.Interfaces;
using Tracking.Services.Interfaces;
using Tracking.Services.Models;

namespace Tracking.Services.Services
{
    public class TrackService : ITrackService
    {
        private ITrackDal _trackDal;
        private IMapper _mapper;

        public TrackService(ITrackDal trackdal)
        {
            _trackDal = trackdal;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MapTrack>();
            });
            _mapper = config.CreateMapper();
        }
        public async Task<Trend> GetTrend(float weight, int cardID)
        {
            var card = await Get(cardID);
            float lastWeight = card.Weight;
            if (weight>lastWeight)
            {
                return Trend.Up;
            }
            if (weight<lastWeight)
            {
                return Trend.Down;
            }
            return Trend.Static;
        }
        public  async Task<TackModel> Get(int cardId)
        {
            Tracking.Data.Entities.Tracking tracking =await _trackDal.Get(cardId);
            TackModel model = _mapper.Map<TackModel>(tracking);
            return model;
        }

        public async Task<int> Post(TackModel trackModel)
        {
            Tracking.Data.Entities.Tracking newTrack = _mapper.Map<Tracking.Data.Entities.Tracking>(trackModel);
            newTrack.Trend=await GetTrend(trackModel.Weight, trackModel.CardID);
            newTrack.Date = DateTime.UtcNow;
            int id = _trackDal.Post(newTrack).Result;
            return id;
        }
        public async Task<List<TackModel>> GetTrackings(int cardID, int page, int records)
        {
            List<Tracking.Data.Entities.Tracking> operationsHistories = await _trackDal.GetTrackings(cardID, page, records);
            return _mapper.Map<List<Tracking.Data.Entities.Tracking>, List<TackModel>>(operationsHistories);
        }
        public Task<int> GetNumOfTrackings(int cardID)
        {
            return _trackDal.GetNumOfTrackings(cardID);
        }
    }
}

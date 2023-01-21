using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tracking.Data;
using Tracking.Data.Entities;
using Tracking.Services.Models;

namespace Tracking.Services;

public class TrackingService : ITrackingService
{
    private readonly ITrackingData _trackingData;
    private readonly IMapper _mapper;

    public TrackingService(ITrackingData trackingData , IMapper mapper)
    {
        _trackingData = trackingData;
        _mapper = mapper;
    }
    public Task<int> AddNewTrack(TrackModel newTrack)
    {
        float previousWeight = _trackingData.GetPreviousWeight(newTrack.CardId);
        if(previousWeight == 0)
            previousWeight = newTrack.Weight;
        newTrack.Trend = (newTrack.Weight - previousWeight) switch
        {
            > 0 => ETrend.Increase,
            < 0 => ETrend.Decrease,
            _ => ETrend.Static
        };   
        return _trackingData.AddNewTrack(_mapper.Map<Track>(newTrack));
    }

    public async Task<List<TrackModel>> GetTracksByCardId(int cardId)
    {
        return _mapper.Map<List<TrackModel>>(await _trackingData.GetTracksByCardId(cardId));

    }
}

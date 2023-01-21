using AutoMapper;
using Tracking.Data.Entities;
using Tracking.Services.Models;
using Tracking.WebApi.DTO;

namespace Tracking.WebApi;

public class AutoMapper : Profile
{
    public AutoMapper()
    {
        CreateMap<Track,TrackModel>().ReverseMap();
        CreateMap<TrackModel, PostTrackDTO>().ReverseMap();
        CreateMap<TrackModel, GetTrackDTO>().ReverseMap();
    }

}

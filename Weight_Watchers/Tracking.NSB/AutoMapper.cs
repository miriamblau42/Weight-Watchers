using AutoMapper;
using Tracking.Data.Entities;
using Tracking.Services.Models;

namespace Tracking;

public class AutoMapper : Profile
{
    public AutoMapper()
    {
        CreateMap<Track,TrackModel>().ReverseMap();
    }

}

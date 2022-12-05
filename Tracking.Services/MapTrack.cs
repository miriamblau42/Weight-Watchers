using AutoMapper;
using Tracking.Services.Models;

namespace Tracking.Services
{
    internal class MapTrack:Profile
    {
        public MapTrack()
        {
            CreateMap<Tracking.Data.Entities.Tracking, TackModel>().ReverseMap();
        }
    }
}

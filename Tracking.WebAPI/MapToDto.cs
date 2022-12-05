using AutoMapper;
using Tracking.Services.Models;
using Tracking.WebAPI.DTOs;

namespace Tracking.WebAPI
{
    public class MapToDto:Profile
    {
        public MapToDto()
        {
            CreateMap<TackModel, TrackDto>();
        }
    }
}

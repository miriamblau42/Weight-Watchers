using AutoMapper;
using Subscriber.Data.Entities;
using Subscriber.Services.Models;
using Subscriber.WebAPI.DTO;

namespace Subscriber.WebAPI
{
    
    public class MapCard : Profile
    {

        public MapCard()
        {
            CreateMap<CardModel, CardInfoDTO>();
        }
    }
}

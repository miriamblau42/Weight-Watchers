using AutoMapper;
using Subscriber.Data.Entities;
using Subscriber.Services;
using Subscriber.Services.Models;
using Subscriber.WebApi.DTO;

namespace Subscriber.WebApi
{
    public class AutoMapper: Profile
    {
        public AutoMapper()
        {
            CreateMap< Data.Entities.Subscriber ,SubscriberModel> ()
               .ReverseMap();
            CreateMap< SubscriberModel, PostSubscriberDTO>()
               .ReverseMap();
            CreateMap<Card, CardModel>()
               .ReverseMap();
            CreateMap<CardModel, GetCardDTO>()
                .ForMember(des => des.FirstName, opts => opts
                        .MapFrom(src => src.Subscriber.FirstName))
                .ForMember(des => des.LastName, opts => opts
                        .MapFrom(src => src.Subscriber.LastName))
                .ReverseMap();
           
        }
    }
}

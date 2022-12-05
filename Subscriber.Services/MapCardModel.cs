using AutoMapper;
using Subscriber.Data.Entities;
using Subscriber.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Subscriber.Services
{
    internal class MapCardModel : Profile
    {
        public MapCardModel()
        {
            CreateMap<Card, CardModel>()
           .ForMember(d => d.FirstName, opt => opt.MapFrom(s => s.Subscriber.FirstName))
           .ForMember(d => d.LastName, opt => opt.MapFrom(s => s.Subscriber.LastName))
           .ReverseMap();
            CreateMap<SubscriberModel, Subscriber.Data.Entities.Subscriber>().ReverseMap();
        }
    }
}


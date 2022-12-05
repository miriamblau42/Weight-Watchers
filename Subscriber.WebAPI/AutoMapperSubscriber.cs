using AutoMapper;
using Subscriber.WebAPI.DTO;
using Subscriber.Services.Models;

namespace Subscriber.WebAPI
{
    public class AutoMapperSubscriber: Profile
    {
        public AutoMapperSubscriber()
        {
           CreateMap<RegisterSubscriberDto,SubscriberModel>().ReverseMap();
        }
    }
}

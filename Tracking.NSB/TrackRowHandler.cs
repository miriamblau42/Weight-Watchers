using AutoMapper;
using Measure.Services;
using NServiceBus;
using NServiceBus.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tracking.Data.Interfaces;
using Tracking.Messages;
using Tracking.Services.Interfaces;

namespace Tracking.NSB
{
    public class TrackRowHandler : IHandleMessages<TrackRowAdd>,IHandleMessages<TrackingPosted>
    {   
        static ILog log = LogManager.GetLogger<TrackRowHandler>();
        private IMapper _mapper;
        ITrackService _service;
        public TrackRowHandler(ITrackService track)
        {
            _service = track;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MapMeasureToEntity>();
            });
            _mapper = config.CreateMapper();
        }
 

        public async Task Handle(TrackRowAdd message, IMessageHandlerContext context)
        {
            log.Info($"card [{message.CardID}] - Successfully added.");
            Tracking.Services.Models.TackModel tr = new Services.Models.TackModel
            {
                CardID = message.CardID,
                BMI = message.BMI,
                Weight = message.Weight,
                Trend = Data.Entities.Trend.Static,
                Date = DateTime.UtcNow
            };
            int id =await _service.Post(tr);
            log.Info($"added[{id}]");
            TrackingPosted tracked = new TrackingPosted { CardID =message.CardID, MeasureID=message.MeasureID, Success=true};

            await context.Publish(tracked);
            

        }

        public Task Handle(TrackingPosted message, IMessageHandlerContext context)
        {
            log.Info("card[{ message.CardID}] -Successfully added");
            return Task.CompletedTask;
        }
    }
}

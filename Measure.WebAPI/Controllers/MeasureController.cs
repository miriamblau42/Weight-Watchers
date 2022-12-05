using AutoMapper;
using Measure.Messages;
using Measure.Services.Interfaces;
using Measure.Services.Models;
using Measure.WebAPI.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NServiceBus;

namespace Measure.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeasureController : ControllerBase
    {
        private IMapper _mapper;
        private IMeasureService _measureService;
        public IMessageSession _messageSession;

        public MeasureController(IMeasureService measureService, IMapper mapper, IMessageSession messageSession)
        {
            _mapper = mapper;
            _measureService = measureService;
            _messageSession = messageSession;
        }
        [HttpPost]
        public async Task<ActionResult> PostMeasure(MeasurePostDTO postMeasure)
        {
            MeasureModel measure = _mapper.Map<MeasureModel>(postMeasure);
            int ID = await _measureService.AddMeasure(measure);
            MeasureInserted measured = new MeasureInserted
            {
                MesureID = ID,
                CardID = postMeasure.CardID,
                Weight = postMeasure.Weight,
            };
            //publish message...
            await _messageSession.Publish(measured);
            Console.WriteLine($"Measure added id={ID}");
            return ID == -1 ? Unauthorized() : Ok();
        }
    }
}

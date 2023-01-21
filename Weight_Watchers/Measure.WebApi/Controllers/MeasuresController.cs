using AutoMapper;
using Measure.Messages.Events;
using Measure.Services;
using Measure.Services.Models;
using Measure.WebApi.DTO;
using Microsoft.AspNetCore.Mvc;
using NServiceBus;

namespace Measure.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeasuresController : ControllerBase
    {
        private readonly IMeasureService _measureService;
        private readonly IMapper _mapper;
        private readonly IMessageSession _messageSession;

        public MeasuresController(IMeasureService measureService, IMapper mapper, IMessageSession messageSession)
        {
            _measureService = measureService;
            _mapper = mapper;
            _messageSession = messageSession;
        }

        [HttpPost]
        public async Task<ActionResult> AddNewMeasure([FromBody] PostMeasureDTO newMeasure)
        {
            int id = await _measureService.AddNewMeasure(_mapper.Map<MeasureModel>(newMeasure));
            MeasureAdded measureAdded = new()
            {
                MeasureId = id,
                CardId = newMeasure.CardId,
                Weight = newMeasure.Weight,
                Date = DateTime.UtcNow,
                Comment = newMeasure.Comment
            };
            await _messageSession.Publish(measureAdded);
            Console.WriteLine($"Measure added. Id = {id}");
            return Ok();
        }
    }
}

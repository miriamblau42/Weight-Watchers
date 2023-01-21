using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tracking.Services;
using Tracking.Services.Models;
using Tracking.WebApi.DTO;

namespace Tracking.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrackingController : ControllerBase
    {
        private readonly ITrackingService _trackingService;
        private readonly IMapper _mapper;

        public TrackingController(ITrackingService trackingService, IMapper mapper)
        {
            _trackingService = trackingService;
            _mapper = mapper;
        }
        [HttpGet("{cardId}")]
        public async Task<ActionResult> GetTracksByCardId(int cardId)
        {
            List<TrackModel> trackModels = await _trackingService.GetTracksByCardId(cardId);
            if (trackModels == null)
                throw new ArgumentException("No such subscriber.");
            List<GetTrackDTO> tracks = _mapper.Map<List<GetTrackDTO>>(trackModels);
            return Ok(tracks);
        }

    }
}

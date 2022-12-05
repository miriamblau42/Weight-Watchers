using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tracking.Services.Interfaces;
using Tracking.Services.Models;
using Tracking.WebAPI.DTOs;

namespace Tracking.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrackController : ControllerBase
    {
        private ITrackService _trackService;
        private IMapper _mapper;

        public TrackController(ITrackService trackService)
        {
            _trackService = trackService;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MapToDto>();
            });
            _mapper = config.CreateMapper();
        }
        [HttpGet("{cardID}")]
        public async Task<ActionResult<TrackDto>> GetLastCard(int cardID)
        {
            TackModel tackModel = await _trackService.Get(cardID);
            if (tackModel is null)
            {
                return NotFound();
            }
            TrackDto track = _mapper.Map<TrackDto>(tackModel);
            return track;
        }

}
}

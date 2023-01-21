using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Subscriber.Services;
using Subscriber.Services.Models;
using Subscriber.WebApi.DTO;


namespace Subscriber.WebApi.Controllers
{
    [Route("api/")]
    [ApiController]
    public class SubscriberController : ControllerBase
    {
        private readonly ISubscriberService _subscriberService;
        private readonly IMapper _mapper;

        public SubscriberController(ISubscriberService subscriberService, IMapper mapper)
        {
            _subscriberService = subscriberService;
            _mapper = mapper;
        }
       

        [HttpGet("card/{id}")]
        public async Task<ActionResult> GetCardById(int id)
        {
            var cardDTO = _mapper.Map<GetCardDTO>(await _subscriberService.getCardById(id));
            if (cardDTO == null)
                return StatusCode(404, "not found");
            return Ok(cardDTO);
        }

        [HttpPost("subscriber/")]
        public async Task<ActionResult> PostSubscriber([FromBody] PostSubscriberDTO newSubscriber)
        {
           bool success = await _subscriberService.AddNewSubscriber(_mapper.Map<SubscriberModel>(newSubscriber),newSubscriber.Height);
            if (!success)
                throw new ArgumentException("Invalid email address.");
            return Ok();
        }

        [HttpPost("Login/")]
        public async Task<ActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            int cardId = await _subscriberService.Login(loginDTO.Email,loginDTO.Password);
            if(cardId == -1)
                throw new ArgumentException("No such subscriber.");
            return Ok(cardId);

        }

        

    }
}

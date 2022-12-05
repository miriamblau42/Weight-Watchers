using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Subscriber.Services.Interfaces;
using Subscriber.Services.Models;
using Subscriber.WebAPI.DTO;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Subscriber.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriberController : ControllerBase
    {
        private ISubscriberService _subscriberService;
        private IMapper _mapper;
       

        public SubscriberController(ISubscriberService subscriberService,ICardService cardService)
        {
            _subscriberService = subscriberService;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapperSubscriber>();
            });
            _mapper = config.CreateMapper();
           
        }

        // POST api/<SubscriberController>
        [HttpPost]
        public async Task<ActionResult<bool>> Register([FromBody] RegisterSubscriberDto subscriberDto)
        {
            if (!_subscriberService.IsValid(subscriberDto.Password))
            {
                return BadRequest();
            }
            SubscriberModel mySubscriber = _mapper.Map<SubscriberModel>(subscriberDto);
            float height = subscriberDto.Height;
            bool success = await _subscriberService.Register(mySubscriber, height);
            //return if it subscribed or not...          
            return Ok(success);
        }
        [HttpPost("/login")]
        public async Task<ActionResult<int>> Login([FromBody] LoginDto personLogin)
        {
            int cardID = await _subscriberService.GetMyCard(personLogin.Email, personLogin.Password);
            if(cardID == -1)
            {
                return Unauthorized();
            }
            return Ok(cardID);
  
            
        }

    }
}

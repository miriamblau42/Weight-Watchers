using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Subscriber.Services.Interfaces;
using Subscriber.Services.Models;
using Subscriber.WebAPI.DTO;

namespace Subscriber.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardController : ControllerBase
    {
        private IMapper _mapper;
        private ICardService _cardService;

        public CardController(ICardService cardService)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MapCard>();
            });
            _mapper = config.CreateMapper();
            _cardService = cardService;
        }
        // GET api/<CardController>/5
        [HttpGet("{ID}")]
        public async Task<ActionResult<CardInfoDTO>> Get(int ID)
        {
            CardModel mycard =await _cardService.GetCardInfoByID(ID);
            if (mycard is null)
            {
                return BadRequest();
            }
/*            SubscriberModel mySubscriber = await _cardService.GetSubscriberModelByID(mycard.subscriberID);
*/            CardInfoDTO card = _mapper.Map<CardInfoDTO>(mycard);
            return Ok(card);
        }
      
    }

}

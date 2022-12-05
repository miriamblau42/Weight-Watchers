using Microsoft.AspNetCore.Mvc;

namespace Subscriber.WebAPI.Controllers
{
    public class EmailVerficationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

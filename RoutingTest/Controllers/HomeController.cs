using Microsoft.AspNetCore.Mvc;

namespace RoutingTest.Controllers
{
    public class HomeController : Controller
    {
        //[Route("/")]
        public IActionResult Index()
        {
            return View();
        }

        [ActionName("hello")]
        [HttpGet("hello", Name = "Hello")]
        public IActionResult AjaxCall()
        {
            return Ok("ok");
        }
    }
}

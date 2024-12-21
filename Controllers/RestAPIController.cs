using Microsoft.AspNetCore.Mvc;

namespace WebOdev.Controllers
{
    [ApiController]
    public class RestAPIController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

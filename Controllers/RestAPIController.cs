using Microsoft.AspNetCore.Mvc;

namespace WebOdev.Controllers
{
    [ApiController]
    public class RestAPIController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RestAPIController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("/api/hizmetler/{id}")]
        public IActionResult Hizmetler(string id)
        {
            var islem = _context.Islemler.FirstOrDefault(i => i.Id == Convert.ToInt32(id));
            if (islem == null)
            {
                return Content("İşlem Bulunamadı!");
            }

            return new JsonResult(islem);
        }
    }
}

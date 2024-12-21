using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebOdev.Models;

namespace WebOdev.Controllers
{
    public class AnasayfaController : Controller
    {
        private readonly ILogger<AnasayfaController> _logger;
        private readonly ApplicationDbContext _context;

        public AnasayfaController(ILogger<AnasayfaController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Hizmetlerimiz()
        {
            var islemler = _context.Islemler.ToList();
            return View(islemler);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

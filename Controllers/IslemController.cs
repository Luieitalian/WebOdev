using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebOdev.Models;

namespace WebOdev.Controllers
{
    public class IslemController : Controller
    {
        private readonly ApplicationDbContext _context;

        public IslemController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Admin, Calisan")]
        public IActionResult Index()
        {
            var islemler = _context.Islemler.ToList();
            return View(islemler);
        }

        [Authorize(Roles = "Admin, Calisan")]
        public IActionResult IslemEkle() { return View(); }

        [HttpPost, ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Calisan")]
        public IActionResult Create(IslemModel islem)
        {
            if (ModelState.IsValid)
            {
                _context.Islemler.Add(islem);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            var errors = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();

            foreach (var error in errors)
            {
                Console.WriteLine(error);
            }

            return View(islem);
        }
    }
}

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

        public IActionResult Index()
        {
            var islemler = _context.Islemler.ToList();
            return View(islemler);
        }

        public IActionResult IslemEkle() { return View(); }

        [HttpPost, ValidateAntiForgeryToken]
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

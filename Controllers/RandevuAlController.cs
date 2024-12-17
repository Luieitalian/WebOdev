using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;


namespace WebOdev.Controllers
{
    public class RandevuAlController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RandevuAlController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
        }

        public IActionResult Adim1()
        {
            var islemler = _context.Islemler.ToList();
            return View(islemler);
        }

        // POST: Adım 1
        [HttpPost]
        public IActionResult Adim1(int islemId)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Adim1");
            }

            var islem = _context.Islemler.Find(islemId);
            if (islem == null)
            {
                ModelState.AddModelError("", "Herhangi bir işlem bulunamadı!");
                return RedirectToAction("Adim2");
            }

            HttpContext.Session.SetString("IslemId", islem.Id.ToString());

            // Adım 2'ye yönlendir
            return RedirectToAction("Adim2");
        }

        public IActionResult Adim2()
        {
            // TempData'dan işlem verisini al
            var islemId = HttpContext.Session.GetString("IslemId");

            if (islemId == null)
            {
                return RedirectToAction("Adim1");
            }
            var calisanlar = _context.Calisanlar.Include(c => c.Kullanici).ToList();

            return View(calisanlar);
        }

        // POST: Adım 2
        [HttpPost]
        public IActionResult Adim2(string calisanId)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Adim2");
            }

            var calisan = _context.Calisanlar.Find(calisanId);
            if (calisan == null)
            {
                ModelState.AddModelError("", "Herhangi bir çalışan bulunamadı!");
                return RedirectToAction("Adim2");
            }

            HttpContext.Session.SetString("CalisanId", calisan.KullaniciId);

            // Adım 3'e yönlendir
            return RedirectToAction("Adim3");
        }

        public IActionResult Adim3()
        {
            var islemid = HttpContext.Session.GetString("IslemId");
            var calisanid = HttpContext.Session.GetString("CalisanId");
            
            Console.WriteLine("İşlem id" + islemid);
            Console.WriteLine("calisan id" + calisanid);
            // TO BE CONTINUED
            return View();
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebOdev.Models; // Model sınıfları
using System.Linq;

namespace WebOdev.Controllers
{
    public class RandevuController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RandevuController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Randevuları Listele
        public IActionResult Index()
        {
            var randevular = _context.Randevular
                .Include(r => r.Calisan)
                .ThenInclude(c => c.Kullanici)
                .Include(r => r.Islem)
                .Include(r => r.Musteri)
                .ToList();

            return View(randevular);
        }

        // Randevu Ekleme Sayfasını Göster
        [HttpGet]
        public IActionResult Create()
        {
            LoadViewData();
            return View("RandevuEkle.cshtml");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(RandevuModel model)
        {
            if (ModelState.IsValid)
            {
                // Model geçerliyse, veritabanına kaydediyoruz
                _context.Randevular.Add(model);
                _context.SaveChanges();

                // Yeni randevu başarıyla eklendiyse, kullanıcıyı "RandevuEkle.cshtml" sayfasına yönlendiriyoruz
                return View("RandevuEkle", model); // Aynı sayfayı geri gönderiyoruz (RandevuEkle.cshtml)
            }

            // Model geçersizse, ViewData'yı tekrar yükleyip formu aynı sayfada tekrar gösteriyoruz
            LoadViewData(); // Form geçerli değilse ViewData'yı tekrar yükle
            return View("RandevuEkle", model); // Aynı sayfayı geri gönderiyoruz (RandevuEkle.cshtml)
        }




        // Randevu Detay Sayfası
        public IActionResult Details(int id)
        {
            var randevu = _context.Randevular
                .Include(r => r.Calisan)
                .ThenInclude(c => c.Kullanici)
                .Include(r => r.Islem)
                .Include(r => r.Musteri)
                .FirstOrDefault(r => r.Id == id);

            if (randevu == null)
            {
                return NotFound();
            }

            return View(randevu);
        }

        // Randevu Silme
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var randevu = _context.Randevular.Find(id);
            if (randevu == null)
            {
                return NotFound();
            }

            _context.Randevular.Remove(randevu);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        // Dinamik Verileri ViewBag'e Yükler
        private void LoadViewData()
        {
            // Çalışanlar için
            ViewBag.Calisanlar = _context.Calisanlar
                .Include(c => c.Kullanici) // Kullanıcı bilgilerini dahil et
                .Select(c => new { c.KullaniciId, AdSoyad = c.Kullanici.Isim + " " + c.Kullanici.Soyisim })
                .ToList();

            // İşlemler için
            ViewBag.Islemler = _context.Islemler
                .Select(i => new { i.Id, i.Baslik })
                .ToList();

            // Müşteriler için
            ViewBag.Musteriler = _context.Musteriler
                .Include(m => m.Kullanici) // Kullanıcı bilgilerini dahil et
                .Select(m => new { m.KullaniciId, AdSoyad = m.Kullanici.Isim + " " + m.Kullanici.Soyisim })
                .ToList();
        }

    }
}

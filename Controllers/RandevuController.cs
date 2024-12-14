using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebOdev.Models; // Model sınıfları
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using AutoMapper;
using System;

namespace WebOdev.Controllers
{
    public class RandevuController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public RandevuController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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

        // GET
        public IActionResult RandevuEkle()
        {{}
            var randevu = new RandevuModelDto();
            var calisanlar = _context.Calisanlar;
            var musteriler = _context.Musteriler;
            var islemler = _context.Islemler;
            // Çalışanları, müşterileri ve işlemleri ViewBag'e aktarma
            randevu.Prepare.Calisan = calisanlar.Select(x => new SelectListItem { Text = $"{x.Kullanici.Isim} {x.Kullanici.Soyisim}", Value = $"{x.Kullanici.Isim}{x.Kullanici.Soyisim}" }).ToList();

            randevu.Prepare.Musteriler = musteriler.Select(x => new SelectListItem { Text = $"{x.Kullanici.Isim} {x.Kullanici.Soyisim}", Value = $"{x.Kullanici.Isim}{x.Kullanici.Soyisim}" }).ToList();
            
            randevu.Prepare.Islemler = islemler.Select(x => new SelectListItem { Text = x.Aciklama, Value = x.Id.ToString() }).ToList();

            // Create View'ini döndürme
            return View(randevu);
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RandevuEkle(RandevuModelDto model)
        {
             int randevuId = 1;
             // Model geçerli ise, veritabanına kaydetme
             var mapResult = _mapper.Map<RandevuModelDto, RandevuModel>(model);
            mapResult.IstemTarihi = DateTime.UtcNow;

            mapResult.BaslangicTarihi = mapResult.BaslangicTarihi.Value.ToUniversalTime();
            //mapResult.BaslangicTarihi = DateTime.UtcNow;
            mapResult.BitisTarihi = mapResult.BaslangicTarihi.Value.ToUniversalTime();


            var randevu = _context.Randevular;
            if (randevu is not null && randevu.Any())
                randevuId = randevu.Max(x => x.Id) + 1;
             //Max(x => x.Id);

             mapResult.Id = randevuId;
             _context.Randevular.Add(mapResult);
             _context.SaveChanges();

             // Başarılı işlem sonrası kullanıcıyı yönlendirme
             return RedirectToAction("Index"); // Örneğin liste sayfasına yönlendirme
          
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

            return View(islem);
        }
    }
}

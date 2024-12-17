using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebOdev.Models;

namespace WebOdev.Controllers
{
    public class CalisanController : Controller
    {
        private readonly ILogger<CalisanController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<KullaniciModel> _userManager;

        public CalisanController(ApplicationDbContext context, UserManager<KullaniciModel> userManager, ILogger<CalisanController> logger)
        {
            _logger = logger;
            _userManager = userManager;
            _context = context;
        }

        // Admin rolünde olanlar yalnızca çalışanları görebilir
        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            // Çalışanları listelerken ilişkili kullanıcı bilgilerini de getiriyoruz
            var calisanlar = _context.Calisanlar.Include(c => c.Kullanici).ToList();
            return View(calisanlar);
        }

        [Authorize(Roles = "Calisan")]
        public IActionResult CalisanPaneli()
        {
            // Çalışanları listelerken ilişkili kullanıcı bilgilerini de getiriyoruz
            var randevular = _context.Randevular.Include(c => c.Islem).Include(c => c.Musteri).ToList();
            return View(randevular);
        }

        // Admin rolünde olanlar yalnızca çalışan ekleyebilir
        [Authorize(Roles = "Admin, Calisan")]
        public IActionResult IslemEkle()
        {
            var islemler = _context.Islemler.ToList();
            return View(islemler);
        }

        [Authorize(Roles = "Admin, Calisan")]
        [HttpPost]
        public IActionResult IslemEkle(int id, int yetkinlik, string not)
        {
            var query =  from calisan in _context.Calisanlar
                         join kullanici in _userManager.Users
                         on calisan.KullaniciId equals kullanici.Id
                         select new
                         {
                             ID = kullanici.Id
                         };


            var newcalisanislem = new CalisanIslemModel
            {
                CalisanId = query.Single().ID,
                IslemId = id,
                Yetkinlik = yetkinlik,
                Not = not,
            };

            _context.CalisanIslemleri.Add(newcalisanislem);
            _context.SaveChanges();

            return RedirectToAction("CalisanPaneli");
        }

        // Admin rolünde olanlar yalnızca çalışan ekleyebilir
        [Authorize(Roles = "Admin")]
        public IActionResult CalisanEkle()
        {
            return View();
        }

        // POST action'ı - Çalışan oluşturma
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(KullaniciEkleViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // Eğer model geçerli değilse, formu aynı sayfada tekrar göster
                return View("CalisanEkle", model);
            }

            // Yeni kullanıcı oluşturma
            var newuser = new KullaniciModel
            {
                UserName = model.Email,
                Email = model.Email,
                PhoneNumber = model.Telefon,
                Cinsiyet = model.Cinsiyet,
                DogumTarihi = model.DogumTarihi,
                Isim = model.Isim,
                Soyisim = model.Soyisim,
            };

            // Kullanıcıyı oluştur
            var result = await _userManager.CreateAsync(newuser, model.Sifre);
            if (result.Succeeded)
            {
                // Yeni kullanıcıya 'Calisan' rolünü ekle
                await _userManager.AddToRoleAsync(newuser, "Calisan");

                // Yeni çalışan ekle
                var calisan = new CalisanModel
                {
                    Kullanici = newuser,
                };

                await _context.Calisanlar.AddAsync(calisan);
                await _context.SaveChangesAsync();

                // Başarılıysa çalışanlar listesine yönlendir
                return RedirectToAction("Index");
            }
            else
            {
                // Hata mesajlarını ModelState'e ekle
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // Eğer bir hata olduysa, formu tekrar yükle
            return View("CalisanEkle", model);
        }

        // Çalışan silme işlemi
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id)
        {
            // ID'ye sahip çalışan ve ilişkili kullanıcıyı bul
            var calisan = await _context.Calisanlar
                                       .Include(c => c.Kullanici)
                                       .FirstOrDefaultAsync(c => c.KullaniciId == id);

            if (calisan == null)
            {
                // Eğer çalışan bulunamazsa, hata sayfasına yönlendir
                return NotFound();
            }

            // Kullanıcıyı sil
            var result = await _userManager.DeleteAsync(calisan.Kullanici);
            if (!result.Succeeded)
            {
                // Eğer silme işlemi başarısız olursa, hataları ModelState'e ekleyin
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return RedirectToAction("Index");
            }

            // Kullanıcı silindikten sonra, çalışanın veritabanından silinmesi
            _context.Calisanlar.Remove(calisan);

            try
            {
                // Değişiklikleri veritabanına kaydet
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                // Eğer bir concurrency hatası alırsanız, burayı yakalayabilirsiniz
                ModelState.AddModelError(string.Empty, "Veri güncellenirken bir hata oluştu.");
                _logger.LogError(ex, "Concurrency exception occurred while saving changes.");
                return RedirectToAction("Index");
            }

            // Başarılıysa, çalışanlar listesine yönlendir
            return RedirectToAction("Index");
        }

        [HttpGet("Calisan/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CalisanGuncelle(string id)
        {
            var calisan = await _context.Calisanlar
                                         .Include(c => c.Kullanici)
                                         .FirstOrDefaultAsync(c => c.KullaniciId == id);
            if (calisan == null) return NotFound();

            ViewBag.Id = calisan.KullaniciId;
            ViewBag.Isim = calisan.Kullanici.Isim;
            ViewBag.Soyisim = calisan.Kullanici.Soyisim;
            ViewBag.DogumTarihi = calisan.Kullanici.DogumTarihi;
            ViewBag.Email = calisan.Kullanici.Email;
            ViewBag.Telefon = calisan.Kullanici.PhoneNumber;
            ViewBag.Cinsiyet = calisan.Kullanici.Cinsiyet;

            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(KullaniciEkleViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();

                foreach (var error in errors)
                {
                    Console.WriteLine(error);
                }
                return Content("model invalid");
            }

            var calisan = await _context.Calisanlar
                .Include(c => c.Kullanici)
                .FirstOrDefaultAsync(c => c.KullaniciId == model.Id);

            if (calisan == null)
            {
                return Content("calisan not found");
            }

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
                if(calisan.Kullanici.Email != user.Email)
                {
                    return Content("ayni emaile sahip zaten bir kullanıcı var!");
                }
            }

            // Güncellenmiş verilerle kullanıcıyı güncelle
            calisan.Kullanici.Isim = model.Isim;
            calisan.Kullanici.Soyisim = model.Soyisim;
            calisan.Kullanici.Email = model.Email;
            calisan.Kullanici.PhoneNumber = model.Telefon;
            calisan.Kullanici.DogumTarihi = model.DogumTarihi;
            calisan.Kullanici.Cinsiyet = model.Cinsiyet;

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
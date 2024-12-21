using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
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
            var userID = _userManager.GetUserId(User);

            var query = from r in _context.Randevular
                        join m in _context.Musteriler.Include(m => m.Kullanici)
                        on r.MusteriId equals m.KullaniciId
                        join c in _context.Calisanlar.Include(c => c.Kullanici)
                        on userID equals c.KullaniciId
                        select new RandevuModel
                        {
                            Calisan = c,
                            Musteri = m,
                            Islem = r.Islem,
                            BaslangicTarihi = r.BaslangicTarihi,
                            BitisTarihi = r.BitisTarihi,
                            IstemTarihi = r.IstemTarihi,
                            Durum = r.Durum,
                            Id = r.Id
                        };

            var randevular = query.ToList();

            return View(randevular);
        }

        // Calisan rolünde olanlar yalnızca işlem ekleyebilir
        [Authorize(Roles = "Calisan")]
        public async Task<IActionResult> IslemEkle()
        {
            var user = await _userManager.GetUserAsync(User);

            var yapamadigiIslemler = _context.Islemler
                .Except(
                    _context.CalisanIslemleri
                        .Where(ci => ci.CalisanId == user!.Id)
                        .Select(ci => ci.Islem)
                )
                .ToList();

            return View(yapamadigiIslemler);
        }

        [HttpPost]
        [Authorize(Roles = "Calisan")]
        public IActionResult IslemEkle(int id, int yetkinlik, string not)
        {
            var islem = _context.Islemler.FirstOrDefault(i => i.Id == id);
            if (islem == null)
            {
                TempData["Message"] = "İşlem Bulunamadı!";
                return RedirectToAction("CalisanPaneli");
            }

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

            TempData["Message"] = "İşlem Başarıyla Eklendi!";
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
                TempData["Message"] = "Çalışan Başarıyla Eklendi!";
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
                TempData["Message"] = "Çalışan Bulunamadı!";
                return RedirectToAction("Index");
            }

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
                TempData["Message"] = "Çalışan Silinemedi!";
                return RedirectToAction("Index");
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
                TempData["Message"] = "Çalışan(Kullanıcı) Silinemedi!";
                return RedirectToAction("Index");
            } 

            TempData["Message"] = "Çalışan Başarıyla Silindi!";
            return RedirectToAction("Index");
        }

        [HttpGet("Calisan/Guncelle/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CalisanGuncelle(string id)
        {
            var calisan = await _context.Calisanlar
                                         .Include(c => c.Kullanici)
                                         .FirstOrDefaultAsync(c => c.KullaniciId == id);
            if (calisan == null) 
            {
                TempData["Message"] = "Çalışan Bulunamadı! Lütfen Daha Sonra Tekrar Deneyiniz.";
                return RedirectToAction("Index");
            }

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
                TempData["Message"] = "Çalışan Bulunamadı! Lütfen Daha Sonra Tekrar Deneyiniz.";
                return RedirectToAction("CalisanGuncelle");
            }

            var calisan = await _context.Calisanlar
                .Include(c => c.Kullanici)
                .FirstOrDefaultAsync(c => c.KullaniciId == model.Id);

            if (calisan == null)
            {
                TempData["Message"] = "Çalışan Bulunamadı!";
                return RedirectToAction("CalisanGuncelle");
            }

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
                if(calisan.Kullanici.Email != user.Email)
                {
                    TempData["Message"] = "Email Kullanımda!";
                    return RedirectToAction("CalisanGuncelle");
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
            
            TempData["Message"] = "Güncelleme Başarılı!";
            return RedirectToAction("Index");
        }
    }
}
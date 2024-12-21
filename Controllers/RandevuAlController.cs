using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using System;
using Microsoft.EntityFrameworkCore;
using WebOdev.Models;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace WebOdev.Controllers
{
    public class RandevuAlController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<KullaniciModel> _userManager;

        public RandevuAlController(ApplicationDbContext context, UserManager<KullaniciModel> userManager)
        {
            _context = context;
            _userManager = userManager;
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
            var islemId = HttpContext.Session.GetString("IslemId");

            if (islemId == null)
            {
                TempData["Message"] = "İşlem Bulunamadı!";
                return RedirectToAction("Adim1");
            }

            var query = from calisanislem in _context.CalisanIslemleri
                        where calisanislem.IslemId == Convert.ToInt32(islemId) 
                        select new
                        {
                            calisanislem.Calisan,
                            calisanislem.Calisan.Kullanici,
                            calisanislem.CalisanId
                        };

            var calisanlar = query
                .Select(c => new CalisanModel
                {
                    KullaniciId = c.CalisanId,
                    Kullanici = c.Kullanici
                })
                .ToList();

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
                TempData["Message"] = "Çalışan Bulunamadı!";
                return RedirectToAction("Adim2");
            }

            HttpContext.Session.SetString("CalisanId", calisan.KullaniciId);

            return RedirectToAction("Adim3");
        }

        public IActionResult Adim3()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Adim3(DateTime tarih)
        {
            if (!ModelState.IsValid) {
                TempData["Message"] = "Model State Geçersiz!";
                return RedirectToAction("Adim3");
            }
            var islemid = HttpContext.Session.GetString("IslemId");
            var calisanid = HttpContext.Session.GetString("CalisanId");

            if(islemid == null || calisanid == null)
            {
                Console.WriteLine("İşlem id " + islemid);
                Console.WriteLine("calisan id " + calisanid);
                TempData["Message"] = "İşlem veya Çalışan Geçersiz!";
                return RedirectToAction("Adim3");
            }

            var islem = _context.Islemler.Find(Convert.ToInt32(islemid));
            if (islem == null)
            {
                TempData["Message"] = "İşlem Bulunamadı!";
                return RedirectToAction("Adim2");
            }

            var calisan = _context.Calisanlar.Include(c => c.Kullanici).FirstOrDefault(c => c.KullaniciId == calisanid);
            if (calisan == null)
            {
                TempData["Message"] = "Çalışan Bulunamadı!";
                return RedirectToAction("Adim2");
            }

            var musteri = _context.Musteriler.Include(m => m.Kullanici).FirstOrDefault(m => m.KullaniciId == _userManager.GetUserId(User));
            if (musteri == null)
            {
                TempData["Message"] = "Müşteri Bulunamadı!";
                return RedirectToAction("Adim2");
            }

            DateTime randevuBaslangicTarihi = tarih;
            DateTime randevuBitisTarihi = tarih + islem.Uzunluk;

            var query = from or in _context.OnayliRandevular
                        join randevu in _context.Randevular
                        on or.RandevuId equals randevu.Id
                        join c in _context.Calisanlar
                        on randevu.CalisanId equals c.KullaniciId
                        select new
                        {
                            randevu.BaslangicTarihi,
                            randevu.BitisTarihi
                        };

            var list = query.ToList();
            if (list.Count > 0) 
            {
                foreach (var item in list)
                {
                    if((item.BaslangicTarihi > randevuBaslangicTarihi && randevuBitisTarihi > item.BaslangicTarihi)
                    || (item.BaslangicTarihi < randevuBaslangicTarihi && item.BitisTarihi > randevuBaslangicTarihi))
                    {
                        TempData["Message"] = "Randevu Saatleri Müsait Değil!";
                        return RedirectToAction("Adim3");
                    }
                }
            }

            RandevuModel randevuModel = new()
            {
                Calisan = calisan,
                CalisanId = calisanid,
                Musteri = musteri,
                MusteriId = musteri.KullaniciId,
                Islem = islem,
                IslemId = Convert.ToInt32(islemid),
                IstemTarihi = DateTime.Now,
                BaslangicTarihi = randevuBaslangicTarihi,
                BitisTarihi = randevuBitisTarihi,
                Durum = RandevuModel.RandevuDurum.OnayBekliyor
            };//?????? TODO

            Console.WriteLine(randevuModel.Musteri.Kullanici.Isim);
            Console.WriteLine(randevuModel.Musteri.Kullanici.Isim);
            Console.WriteLine(randevuModel.Musteri.Kullanici.Isim);
            Console.WriteLine(randevuModel.Musteri.Kullanici.Isim);
            Console.WriteLine(randevuModel.Musteri.Kullanici.Isim);

            _context.Randevular.Add(randevuModel);
            _context.SaveChanges();

            TempData["Message"] = "Randevu Çalışan Onayına Gönderilmiştir!";
            return RedirectToAction("Randevularim", "Randevu");
        }
    }
}

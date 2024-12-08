using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
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

        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            var calisanlar = _context.Calisanlar.Include(c => c.Kullanici).ToList();
            return View(calisanlar);
        }

        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public IActionResult CalisanEkle()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(KullaniciEkleViewModel model)
        {

            if (!ModelState.IsValid)
            {
                // Return the same view if something goes wrong
                return View(nameof(CalisanEkle));
            }

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

            var result = await _userManager.CreateAsync(newuser, model.Sifre);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(newuser, "Calisan");

                var calisan = new CalisanModel
                {
                    Kullanici = newuser,
                };

                await _context.Calisanlar.AddAsync(calisan);
                await _context.SaveChangesAsync();

                // Redirect to another page after successful registration
                return RedirectToAction("Index", "Calisan");
            }
            else
            {
                foreach (var error in result.Errors)
                    
                {
                    Console.WriteLine($"Error: {error.Code} - {error.Description}");
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(nameof(CalisanEkle));
        }
    }
}

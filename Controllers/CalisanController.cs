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

        public IActionResult Index()
        {
            var calisanlar = _context.Calisanlar.Include(c => c.Kullanici).ToList();
            return View(calisanlar);
        }

        public IActionResult CalisanEkle()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(KullaniciEkleViewModel model)
        {

            if (ModelState.IsValid)
            {
                var user = new KullaniciModel
                {
                    UserName = model.Email,
                    Email = model.Email,
                    PhoneNumber = model.Telefon,
                    Cinsiyet = model.Cinsiyet,
                    DogumTarihi = model.DogumTarihi,
                    Isim = model.Isim,
                    Soyisim = model.Soyisim,
                };

                var result = await _userManager.CreateAsync(user, model.Sifre);
                if (result.Succeeded)
                {
                    // Optionally assign roles to the user
                    await _userManager.AddToRoleAsync(user, "Calisan");

                    var calisan = new CalisanModel
                    {
                        Kullanici = user,
                    };
                    await _context.Calisanlar.AddAsync(calisan);
                    await _context.SaveChangesAsync();
                    Console.WriteLine("database kaydedildi heralde amk?");
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
            }

            // Return the same view if something goes wrong
            return View(nameof(CalisanEkle));
        }
    }

}

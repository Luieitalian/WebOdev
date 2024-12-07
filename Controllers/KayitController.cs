using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebOdev.Models;

namespace WebOdev.Controllers
{
    public class KayitController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public KayitController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CalisanEkle(KullaniciEkleViewModel model)
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

                    // Redirect to another page after successful registration
                    return RedirectToAction("Index", "Home");
                }

                // If there are errors, add them to ModelState
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            var errors = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();

            foreach (var error in errors)
            {
                Console.WriteLine(error);
            }

            // Return the same view if something goes wrong
            return View();
        }
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> KayitOl(KullaniciEkleViewModel model)
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
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(nameof(Index));
        }
    }
}

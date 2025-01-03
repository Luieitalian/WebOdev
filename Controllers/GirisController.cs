using System.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebOdev.Models;

namespace WebOdev.Controllers
{
    public class GirisController : Controller
    {
        private readonly ILogger<GirisController> _logger;
        private readonly UserManager<KullaniciModel> _userManager;
        private readonly SignInManager<KullaniciModel> _signInManager;

        public GirisController(UserManager<KullaniciModel> userManager, SignInManager<KullaniciModel> signInManager, ILogger<GirisController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> GirisYap(string email, string password, bool rememberMe)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(user, password, rememberMe, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Anasayfa"); // Redirect to a secured page or home page
                }

                ModelState.AddModelError("", "Invalid login attempt.");
            }
            else
            {
                ModelState.AddModelError("", "User not found.");
            }

            return View(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> CikisYap()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Anasayfa");
        }

        [HttpGet]
        public IActionResult YetkisizGiris()
        {
            return View();
        }
    }
}

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebOdev.Models;

namespace WebOdev.Controllers
{
    public class KayitController : Controller
    {
        private readonly UserManager<KullaniciModel> _userManager;
        private readonly SignInManager<KullaniciModel> _signInManager;
        private readonly ApplicationDbContext _context;

        public KayitController(ApplicationDbContext context, UserManager<KullaniciModel> userManager, SignInManager<KullaniciModel> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> KayitOl(string email, string password)
        {
            if (ModelState.IsValid)
            {
                var user = new KullaniciModel
                {
                    UserName = email,
                    Email = email,
                };

                var result = await _userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Musteri");
                    await _signInManager.SignInAsync(user, isPersistent: false);

                    var newmusteri = new MusteriModel 
                    {
                        Kullanici = user,
                    };

                    await _context.Musteriler.AddAsync(newmusteri);
                    await _context.SaveChangesAsync();

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

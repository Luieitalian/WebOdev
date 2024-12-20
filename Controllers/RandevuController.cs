using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebOdev.Models; // Model sınıfları
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using AutoMapper;
using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace WebOdev.Controllers
{
    public class RandevuController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<KullaniciModel> _userManager;

        public RandevuController(ApplicationDbContext context, UserManager<KullaniciModel> userManager)
        {
            _userManager = userManager;
            _context = context;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Randevular()
        {
            var randevular = _context.Randevular
                .Include(r => r.Calisan)
                .ThenInclude(c => c.Kullanici)
                .Include(r => r.Islem)
                .Include(r => r.Musteri)
                .ToList();

            return View(randevular);
        }

        [Authorize(Roles = "Musteri")]
        public IActionResult Randevularim()
        {
            var userID = _userManager.GetUserId(User);

            var query = from r in _context.Randevular
                        join m in _context.Musteriler
                        on userID equals m.KullaniciId
                        select r;
            // TODO
            var randevular = query.ToList();

            return View(randevular);
        }
    }
}

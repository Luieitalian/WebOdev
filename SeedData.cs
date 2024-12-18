using Microsoft.AspNetCore.Identity;
using WebOdev.Models;

namespace WebOdev
{
    public class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<KullaniciModel>>();
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();

            // Define roles
            string[] roles = { "Admin", "Calisan", "Musteri" };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            // Create a default Admin user
            var adminUser = new KullaniciModel
            {
                UserName = "admin",
                Isim = "Berke",
                Soyisim = "Pite",
                Cinsiyet = CinsiyetEnum.Erkek,
                DogumTarihi = DateTime.Now,
                Email = "berke.pite@ogr.sakarya.edu.tr",
                EmailConfirmed = true,
                PhoneNumber = "05360251285",
                PhoneNumberConfirmed = true,
            };

            var adminUser2 = new KullaniciModel
            {
                UserName = "admin2",
                Isim = "Zehranur",
                Soyisim = "Sarı",
                Cinsiyet = CinsiyetEnum.Kadin,
                DogumTarihi = DateTime.Now,
                Email = "zehranur.sari@ogr.sakarya.edu.tr",
                EmailConfirmed = true,
                PhoneNumber = "05360000000", // Telefon numarasını güncellemeniz gerekebilir
                PhoneNumberConfirmed = true,
            };

            string adminPassword = "Admin@123";
            var user = await userManager.FindByEmailAsync(adminUser.Email);
            if (user == null)
            {
                var createAdmin = await userManager.CreateAsync(adminUser, adminPassword);
                if (createAdmin.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }

            string adminPassword2 = "Admin2@123";
            var user2 = await userManager.FindByEmailAsync(adminUser2.Email);
            if (user2 == null)
            {
                var createAdmin2 = await userManager.CreateAsync(adminUser2, adminPassword2);
                if (createAdmin2.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser2, "Admin");
                }
            }

            await CalisanlariEkle(roleManager, userManager, context);
            await IslemleriEkle(context);
        }

        public static async Task CalisanlariEkle(RoleManager<IdentityRole> roleManager, UserManager<KullaniciModel> userManager, ApplicationDbContext context)
        {
            var serra = new KullaniciModel
            {
                UserName = "serra@ornek.com",
                Isim = "Serra",
                Soyisim = "Yılmaz",
                Cinsiyet = CinsiyetEnum.Kadin,
                DogumTarihi = DateTime.Parse("2024-11-28"),
                Email = "serra@ornek.com",
                EmailConfirmed = true,
                PhoneNumber = "05321233322",
                PhoneNumberConfirmed = true,
            };
            var serrapassword = "Serra@123";
            var userserra = await userManager.FindByEmailAsync(serra.Email);
            if (userserra == null)
            {
                var createCalisan = await userManager.CreateAsync(serra, serrapassword);
                if (createCalisan.Succeeded)
                {
                    await userManager.AddToRoleAsync(serra, "Calisan");
                    var calisan = new CalisanModel
                    {
                        Kullanici = serra
                    };
                    await context.Calisanlar.AddAsync(calisan);
                }
            }

            var batukan = new KullaniciModel
            {
                UserName = "batukan@ornek.com",
                Isim = "Batukan",
                Soyisim = "Keskin",
                Cinsiyet = CinsiyetEnum.Erkek,
                DogumTarihi = DateTime.Parse("2024-11-28"),
                Email = "batukan@ornek.com",
                EmailConfirmed = true,
                PhoneNumber = "05321223322",
                PhoneNumberConfirmed = true,
            };
            var batukanpassword = "Batukan@123";
            var userbatukan = await userManager.FindByEmailAsync(batukan.Email);
            if (userbatukan == null)
            {
                var createCalisan = await userManager.CreateAsync(batukan, batukanpassword);
                if (createCalisan.Succeeded)
                {
                    await userManager.AddToRoleAsync(batukan, "Calisan");
                    var calisan = new CalisanModel
                    {
                        Kullanici = batukan
                    };
                    await context.Calisanlar.AddAsync(calisan);
                }
            }

            var kemal = new KullaniciModel
            {
                UserName = "kemal@ornek.com",
                Isim = "Kemal",
                Soyisim = "Parlak",
                Cinsiyet = CinsiyetEnum.Erkek,
                DogumTarihi = DateTime.Parse("2024-11-28"),
                Email = "kemal@ornek.com",
                EmailConfirmed = true,
                PhoneNumber = "05322223322",
                PhoneNumberConfirmed = true,
            };
            var kemalpassword = "Kemal@123";
            var userkemal = await userManager.FindByEmailAsync(kemal.Email);
            if (userkemal == null)
            {
                var createCalisan = await userManager.CreateAsync(kemal, kemalpassword);
                if (createCalisan.Succeeded)
                {
                    await userManager.AddToRoleAsync(kemal, "Calisan");
                    var calisan = new CalisanModel
                    {
                        Kullanici = kemal
                    };
                    await context.Calisanlar.AddAsync(calisan);
                }
            }

            var ahmet = new KullaniciModel
            {
                UserName = "ahmet@ornek.com",
                Isim = "Ahmet",
                Soyisim = "Pekdemir",
                Cinsiyet = CinsiyetEnum.Erkek,
                DogumTarihi = DateTime.Parse("2024-11-28"),
                Email = "ahmet@ornek.com",
                EmailConfirmed = true,
                PhoneNumber = "05322243322",
                PhoneNumberConfirmed = true,
            };
            var ahmetpassword = "Ahmet@123";
            var userahmet = await userManager.FindByEmailAsync(ahmet.Email);
            if (userahmet == null)
            {
                var createCalisan = await userManager.CreateAsync(ahmet, ahmetpassword);
                if (createCalisan.Succeeded)
                {
                    await userManager.AddToRoleAsync(ahmet, "Calisan");
                    var calisan = new CalisanModel
                    {
                        Kullanici = ahmet
                    };
                    await context.Calisanlar.AddAsync(calisan);
                }
            }

            await context.SaveChangesAsync();
        }
        
        public static async Task IslemleriEkle(ApplicationDbContext _context)
        {
            if (_context.Islemler.Any()) return;

            var islem1 = new IslemModel
            {
                Baslik = "Kısa Dalgalı Saçlar",
                Aciklama = "Doğal dalgalarla modern ve hacimli bir görünüm sağlar.",
                Cinsiyet = CinsiyetEnum.Kadin,
                Ucret = 150,
                Uzunluk = TimeSpan.FromMinutes(20)
            };

            _context.Islemler.Add(islem1);

            var islem2 = new IslemModel
            {
                Baslik = "Sade Topuz",
                Aciklama = "Şık ve zarif bir görünüm için düşük topuz modelidir.",
                Cinsiyet = CinsiyetEnum.Kadin,
                Ucret = 250,
                Uzunluk = TimeSpan.FromMinutes(30)
            };

            _context.Islemler.Add(islem2);

            var islem3 = new IslemModel
            {
                Baslik = "Büyük Dalga Saçlar",
                Aciklama = "Hollywood tarzı büyük ve hacimli dalgalarla dikkat çekici bir stil.",
                Cinsiyet = CinsiyetEnum.Kadin,
                Ucret = 250,
                Uzunluk = TimeSpan.FromMinutes(30)
            };

            _context.Islemler.Add(islem3);

            var islem4 = new IslemModel
            {
                Baslik = "Düz ve Parlak Saç",
                Aciklama = "Sağlıklı ve pürüzsüz bir görünüm için düzleştirilmiş uzun saçlar.",
                Cinsiyet = CinsiyetEnum.Kadin,
                Ucret = 200,
                Uzunluk = TimeSpan.FromMinutes(20)
            };

            _context.Islemler.Add(islem4);

            var islem5 = new IslemModel
            {
                Baslik = "Klasik Kısa",
                Aciklama = "Kısa, düzenli ve temiz bir kesim. Hem günlük kullanım hem de iş yerlerinde uygun.",
                Cinsiyet = CinsiyetEnum.Erkek,
                Ucret = 200,
                Uzunluk = TimeSpan.FromMinutes(10)
            };

            _context.Islemler.Add(islem5);

            var islem6 = new IslemModel
            {
                Baslik = "Undercut",
                Aciklama = "Yanlar ve arka kısım çok kısa kesilir, üst kısmı ise daha uzun bırakılır. Şık ve cesur bir stil.",
                Cinsiyet = CinsiyetEnum.Erkek,
                Ucret = 100,
                Uzunluk = TimeSpan.FromMinutes(15)
            };

            _context.Islemler.Add(islem6);

            var islem7 = new IslemModel
            {
                Baslik = "Buzz Cut",
                Aciklama = "Tamamen kısa kesilen bir modeldir. Bakımı kolay ve rahat bir seçenek.",
                Cinsiyet = CinsiyetEnum.Erkek,
                Ucret = 100,
                Uzunluk = TimeSpan.FromMinutes(10)
            };

            _context.Islemler.Add(islem7);

            await _context.SaveChangesAsync();
        }

    }
}

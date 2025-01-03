﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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

            IslemleriEkle(context);
            await CalisanlariEkle(roleManager, userManager, context);

            #region admins

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

            //string adminPassword2 = "Admin2@123";
            //var user2 = await userManager.FindByEmailAsync(adminUser2.Email);
            //if (user2 == null)
            //{
            //    var createAdmin2 = await userManager.CreateAsync(adminUser2, adminPassword2);
            //    if (createAdmin2.Succeeded)
            //    {
            //        await userManager.AddToRoleAsync(adminUser2, "Admin");
            //    }
            //}
            #endregion

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
                    var calisanislem = new CalisanIslemModel
                    {
                        Calisan = calisan,
                        IslemId = 2,
                        Yetkinlik = 3,
                    };

                    context.Calisanlar.Add(calisan);
                    context.CalisanIslemleri.Add(calisanislem);
                }
                else
                {
                    Console.WriteLine("\nfailed to create user\n");
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
                    var calisanislem = new CalisanIslemModel
                    {
                        Calisan = calisan,
                        IslemId = 2,
                        Yetkinlik = 3,
                    };
                    var calisanislem2 = new CalisanIslemModel
                    {
                        Calisan = calisan,
                        IslemId = 4,
                        Yetkinlik = 6,
                    };
                    var calisanislem3 = new CalisanIslemModel
                    {
                        Calisan = calisan,
                        IslemId = 1,
                        Yetkinlik = 8,
                    };
                    var calisanislem4 = new CalisanIslemModel
                    {
                        Calisan = calisan,
                        IslemId = 3,
                        Yetkinlik = 6,
                    };

                    context.Calisanlar.Add(calisan);
                    context.CalisanIslemleri.Add(calisanislem);
                    context.CalisanIslemleri.Add(calisanislem2);
                    context.CalisanIslemleri.Add(calisanislem3);
                    context.CalisanIslemleri.Add(calisanislem4);
                }
                else
                {
                    Console.WriteLine("\nfailed to create user\n");
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
                    var calisanislem = new CalisanIslemModel
                    {
                        Calisan = calisan,
                        IslemId = 7,
                        Yetkinlik = 3,
                    };
                    var calisanislem2 = new CalisanIslemModel
                    {
                        Calisan = calisan,
                        IslemId = 6,
                        Yetkinlik = 6,
                    };
                    var calisanislem3 = new CalisanIslemModel
                    {
                        Calisan = calisan,
                        IslemId = 5,
                        Yetkinlik = 8,
                    };
                    var calisanislem4 = new CalisanIslemModel
                    {
                        Calisan = calisan,
                        IslemId = 4,
                        Yetkinlik = 6,
                    };

                    context.Calisanlar.Add(calisan);
                    context.CalisanIslemleri.Add(calisanislem);
                    context.CalisanIslemleri.Add(calisanislem2);
                    context.CalisanIslemleri.Add(calisanislem3);
                    context.CalisanIslemleri.Add(calisanislem4);
                }
                else
                {
                    Console.WriteLine("\nfailed to create user\n");
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
                    var calisanislem = new CalisanIslemModel
                    {
                        Calisan = calisan,
                        IslemId = 7,
                        Yetkinlik = 1,
                    };
                    var calisanislem2 = new CalisanIslemModel
                    {
                        Calisan = calisan,
                        IslemId = 3,
                        Yetkinlik = 5,
                    };
                    var calisanislem3 = new CalisanIslemModel
                    {
                        Calisan = calisan,
                        IslemId = 2,
                        Yetkinlik = 2,
                    };
                    var calisanislem4 = new CalisanIslemModel
                    {
                        Calisan = calisan,
                        IslemId = 1,
                        Yetkinlik = 2,
                    };

                    context.Calisanlar.Add(calisan);
                    context.CalisanIslemleri.Add(calisanislem);
                    context.CalisanIslemleri.Add(calisanislem2);
                    context.CalisanIslemleri.Add(calisanislem3);
                    context.CalisanIslemleri.Add(calisanislem4);
                }
                else
                {
                    Console.WriteLine("\nfailed to create user\n");
                }
            }

            context.SaveChanges();
        }

        public static void IslemleriEkle(ApplicationDbContext _context)
        {
            if (_context.Islemler.Any()) return;

            var islem1 = new IslemModel
            {
                Id = 1,
                Baslik = "Kısa Dalgalı Saçlar",
                Aciklama = "Doğal dalgalarla modern ve hacimli bir görünüm sağlar.",
                Cinsiyet = CinsiyetEnum.Kadin,
                Ucret = 150,
                Uzunluk = TimeSpan.FromMinutes(20),
                ImageURL = "https://static.vecteezy.com/system/resources/previews/049/085/400/non_2x/young-woman-with-wavy-hair-wearing-white-t-shirt-poses-against-a-plain-background-free-photo.jpeg"
            };

            _context.Islemler.Add(islem1);

            var islem2 = new IslemModel
            {
                Id = 2,
                Baslik = "Sade Topuz",
                Aciklama = "Şık ve zarif bir görünüm için düşük topuz modelidir.",
                Cinsiyet = CinsiyetEnum.Kadin,
                Ucret = 250,
                Uzunluk = TimeSpan.FromMinutes(30),
                ImageURL = "https://static.vecteezy.com/system/resources/previews/031/299/513/non_2x/a-beautiful-bride-with-a-bun-hairstyles-for-long-hair-look-from-back-a-female-hairstyle-rear-view-concept-by-ai-generated-free-photo.jpg"
            };

            _context.Islemler.Add(islem2);

            var islem3 = new IslemModel
            {
                Id = 3,
                Baslik = "Büyük Dalgalı Saçlar",
                Aciklama = "Hollywood tarzı büyük ve hacimli dalgalarla dikkat çekici bir stil.",
                Cinsiyet = CinsiyetEnum.Kadin,
                Ucret = 250,
                Uzunluk = TimeSpan.FromMinutes(30),
                ImageURL = "https://static.vecteezy.com/system/resources/previews/045/891/019/non_2x/woman-with-long-blonde-hair-seen-from-behind-free-photo.jpeg"
            };

            _context.Islemler.Add(islem3);

            var islem4 = new IslemModel
            {
                Id = 4,
                Baslik = "Düz ve Parlak Saç",
                Aciklama = "Sağlıklı ve pürüzsüz bir görünüm için düzleştirilmiş uzun saçlar.",
                Cinsiyet = CinsiyetEnum.Kadin,
                Ucret = 200,
                Uzunluk = TimeSpan.FromMinutes(20),
                ImageURL = "https://static.vecteezy.com/system/resources/previews/034/923/792/non_2x/back-view-of-a-beautiful-young-woman-with-long-healthy-hair-on-a-dark-background-rear-view-of-a-beautiful-woman-with-long-straight-hair-blond-girl-ai-generated-free-photo.jpg"
            };

            _context.Islemler.Add(islem4);

            var islem5 = new IslemModel
            {
                Id = 5,
                Baslik = "Klasik Kısa",
                Aciklama = "Kısa, düzenli ve temiz bir kesim. Hem günlük kullanım hem de iş yerlerinde uygun.",
                Cinsiyet = CinsiyetEnum.Erkek,
                Ucret = 200,
                Uzunluk = TimeSpan.FromMinutes(10),
                ImageURL = "https://static.vecteezy.com/system/resources/previews/053/631/100/non_2x/a-man-in-a-white-shirt-looking-up-free-photo.jpeg"
            };

            _context.Islemler.Add(islem5);

            var islem6 = new IslemModel
            {
                Id = 6,
                Baslik = "Undercut",
                Aciklama = "Yanlar ve arka kısım çok kısa kesilir, üst kısmı ise daha uzun bırakılır. Şık ve cesur bir stil.",
                Cinsiyet = CinsiyetEnum.Erkek,
                Ucret = 100,
                Uzunluk = TimeSpan.FromMinutes(15),
                ImageURL = "https://static.vecteezy.com/system/resources/previews/030/703/495/non_2x/man-hair-style-from-back-side-free-photo.jpg"
            };

            _context.Islemler.Add(islem6);

            var islem7 = new IslemModel
            {
                Id = 7,
                Baslik = "Buzz Cut",
                Aciklama = "Tamamen kısa kesilen bir modeldir. Bakımı kolay ve rahat bir seçenek.",
                Cinsiyet = CinsiyetEnum.Erkek,
                Ucret = 100,
                Uzunluk = TimeSpan.FromMinutes(10),
                ImageURL = "https://static.vecteezy.com/system/resources/previews/048/988/824/non_2x/casual-male-fashion-model-in-soft-green-t-shirt-relaxed-lifestylegraphy-stylish-and-comfortable-males-fashion-with-a-touch-of-trendiness-free-photo.jpg"
            };

            _context.Islemler.Add(islem7);

            _context.SaveChanges();
        }
    }
}

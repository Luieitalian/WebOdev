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
        }
    }

}

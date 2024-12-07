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
                EmailConfirmed = true
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
        }
    }

}

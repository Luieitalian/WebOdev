using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebOdev.Models;

namespace WebOdev
{
    public class ApplicationDbContext : IdentityDbContext<KullaniciModel>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        public DbSet<CalisanModel> Calisanlar { get; set; }
        public DbSet<IslemModel> Islemler { get; set; }
        public DbSet<CalisanIslemModel> CalisanIslemleri { get; set; }
        public DbSet<RandevuModel> Randevular { get; set; }
        public DbSet<OnayliRandevuModel> OnayliRandevular { get; set; }
        public DbSet<MusteriModel> Musteriler { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=WebDB;Username=berkepite;Password=1234");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CalisanIslemModel>()
                .HasKey(pa => new { pa.CalisanId, pa.IslemId });

            modelBuilder.Entity<CalisanIslemModel>()
                .HasOne(pa => pa.Calisan)
                .WithMany(p => p.CalisanIslemleri)
                .HasForeignKey(pa => pa.CalisanId);

            modelBuilder.Entity<CalisanIslemModel>()
                .HasOne(pa => pa.Islem)
                .WithMany(a => a.CalisanIslemleri)
                .HasForeignKey(pa => pa.IslemId);

            modelBuilder.Entity<KullaniciModel>()
                .Property(c => c.DogumTarihi)
                .HasColumnType("date");

            modelBuilder.Entity<KullaniciModel>()
                .Property(c => c.Cinsiyet)
                .HasConversion<int>(); // Enum as integer

            modelBuilder.Entity<IslemModel>()
                .Property(c => c.Cinsiyet)
                .HasConversion<int>(); // Enum as integer

            modelBuilder.Entity<OnayliRandevuModel>()
                .HasOne(or => or.Randevu) // One OnayliRandevuModel has one RandevuModel
                .WithOne()                // One RandevuModel has one OnayliRandevuModel
                .HasForeignKey<OnayliRandevuModel>(or => or.RandevuId); // FK and PK in OnayliRandevuModel

            modelBuilder.Entity<RandevuModel>()
                .HasOne(r => r.Calisan)  // RandevuModel has one Calisan
                .WithMany()              // CalisanModel can have many RandevuModels
                .HasForeignKey(r => r.CalisanId); // Foreign Key in RandevuModel

            modelBuilder.Entity<RandevuModel>()
                .HasOne(r => r.Musteri)  // RandevuModel has one Calisan
                .WithMany()              // MusteriModel can have many RandevuModels
                .HasForeignKey(r => r.MusteriId); // Foreign Key in RandevuModel

            modelBuilder.Entity<RandevuModel>()
                .HasOne(r => r.Islem)  // RandevuModel has one Calisan
                .WithMany()              // IslemModel can have many RandevuModels
                .HasForeignKey(r => r.IslemId); // Foreign Key in RandevuModel

            modelBuilder.Entity<CalisanModel>()
                .HasOne(c => c.Kullanici) // Calisan has one KullaniciModel
                .WithOne()                  // KullaniciModel has one Calisan
                .HasForeignKey<CalisanModel>(a => a.KullaniciId); // Use KullaniciId as FK

            modelBuilder.Entity<MusteriModel>()
                .HasOne(c => c.Kullanici) // Musteri has one KullaniciModel
                .WithOne() // KullaniciModel has one Musteri
                .HasForeignKey<MusteriModel>(a => a.KullaniciId); // Use MusteriId as FK
        }
    }
}

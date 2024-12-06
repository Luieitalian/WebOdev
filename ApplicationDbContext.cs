using Microsoft.EntityFrameworkCore;
using WebOdev.Models;

namespace WebOdev
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        public DbSet<CalisanModel> Calisanlar { get; set; }

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
                .HasKey(pa => new { pa.CalisanID, pa.IslemID });

            // Foreign Key Relationship: Personnel -> PersonnelAbility
            modelBuilder.Entity<CalisanIslemModel>()
                .HasOne(pa => pa.Calisan)
                .WithMany(p => p.CalisanIslemleri)
                .HasForeignKey(pa => pa.CalisanID);

            // Foreign Key Relationship: Ability -> PersonnelAbility
            modelBuilder.Entity<CalisanIslemModel>()
                .HasOne(pa => pa.Islem)
                .WithMany(a => a.CalisanIslemleri)
                .HasForeignKey(pa => pa.IslemID);

            modelBuilder.Entity<CalisanModel>()
                .Property(c => c.DogumTarihi)
                .HasColumnType("date");
        }
    }
}

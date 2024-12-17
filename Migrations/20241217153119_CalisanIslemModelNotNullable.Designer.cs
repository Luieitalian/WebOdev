﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using WebOdev;

#nullable disable

namespace WebOdev.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20241217153119_CalisanIslemModelNotNullable")]
    partial class CalisanIslemModelNotNullable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("text");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .HasColumnType("text");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("WebOdev.Models.CalisanIslemModel", b =>
                {
                    b.Property<string>("CalisanId")
                        .HasColumnType("text");

                    b.Property<int>("IslemId")
                        .HasColumnType("integer");

                    b.Property<string>("Not")
                        .HasColumnType("text");

                    b.Property<int>("Yetkinlik")
                        .HasColumnType("integer");

                    b.HasKey("CalisanId", "IslemId");

                    b.HasIndex("IslemId");

                    b.ToTable("CalisanIslemleri");
                });

            modelBuilder.Entity("WebOdev.Models.CalisanModel", b =>
                {
                    b.Property<string>("KullaniciId")
                        .HasColumnType("text");

                    b.HasKey("KullaniciId");

                    b.ToTable("Calisanlar");
                });

            modelBuilder.Entity("WebOdev.Models.IslemModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Aciklama")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Baslik")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Cinsiyet")
                        .HasColumnType("integer");

                    b.Property<int>("Ucret")
                        .HasColumnType("integer");

                    b.Property<TimeSpan>("Uzunluk")
                        .HasColumnType("interval");

                    b.HasKey("Id");

                    b.ToTable("Islemler");
                });

            modelBuilder.Entity("WebOdev.Models.KullaniciModel", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<int?>("Cinsiyet")
                        .HasColumnType("integer");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<DateTime?>("DogumTarihi")
                        .HasColumnType("date");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("Isim")
                        .HasColumnType("text");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<string>("Soyisim")
                        .HasColumnType("text");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("WebOdev.Models.MusteriModel", b =>
                {
                    b.Property<string>("KullaniciId")
                        .HasColumnType("text");

                    b.HasKey("KullaniciId");

                    b.ToTable("Musteriler");
                });

            modelBuilder.Entity("WebOdev.Models.OnayliRandevuModel", b =>
                {
                    b.Property<int>("RandevuId")
                        .HasColumnType("integer");

                    b.HasKey("RandevuId");

                    b.ToTable("OnayliRandevular");
                });

            modelBuilder.Entity("WebOdev.Models.RandevuModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("BaslangicTarihi")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("BitisTarihi")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CalisanId")
                        .HasColumnType("text");

                    b.Property<int?>("IslemId")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("IstemTarihi")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("MusteriId")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CalisanId");

                    b.HasIndex("IslemId");

                    b.HasIndex("MusteriId");

                    b.ToTable("Randevular");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("WebOdev.Models.KullaniciModel", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("WebOdev.Models.KullaniciModel", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebOdev.Models.KullaniciModel", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("WebOdev.Models.KullaniciModel", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WebOdev.Models.CalisanIslemModel", b =>
                {
                    b.HasOne("WebOdev.Models.CalisanModel", "Calisan")
                        .WithMany("CalisanIslemleri")
                        .HasForeignKey("CalisanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebOdev.Models.IslemModel", "Islem")
                        .WithMany("CalisanIslemleri")
                        .HasForeignKey("IslemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Calisan");

                    b.Navigation("Islem");
                });

            modelBuilder.Entity("WebOdev.Models.CalisanModel", b =>
                {
                    b.HasOne("WebOdev.Models.KullaniciModel", "Kullanici")
                        .WithOne()
                        .HasForeignKey("WebOdev.Models.CalisanModel", "KullaniciId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Kullanici");
                });

            modelBuilder.Entity("WebOdev.Models.MusteriModel", b =>
                {
                    b.HasOne("WebOdev.Models.KullaniciModel", "Kullanici")
                        .WithOne()
                        .HasForeignKey("WebOdev.Models.MusteriModel", "KullaniciId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Kullanici");
                });

            modelBuilder.Entity("WebOdev.Models.OnayliRandevuModel", b =>
                {
                    b.HasOne("WebOdev.Models.RandevuModel", "Randevu")
                        .WithOne()
                        .HasForeignKey("WebOdev.Models.OnayliRandevuModel", "RandevuId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Randevu");
                });

            modelBuilder.Entity("WebOdev.Models.RandevuModel", b =>
                {
                    b.HasOne("WebOdev.Models.CalisanModel", "Calisan")
                        .WithMany()
                        .HasForeignKey("CalisanId");

                    b.HasOne("WebOdev.Models.IslemModel", "Islem")
                        .WithMany()
                        .HasForeignKey("IslemId");

                    b.HasOne("WebOdev.Models.MusteriModel", "Musteri")
                        .WithMany()
                        .HasForeignKey("MusteriId");

                    b.Navigation("Calisan");

                    b.Navigation("Islem");

                    b.Navigation("Musteri");
                });

            modelBuilder.Entity("WebOdev.Models.CalisanModel", b =>
                {
                    b.Navigation("CalisanIslemleri");
                });

            modelBuilder.Entity("WebOdev.Models.IslemModel", b =>
                {
                    b.Navigation("CalisanIslemleri");
                });
#pragma warning restore 612, 618
        }
    }
}

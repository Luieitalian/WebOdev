﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using WebOdev;

#nullable disable

namespace WebOdev.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("WebOdev.Models.CalisanIslemModel", b =>
                {
                    b.Property<int>("CalisanID")
                        .HasColumnType("integer");

                    b.Property<int>("IslemID")
                        .HasColumnType("integer");

                    b.Property<string>("Not")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Yetkinlik")
                        .HasColumnType("integer");

                    b.HasKey("CalisanID", "IslemID");

                    b.HasIndex("IslemID");

                    b.ToTable("CalisanIslemModel");
                });

            modelBuilder.Entity("WebOdev.Models.CalisanModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Cinsiyet")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("DogumTarihi")
                        .HasColumnType("date");

                    b.Property<string>("Isim")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Soyisim")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Telefon")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

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

                    b.Property<string>("Cinsiyet")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Ucret")
                        .HasColumnType("integer");

                    b.Property<TimeSpan>("Uzunluk")
                        .HasColumnType("interval");

                    b.HasKey("Id");

                    b.ToTable("IslemModel");
                });

            modelBuilder.Entity("WebOdev.Models.CalisanIslemModel", b =>
                {
                    b.HasOne("WebOdev.Models.CalisanModel", "Calisan")
                        .WithMany("CalisanIslemleri")
                        .HasForeignKey("CalisanID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebOdev.Models.IslemModel", "Islem")
                        .WithMany("CalisanIslemleri")
                        .HasForeignKey("IslemID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Calisan");

                    b.Navigation("Islem");
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
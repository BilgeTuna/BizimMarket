﻿// <auto-generated />
using BizimMarket.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BizimMarket.Migrations
{
    [DbContext(typeof(BizimMarketContext))]
    partial class BizimMarketContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.13")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BizimMarket.Models.Kategori", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Ad")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Kategoriler");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Ad = "Atıştırmalık"
                        },
                        new
                        {
                            Id = 2,
                            Ad = "Dondurma"
                        },
                        new
                        {
                            Id = 3,
                            Ad = "Fırın"
                        });
                });

            modelBuilder.Entity("BizimMarket.Models.Urun", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Ad")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<decimal>("Fiyat")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("KategoriId")
                        .HasColumnType("int");

                    b.Property<string>("ResimYolu")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("KategoriId");

                    b.ToTable("Urunler");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Ad = "Kinder Joy 20g",
                            Fiyat = 7.90m,
                            KategoriId = 1,
                            ResimYolu = "kinderjoy.jpg"
                        },
                        new
                        {
                            Id = 2,
                            Ad = "Toblerone Sütlü Çikolata 100g",
                            Fiyat = 18.90m,
                            KategoriId = 1,
                            ResimYolu = "toblerone.jpg"
                        },
                        new
                        {
                            Id = 3,
                            Ad = "Tadelle Fındık Dolgulu Sütlü Çikolata 30g",
                            Fiyat = 4.95m,
                            KategoriId = 1,
                            ResimYolu = "tadelle.jpg"
                        },
                        new
                        {
                            Id = 4,
                            Ad = "Algida Maraş Usulü Sade Çikolata Dondurma 500ml",
                            Fiyat = 28.50m,
                            KategoriId = 2,
                            ResimYolu = "algidamaras.jpg"
                        },
                        new
                        {
                            Id = 5,
                            Ad = "Carte d'Or Selection Meyve Şöleni 850ml",
                            Fiyat = 36.90m,
                            KategoriId = 2,
                            ResimYolu = "cartedormeyve.jpg"
                        },
                        new
                        {
                            Id = 6,
                            Ad = "Minik Sandviç 10'lu",
                            Fiyat = 17.90m,
                            KategoriId = 3,
                            ResimYolu = "miniksandvic.jpg"
                        });
                });

            modelBuilder.Entity("BizimMarket.Models.Urun", b =>
                {
                    b.HasOne("BizimMarket.Models.Kategori", "Kategori")
                        .WithMany("Urunler")
                        .HasForeignKey("KategoriId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Kategori");
                });

            modelBuilder.Entity("BizimMarket.Models.Kategori", b =>
                {
                    b.Navigation("Urunler");
                });
#pragma warning restore 612, 618
        }
    }
}

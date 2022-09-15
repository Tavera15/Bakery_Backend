﻿// <auto-generated />
using System;
using API.DataAccess.SQL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace API.DataAccess.SQL.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("API.Core.Models.Basket", b =>
                {
                    b.Property<string>("mID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("mTimeEntered")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("mTimeModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("mID");

                    b.ToTable("Baskets");
                });

            modelBuilder.Entity("API.Core.Models.BasketItem", b =>
                {
                    b.Property<string>("mID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("basketmID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("mTimeEntered")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("mTimeModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("productmID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("qty")
                        .HasColumnType("int");

                    b.Property<string>("sizeSelected")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("mID");

                    b.HasIndex("basketmID");

                    b.HasIndex("productmID");

                    b.ToTable("BasketItems");
                });

            modelBuilder.Entity("API.Core.Models.Product", b =>
                {
                    b.Property<string>("mID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("mDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("mIsAvailable")
                        .HasColumnType("bit");

                    b.Property<DateTime>("mTimeEntered")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("mTimeModified")
                        .HasColumnType("datetime2");

                    b.Property<double>("mUnitPrice")
                        .HasColumnType("float");

                    b.Property<string>("name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("mID");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("API.Core.Models.BasketItem", b =>
                {
                    b.HasOne("API.Core.Models.Basket", "basket")
                        .WithMany("basketItems")
                        .HasForeignKey("basketmID");

                    b.HasOne("API.Core.Models.Product", "product")
                        .WithMany()
                        .HasForeignKey("productmID");

                    b.Navigation("basket");

                    b.Navigation("product");
                });

            modelBuilder.Entity("API.Core.Models.Basket", b =>
                {
                    b.Navigation("basketItems");
                });
#pragma warning restore 612, 618
        }
    }
}

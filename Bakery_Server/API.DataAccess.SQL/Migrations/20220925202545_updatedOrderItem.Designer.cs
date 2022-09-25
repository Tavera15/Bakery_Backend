﻿// <auto-generated />
using System;
using API.DataAccess.SQL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace API.DataAccess.SQL.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20220925202545_updatedOrderItem")]
    partial class updatedOrderItem
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("API.Core.Models.BakeryOrder", b =>
                {
                    b.Property<string>("mID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("addressLine1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("addressLine2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("city")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("customerEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("customerPhone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("grandTotal")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("mTimeEntered")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("mTimeModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("state")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("zipCode")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("mID");

                    b.ToTable("BakeryOrder");
                });

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

            modelBuilder.Entity("API.Core.Models.OrderItem", b =>
                {
                    b.Property<string>("mID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("mTimeEntered")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("mTimeModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("parentOrdermID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("productDesc")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("productPrice")
                        .HasColumnType("float");

                    b.Property<int>("quantity")
                        .HasColumnType("int");

                    b.Property<string>("sizeSelected")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("total")
                        .HasColumnType("float");

                    b.HasKey("mID");

                    b.HasIndex("parentOrdermID");

                    b.ToTable("OrderItem");
                });

            modelBuilder.Entity("API.Core.Models.Product", b =>
                {
                    b.Property<string>("mID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("mAvailableSizes")
                        .HasColumnType("nvarchar(max)");

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

            modelBuilder.Entity("API.Core.Models.ProductImage", b =>
                {
                    b.Property<string>("mID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("imageSource")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("mTimeEntered")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("mTimeModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("productmID")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("mID");

                    b.HasIndex("productmID");

                    b.ToTable("ProductImages");
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

            modelBuilder.Entity("API.Core.Models.OrderItem", b =>
                {
                    b.HasOne("API.Core.Models.BakeryOrder", "parentOrder")
                        .WithMany("orderItems")
                        .HasForeignKey("parentOrdermID");

                    b.Navigation("parentOrder");
                });

            modelBuilder.Entity("API.Core.Models.ProductImage", b =>
                {
                    b.HasOne("API.Core.Models.Product", "product")
                        .WithMany("productImages")
                        .HasForeignKey("productmID");

                    b.Navigation("product");
                });

            modelBuilder.Entity("API.Core.Models.BakeryOrder", b =>
                {
                    b.Navigation("orderItems");
                });

            modelBuilder.Entity("API.Core.Models.Basket", b =>
                {
                    b.Navigation("basketItems");
                });

            modelBuilder.Entity("API.Core.Models.Product", b =>
                {
                    b.Navigation("productImages");
                });
#pragma warning restore 612, 618
        }
    }
}

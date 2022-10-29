﻿// <auto-generated />
using System;
using Microservice.Persistence.EFCore.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Microservice.Persistence.EFCore.Migrations
{
    [DbContext(typeof(MicroservicePersistenceEFcoreContext))]
    [Migration("20221022185126_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Microservice.Persistence.EFCore.Data.Entities.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderId"), 1L, 1);

                    b.Property<string>("Description")
                        .HasMaxLength(250)
                        .IsUnicode(false)
                        .HasColumnType("varchar(250)");

                    b.HasKey("OrderId");

                    b.ToTable("Order", (string)null);
                });

            modelBuilder.Entity("Microservice.Persistence.EFCore.Data.Entities.OrderItem", b =>
                {
                    b.Property<int>("OrderItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderItemId"), 1L, 1);

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.HasKey("OrderItemId");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderItem", (string)null);
                });

            modelBuilder.Entity("Microservice.Persistence.EFCore.Data.Entities.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductId"), 1L, 1);

                    b.Property<string>("Description")
                        .HasMaxLength(250)
                        .IsUnicode(false)
                        .HasColumnType("varchar(250)");

                    b.Property<decimal?>("Price")
                        .HasColumnType("decimal(30,6)");

                    b.Property<string>("Unit")
                        .HasMaxLength(3)
                        .IsUnicode(false)
                        .HasColumnType("varchar(3)");

                    b.HasKey("ProductId");

                    b.ToTable("Product", (string)null);
                });

            modelBuilder.Entity("Microservice.Persistence.EFCore.Data.Entities.OrderItem", b =>
                {
                    b.HasOne("Microservice.Persistence.EFCore.Data.Entities.Order", "Order")
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderId")
                        .IsRequired()
                        .HasConstraintName("FK_OrdernItemOrden");

                    b.HasOne("Microservice.Persistence.EFCore.Data.Entities.Product", "Product")
                        .WithMany("OrderItems")
                        .HasForeignKey("ProductId")
                        .IsRequired()
                        .HasConstraintName("FK_OrdernItemProducto");

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Microservice.Persistence.EFCore.Data.Entities.Order", b =>
                {
                    b.Navigation("OrderItems");
                });

            modelBuilder.Entity("Microservice.Persistence.EFCore.Data.Entities.Product", b =>
                {
                    b.Navigation("OrderItems");
                });
#pragma warning restore 612, 618
        }
    }
}
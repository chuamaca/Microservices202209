﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using Microservices.Monitoring.Sales.API.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore;
using System;

namespace Microservices.Monitoring.Sales.API.Infrastructure.Data.Contexts
{
    public class StoreConfiguration : IEntityTypeConfiguration<Store>
    {
        public void Configure(EntityTypeBuilder<Store> entity)
        {
            entity.ToTable("Store", "Sales");

            entity.Property(e => e.City)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.Property(e => e.Phone)
                .HasMaxLength(25)
                .IsUnicode(false);

            entity.Property(e => e.State)
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.Property(e => e.StoreName)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.Property(e => e.Street)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.Property(e => e.ZipCode)
                .HasMaxLength(5)
                .IsUnicode(false);
        }
    }
}

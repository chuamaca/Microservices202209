using Microservice.Persistence.NoSQL.Cosmo.EFCore.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microservice.Persistence.NoSQL.Cosmo.EFCore.Data.Contexts
{
    public class OrderNoSqlContext : DbContext
    {
        public OrderNoSqlContext()
        {
        }

        public OrderNoSqlContext(DbContextOptions<OrderNoSqlContext> options)
            : base(options)
        {
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<Distributor> Distributors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultContainer("Store");
            modelBuilder.Entity<Distributor>().OwnsMany(p => p.ShippingCenters);

            modelBuilder.Entity<Order>().ToContainer("Orders");
            modelBuilder.Entity<Order>().HasNoDiscriminator();
            modelBuilder.Entity<Order>().HasPartitionKey(o => o.PartitionKey);
            modelBuilder.Entity<Order>().OwnsOne(
                o => o.ShippingAddress,
                sa =>
                {
                    sa.ToJsonProperty("Address");
                    sa.Property(p => p.Street).ToJsonProperty("ShipsToStreet");
                    sa.Property(p => p.City).ToJsonProperty("ShipsToCity");
                });
        }
    }
}

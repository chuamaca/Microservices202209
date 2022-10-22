using Microservice.Persistence.NoSQL.Cosmo.EFCore.Data.Contexts;
using Microservice.Persistence.NoSQL.Cosmo.EFCore.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Microservice.Persistence.NoSQL.Cosmo.EFCore.Data.Init
{
    public static class DataSeeder
    {
        public static void SeedOrders(OrderNoSqlContext context)
        {

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            context.Orders.Add(new Order
            {
                Id = 1,
                ShippingAddress = new StreetAddress { City = "London", Street = "221 B Baker St" },
                PartitionKey = "1"
            });

            context.Orders.Add(new Order
            {
                Id = 2,
                ShippingAddress = new StreetAddress { City = "New York", Street = "11 Wall Street" },
                PartitionKey = "2"
            });

            context.Distributors.Add(new Distributor
            {
                Id = 1,
                ShippingCenters = new HashSet<StreetAddress> {
                        new StreetAddress { City = "Phoenix", Street = "500 S 48th Street" },
                        new StreetAddress { City = "Anaheim", Street = "5650 Dolly Ave" }
                    }
            });

            context.SaveChanges();
        }
    }
}

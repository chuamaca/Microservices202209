using System;
using System.Collections.Generic;
using System.Text;

namespace Microservice.Persistence.NoSQL.Cosmo.EFCore.Data.Entities
{
    public class Distributor
    {
        public int Id { get; set; }
        public ICollection<StreetAddress> ShippingCenters { get; set; }
    }
}

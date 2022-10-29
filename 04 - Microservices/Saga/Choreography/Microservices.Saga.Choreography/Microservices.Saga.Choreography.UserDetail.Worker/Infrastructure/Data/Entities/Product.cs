using System;
using System.Collections.Generic;

namespace Microservices.Saga.Choreography.UserDetail.Worker.Infrastructure.Data.Entities
{
    public partial class Product
    {
        public Product()
        {
            UserDetails = new HashSet<UserDetail>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public decimal? Price { get; set; }
        public bool? IsActive { get; set; }

        public virtual ICollection<UserDetail> UserDetails { get; set; }
    }
}

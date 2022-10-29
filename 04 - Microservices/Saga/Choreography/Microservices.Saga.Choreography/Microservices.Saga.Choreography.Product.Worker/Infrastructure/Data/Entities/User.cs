using System;
using System.Collections.Generic;

namespace Microservices.Saga.Choreography.Product.Worker.Infrastructure.Data.Entities
{
    public partial class User
    {
        public User()
        {
            UserDetails = new HashSet<UserDetail>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int? No { get; set; }
        public bool? IsActive { get; set; }

        public virtual ICollection<UserDetail> UserDetails { get; set; }
    }
}

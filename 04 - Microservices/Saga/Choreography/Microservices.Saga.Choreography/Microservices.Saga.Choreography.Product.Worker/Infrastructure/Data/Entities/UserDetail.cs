using System;
using System.Collections.Generic;

namespace Microservices.Saga.Choreography.Product.Worker.Infrastructure.Data.Entities
{
    public partial class UserDetail
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public bool? IsActive { get; set; }

        public virtual Product Product { get; set; }
        public virtual User User { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservices.APIGateway.Sale.API.Models
{
    public class OrderItem
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}

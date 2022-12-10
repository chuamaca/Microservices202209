using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservices.APIGateway.Sale.API.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public string Description { get; set; }
        public List<OrderItem> Items { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}

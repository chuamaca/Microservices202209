
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservice.Persistence.Infrastructure.Dto
{
    public class OrderItemDto
    {
        public int OrderItemId { get; set; }
        public int OrderId { get; set; }
        public int  ProductId { get; set; }
        public string Description { get; set; }
        public Decimal? Price { get; set; }
    }
}

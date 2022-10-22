using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservice.Persistence.Infrastructure.Dto
{
    public class OrderDto
    {
        public int OrderId { get; set; }
        public string Description { get; set; }
        public ICollection<OrderItemDto> Items { get; set; }
    }
}

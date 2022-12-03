using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservices.Sales.API.Infrastructure.Dto
{
    public class OrderDto
    {
        public int OrderId { get; set; }
        public int? CustomerId { get; set; }
        public byte OrderStatus { get; set; }
        public DateTime OrderDate { get; set; }
        public int StoreId { get; set; }
        public int StaffId { get; set; }
        public CustomerDto Customer { get; set; }
        public StaffDto Staff { get; set; }
        public StoreDto Store { get; set; }
    }
}

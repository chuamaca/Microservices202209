using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservices.Monitoring.Client.MVC.Infrastructure.Dto
{
    public class ProductDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int BrandId { get; set; }
        public int CategoryId { get; set; }
        public short ModelYear { get; set; }
        public decimal ListPrice { get; set; }

        public virtual BrandDto Brand { get; set; }
        public virtual CategoryDto Category { get; set; }
        public virtual ICollection<StockDto> Stocks { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservices.Sales.API.DataTransferObjects.Commands.Orders
{
    public class PayForOrderCommand
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public int Version { get; set; }
    }
}

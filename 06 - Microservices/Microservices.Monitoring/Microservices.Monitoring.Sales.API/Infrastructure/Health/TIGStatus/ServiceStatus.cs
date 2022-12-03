using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservices.Monitoring.Sales.API.Infrastructure.Health.TIGStatus
{

    public class ServiceStatus
    {
        public string Service { get; set; }
        public int Status { get; set; }
    }
}

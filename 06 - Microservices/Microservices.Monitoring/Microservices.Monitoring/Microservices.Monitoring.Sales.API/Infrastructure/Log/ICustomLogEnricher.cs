using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservices.Monitoring.Sales.API.Infrastructure.Log
{
    public interface ICustomLogEnricher
    {
        string CorrelationId { get; set; }
    }
}

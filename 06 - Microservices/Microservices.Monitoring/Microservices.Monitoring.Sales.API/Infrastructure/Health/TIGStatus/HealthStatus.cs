using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservices.Monitoring.Sales.API.Infrastructure.Health.TIGStatus
{

    public enum HealthStatus
    {
        //    
        // Summary:    
        //     Indicates that the health check determined that the component was unhealthy,    
        //     or an unhandled exception was thrown while executing the health check.    
        Unhealthy = 0,
        //    
        // Summary:    
        //     Indicates that the health check determined that the component was in a degraded    
        //     state.    
        Degraded = 1,
        //    
        // Summary:    
        //     Indicates that the health check determined that the component was healthy.    
        Healthy = 2
    }
}

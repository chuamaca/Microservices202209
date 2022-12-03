using System;
using System.Collections.Generic;
using System.Text;

namespace Microservices.Monitoring.TIG.Worker
{
    public class HealthCheckResult
    {
        public string Service { get; set; }
        public int Status { get; set; }
    }
}

using Serilog;
using Serilog.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservices.Monitoring.Products.API.Infrastructure.Log
{
    public static class LoggingExtensions
    {
        public static LoggerConfiguration CustomLogEnricher(this LoggerEnrichmentConfiguration enrich)
        {
            if(enrich == null)
            {
                throw new ArgumentException(nameof(enrich));
            }

            return enrich.With<CustomLogEnricher>();
        }
    }
}

using Microsoft.AspNetCore.Http;
using Serilog.Core;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Microservices.Monitoring.Products.API.Infrastructure.Log
{
    public class CustomLogEnricher: ILogEventEnricher, ICustomLogEnricher
    {
        public string CorrelationId { get; set; }
        public static IServiceProvider ServiceProvider { get; set; }

        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            if(!(ServiceProvider?.GetService<IHttpContextAccessor>()?.HttpContext is HttpContext httpContext))
            {
                return;
            }

            var headers = httpContext.Request.Headers;
            CorrelationId = headers["CorrelationId"];

            logEvent.AddOrUpdateProperty(new LogEventProperty("CorrelationId", new ScalarValue(CorrelationId)));
        }
    }
}

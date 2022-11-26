using Microservices.Resilient.Hystrix.Client.MVC.Infrastructure.Data.Entities;
using Microsoft.Extensions.Logging;
using Steeltoe.CircuitBreaker.Hystrix;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservices.Resilient.Hystrix.Client.MVC.Infrastructure.Agents.Orders.Commands
{
    public class ListCommand: HystrixCommand<List<Order>>
    {
        private IOrderClient _orderClient;
        private ILogger<ListCommand> _logger;

        public ListCommand(IHystrixCommandOptions options, IOrderClient orderClient, ILogger<ListCommand> logger) : base(options)
        {
            _orderClient = orderClient;
            _logger = logger;
            IsFallbackUserDefined = true;
        }

        //public Task<List<Order>> ExecuteAsync()
        //{
        //    return base.ExecuteAsync();
        //}
        protected override async Task<List<Order>> RunAsync()
        {
            return await _orderClient.List();
        }

        protected override Task<List<Order>> RunFallbackAsync()
        {
            var circuitBreakerStatus = IsCircuitBreakerOpen ? bool.TrueString : bool.FalseString;
            _logger.LogCritical($"Circuit breaker status: {circuitBreakerStatus}");
            if (!IsCircuitBreakerOpen && IsFailedExecution)
            {
                _logger.LogCritical(FailedExecutionException, FailedExecutionException.Message);
            }
            return _orderClient.ListFallback();
        }
    }
}

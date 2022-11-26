using Microservices.Resilient.Hystrix.Client.MVC.Infrastructure.Data.Entities;
using Microsoft.Extensions.Logging;
using Steeltoe.CircuitBreaker.Hystrix;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservices.Resilient.Hystrix.Client.MVC.Infrastructure.Agents.Orders.Commands
{
    public class GetCommand: HystrixCommand<Order>
    {
        private IOrderClient _orderClient;
        private ILogger<GetCommand> _logger;
        private int _orderId;

        public GetCommand(IHystrixCommandOptions options, IOrderClient orderClient, ILogger<GetCommand> logger) : base(options)
        {
            _orderClient = orderClient;
            _logger = logger;
            IsFallbackUserDefined = true;
        }

        public Task<Order> ExecuteAsync(int orderId)
        {
            _orderId = orderId;
            return base.ExecuteAsync();
        }
        protected override async Task<Order> RunAsync()
        {
            return await _orderClient.Get(_orderId);
        }

        protected override Task<Order> RunFallbackAsync()
        {
            var circuitBreakerStatus = IsCircuitBreakerOpen ? bool.TrueString : bool.FalseString;
            _logger.LogCritical($"Circuit breaker status: {circuitBreakerStatus}");
            if (!IsCircuitBreakerOpen && IsFailedExecution)
            {
                _logger.LogCritical(FailedExecutionException, FailedExecutionException.Message);
            }
            return _orderClient.GetFallback();
        }
    }
}

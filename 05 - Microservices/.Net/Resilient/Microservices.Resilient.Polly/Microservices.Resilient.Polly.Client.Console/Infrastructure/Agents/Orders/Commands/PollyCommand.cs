using Polly;
using Polly.Retry;
using Polly.Bulkhead;
using Polly.CircuitBreaker;
using Polly.Fallback;
using System.Collections.Generic;
using System.Net.Http;

namespace Microservices.Resilient.Polly.Client.Console.Infrastructure.Agents.Orders.Commands
{    
    using System;
    public class PollyCommand<T>
    {
        private int maxRetryCount = 6;
        private double circuitBreakDurationSeconds = 0.2 /* experiment with effect of shorter or longer here, eg: change to = 1, and the fallbackForCircuitBreaker is correctly invoked */ ;
        private int maxExceptionsBeforeBreaking = 4; /* experiment with effect of fewer here, eg change to = 1, and the fallbackForCircuitBreaker is correctly invoked */
        private int maxParallelizations = 2;
        private int maxQueuingActions = 2;        
        private Policy<T> _policyWrap;

        public PollyCommand()
        {
            BuildPolicies();
        }
        private RetryPolicy BuildRetryPolicy()
        {
            RetryPolicy retryPolicy = Policy.Handle<Exception>(e => (e is HttpRequestException || (/*!(e is BrokenCircuitException) &&*/ e.InnerException is HttpRequestException))) // experiment with introducing the extra (!(e is BrokenCircuitException) && ) clause here, if necessary/desired, depending on goal
            .WaitAndRetry(
                retryCount: maxRetryCount,
                sleepDurationProvider: attempt => TimeSpan.FromMilliseconds(50 * Math.Pow(2, attempt)),
                onRetry: (ex, calculatedWaitDuration, retryCount, context) =>
                {
                    Console.WriteLine(String.Format("Retry => Count: {0}, Wait duration: {1}, Policy Wrap: {2}, Policy: {3}, Endpoint: {4}, Exception: {5}", retryCount, calculatedWaitDuration, context.PolicyWrapKey, context.PolicyKey, context.OperationKey, ex.Message));
                });

            return retryPolicy;
        }

        private CircuitBreakerPolicy BuildCircuitBreakerPolicy()
        {
            CircuitBreakerPolicy circuitBreakerPolicy = Policy.Handle<Exception>(e => (e is HttpRequestException || e.InnerException is HttpRequestException))
            .CircuitBreaker(maxExceptionsBeforeBreaking,
                TimeSpan.FromSeconds(circuitBreakDurationSeconds),
                onBreak: (ex, breakDuration) =>
                {
                    Console.WriteLine(String.Format("Circuit breaking for {0} ms due to {1}", breakDuration.TotalMilliseconds, ex.Message));
                },
                onReset: () =>
                {
                    Console.WriteLine("Circuit closed again.");
                },
                onHalfOpen: () => { Console.WriteLine("Half open."); });

            return circuitBreakerPolicy;
        }

        private BulkheadPolicy BuildBulkheadPolicy()
        {
            var bulkheadPolicy = Policy.Bulkhead(maxParallelizations, maxQueuingActions);

            return bulkheadPolicy;
        }

        private FallbackPolicy<T> BuildFallbackForCircuitBreakerPolicy(Func<Context,T> onFallback)
        {
            FallbackPolicy<T> fallbackForCircuitBreaker = Policy<T>
                 .Handle<BrokenCircuitException>()
                 /* .OrInner<BrokenCircuitException>() */ // Consider this if necessary.
                 /* .Or<Exception>(e => circuitBreaker.State != CircuitState.Closed) */ // This check will also detect the circuit in anything but healthy state, regardless of the final exception thrown.
                 .Fallback<T>(fallbackAction: (context) => { return onFallback(context); },
                 onFallback: (b, context) =>
                 {
                     Console.WriteLine(String.Format("Operation attempted on broken circuit => Policy Wrap: {0}, Policy: {1}, Endpoint: {2}", context.PolicyWrapKey, context.PolicyKey, context.OperationKey));
                 });

            return fallbackForCircuitBreaker;
        }

        private FallbackPolicy<T> BuildFallbackForAnyExceptionPolicy(Func<Context, T> onFallback)
        {
            FallbackPolicy<T> fallbackForAnyException = Policy<T>
                    .Handle<Exception>()
                    .Fallback<T>(fallbackAction: (context) => { return onFallback(context); },
                    onFallback: (e, context) =>
                    {
                        Console.WriteLine(String.Format("An unexpected error occured => Policy Wrap: {0}, Policy: {1}, Endpoint: {2}, Exception: {3}", context.PolicyWrapKey, context.PolicyKey, context.OperationKey, e.Exception.Message));
                    });

            return fallbackForAnyException;
        }

        private void BuildPolicies()
        {
            var retryPolicy = BuildRetryPolicy();
            var circuitBreakerPolicy = BuildRetryPolicy();
            var bulkheadPolicy = BuildBulkheadPolicy();
            var fallbackForAnyException = BuildFallbackForAnyExceptionPolicy(RunFallbackForAnyException);
            var fallbackForCircuitBreaker = BuildFallbackForCircuitBreakerPolicy(RunFallback);

            var resilienceStrategy = Policy.Wrap(retryPolicy, circuitBreakerPolicy, bulkheadPolicy);
            _policyWrap = fallbackForAnyException.Wrap(fallbackForCircuitBreaker.Wrap(resilienceStrategy));            
        }

        public T Execute()
        {
            return _policyWrap.Execute((context) => Run(), new Context("some endpoint info"));            
        }

        protected virtual T Run()
        {
            return default(T);
        }

        protected virtual T RunFallback(Context context)
        {            
            return default(T);
        }

        protected virtual T RunFallbackForAnyException(Context context)
        {
            return default(T);
        }
    }
}

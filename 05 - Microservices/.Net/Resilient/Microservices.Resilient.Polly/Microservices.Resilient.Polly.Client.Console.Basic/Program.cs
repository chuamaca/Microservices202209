using Polly;
using Polly.CircuitBreaker;
using Polly.Retry;
using System.Text.Json;

namespace Microservices.Resilient.Polly.Client.Console.Basic;

using Microservices.Resilient.Polly.Client.Console.Basic.Infrastructure.Data.Entities;
using System;
using System.Collections.Generic;
using System.Net.Http;


class Program
{
    private static AsyncCircuitBreakerPolicy _circuitBreakerPolicy;
    private static AsyncRetryPolicy _retryPolicy;
    static async Task Main(string[] args)
    {
        _retryPolicy = Policy
            .Handle<Exception>()
            .WaitAndRetryAsync(2, retryAttempt =>
            {
                var timeToWait = TimeSpan.FromSeconds(Math.Pow(2, retryAttempt));
                Console.WriteLine($"Waiting {timeToWait.TotalSeconds} seconds");
                return timeToWait;
            });

        _circuitBreakerPolicy = Policy
            .Handle<Exception>()
            .CircuitBreakerAsync(1, TimeSpan.FromSeconds(10), OnBreak, OnReset, OnHalfOpen);


        var apiClient = new HttpClient();
        int i = 0;

        Console.WriteLine("Microservices.Resilient.Polly.Client.Console.Basic");
        Console.WriteLine("==================================================");

        while (true)
        {
            i++;
            Console.WriteLine($"{i}. Start calling to Web API");
            Console.WriteLine("\n");
            Console.WriteLine("-------------------------------------------------------------------------------------------");

            var apiResponse = new HttpResponseMessage();

            try
            {
                //apiResponse = await _circuitBreakerPolicy.ExecuteAsync(
                //    ()=>apiClient.GetAsync("https://localhost:44312/api/orders/", HttpCompletionOption.ResponseContentRead)
                //);

                //apiResponse = await _circuitBreakerPolicy.ExecuteAsync(
                //    () => _retryPolicy.ExecuteAsync(
                //        async () => await apiClient.GetAsync("https://localhost:44312/api/orders/", HttpCompletionOption.ResponseContentRead)
                //        )
                //);

                var json =
                    await _circuitBreakerPolicy.ExecuteAsync(
                         () =>
                        _retryPolicy.ExecuteAsync(
                        async () =>
                        {
                            var result = await apiClient.GetAsync("https://localhost:44312/api/orders/", HttpCompletionOption.ResponseContentRead);
                            var json = await result.Content.ReadAsStringAsync();
                            JsonSerializer.Deserialize<List<Order>>(json);

                            return result;
                        }
                        ));

                //var json = await apiResponse.Content.ReadAsStringAsync();

                Console.WriteLine($"Http Status Code: {apiResponse.StatusCode}");
                Console.WriteLine("\n");
                Console.WriteLine($"Response: {json}");
                Console.WriteLine("\n");
                Console.WriteLine($"{i}. End calling to Web API");
                Console.WriteLine("\n");
                Console.WriteLine("-------------------------------------------------------------------------------------------");
                Console.WriteLine("Type any key and press Enter to make new calling to Web API");
                Console.WriteLine("-------------------------------------------------------------------------------------------");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
        }
    }

    private static void OnHalfOpen()
    {
        Console.WriteLine("Connection half open - Circuit Breaker state is HALF-OPEN");
    }

    private static void OnReset()
    {
        Console.WriteLine("Connection reset - Circuit Breaker state is CLOSED");
    }

    private static void OnBreak(Exception exception, TimeSpan timeSpan)
    {
        Console.WriteLine("Connection is Closed - Circuit Breaker state is OPEN");
    }
}

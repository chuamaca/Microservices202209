using Microservices.Resilient.Polly.Client.Console.Infrastructure.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Microservices.Resilient.Polly.Client.Console.Infrastructure.Agents.Orders
{
    public class OrderClient : IOrderClient
    {
        private string API_URL = "https://localhost:44312/api/orders/";

        public async Task<List<Order>> List()
        {
            var Client = GetClient();
            var result = await Client.GetStringAsync(API_URL);
            var orders = JsonSerializer.Deserialize<List<Order>>(result);
            return orders;
        }

        public async Task<List<Order>> ListFallback()
        {
            List<Order> orders = new List<Order> { new Order { OrderId = 0, Description = "From Fallback" } };
            return await Task.FromResult(orders);
        }
        public async Task<Order> Get(int orderId)
        {
            Random rnd = new Random();
            int number = rnd.Next(1, 13);
            if (number % 2 == 0)
            {
                throw new Exception("error on services");
            }

            var Client = GetClient();
            var result = await Client.GetStringAsync(API_URL + orderId);
            var order = JsonSerializer.Deserialize<Order>(result);
            return order;
        }

        public async Task<Order> GetFallback()
        {
            Order order = new Order { OrderId = 0, Description = "From Fallback" };
            return await Task.FromResult(order);
        }

        private HttpClient GetClient()
        {
            var Client = new HttpClient();
            return Client;
        }
    }
}

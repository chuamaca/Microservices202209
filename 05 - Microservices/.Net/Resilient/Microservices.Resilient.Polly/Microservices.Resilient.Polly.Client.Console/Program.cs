using Microservices.Resilient.Polly.Client.Console.Infrastructure.Agents.Orders;
using Microservices.Resilient.Polly.Client.Console.Infrastructure.Agents.Orders.Commands;
using Microservices.Resilient.Polly.Client.Console.Infrastructure.Data.Entities;
using System.Collections.Generic;
using System.Text.Json;

namespace Microservices.Resilient.Polly.Client.Console
{
    using System;
    using System.Threading.Tasks;

    class Program
    {
        private static IOrdersAgent _ordersAgent;
        private static ListCommand _listCommand;
        private static GetCommand _getCommand;
        private static IOrderClient _orderClient;
        static async Task Main()
        {
            _orderClient = new OrderClient();
            _listCommand = new ListCommand(_orderClient);
            _getCommand = new GetCommand(_orderClient);
            _ordersAgent = new OrdersAgent(_listCommand, _getCommand);
            int i = 0;

            Console.WriteLine("Microservices.Resilient.Polly.Client.Console");
            Console.WriteLine("============================================");

            while (true)
            {
                i++;
                List<Order> orders = _ordersAgent.List();
                string json = JsonSerializer.Serialize(orders);

                Console.WriteLine($"{i}. Start calling to Web API");
                Console.WriteLine("\n");
                Console.WriteLine("-------------------------------------------------------------------------------------------");
                Console.WriteLine($"Response: {json}");
                Console.WriteLine("\n");
                Console.WriteLine($"{i}. End calling to Web API");
                Console.WriteLine("\n");
                Console.WriteLine("-------------------------------------------------------------------------------------------");
                Console.WriteLine("Type any key and press Enter to make new calling to Web API");
                Console.WriteLine("-------------------------------------------------------------------------------------------");
                Console.ReadLine();
            }
        }
    }
}

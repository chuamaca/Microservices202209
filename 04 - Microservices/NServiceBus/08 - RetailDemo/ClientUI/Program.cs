using Messages.Commands;
using NServiceBus;
using NServiceBus.Logging;
using System;
using System.Threading.Tasks;

namespace ClientUI
{
    public class Program
    {
        static ILog log = LogManager.GetLogger<Program>();
        static void Main(string[] args)
        {
            AsyncMain().GetAwaiter().GetResult();
        }

        static async Task AsyncMain()
        {
            Console.Title = "Rabbit.ClientUI";
            var endpointConfiguration = new EndpointConfiguration("Rabbit.ClientUI");

            /*
            var transport = endpointConfiguration.UseTransport<LearningTransport>();
            */
            var transport = endpointConfiguration.UseTransport<RabbitMQTransport>();
            transport.ConnectionString("host=localhost;username=test;password=test");
            //transport.UsePublisherConfirms(true);            
            transport.UseConventionalRoutingTopology(QueueType.Quorum);

            var routing = transport.Routing();
            routing.RouteToEndpoint(typeof(PlaceOrder), "Rabbit.Sales");

            endpointConfiguration.EnableInstallers();
            var endpointInstance = await Endpoint.Start(endpointConfiguration).ConfigureAwait(false);

            await RunLoop(endpointInstance).ConfigureAwait(false);

            //Console.WriteLine("Press Enter to exit...");
            //Console.ReadLine();

            await endpointInstance.Stop().ConfigureAwait(false);
        }

        static async Task RunLoop(IEndpointInstance endpointInstance)
        {
            while (true)
            {
                log.Info("Press 'P' to place an order, or 'Q' to quit.");
                var key = Console.ReadKey();
                Console.WriteLine();

                switch (key.Key)
                {
                    case ConsoleKey.P:
                        // Instantiate the command
                        var command = new PlaceOrder
                        {
                            OrderId = Guid.NewGuid().ToString()
                        };

                        // Send the command to the local endpoint
                        log.Info($"Sending PlaceOrder command, OrderId = {command.OrderId}");
                        await endpointInstance.Send(command)
                            .ConfigureAwait(false);

                        break;

                    case ConsoleKey.Q:
                        return;

                    default:
                        log.Info("Unknown input. Please try again.");
                        break;
                }
            }
        }
    }
}

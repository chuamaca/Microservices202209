using NServiceBus;
using System;
using System.Threading.Tasks;

namespace Shipping
{
    class Program
    {
        static async Task Main()
        {
            Console.Title = "Rabbit.Shipping";

            var endpointConfiguration = new EndpointConfiguration("Rabbit.Shipping");

            //var transport = endpointConfiguration.UseTransport<LearningTransport>();
            var persistence = endpointConfiguration.UsePersistence<LearningPersistence>();

            var transport = endpointConfiguration.UseTransport<RabbitMQTransport>();
            transport.ConnectionString("host=localhost;username=test;password=test");
            //transport.UsePublisherConfirms(true);            
            transport.UseConventionalRoutingTopology(QueueType.Quorum);

            endpointConfiguration.EnableInstallers();
            var endpointInstance = await Endpoint.Start(endpointConfiguration)
                .ConfigureAwait(false);

            Console.WriteLine("Press Enter to exit.");
            Console.ReadLine();

            await endpointInstance.Stop()
                .ConfigureAwait(false);
        }
    }
}

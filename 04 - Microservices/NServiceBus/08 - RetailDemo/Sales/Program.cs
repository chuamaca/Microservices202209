using NServiceBus;
using System;
using System.Threading.Tasks;

namespace Sales
{
    public class Program
    {
        static async Task Main()
        {
            Console.Title = "Rabbit.Sales";

            var endpointConfiguration = new EndpointConfiguration("Rabbit.Sales");

            //var transport = endpointConfiguration.UseTransport<LearningTransport>();
            var transport = endpointConfiguration.UseTransport<RabbitMQTransport>();
            transport.ConnectionString("host=localhost;username=test;password=test");
            //transport.UsePublisherConfirms(true);            
            transport.UseConventionalRoutingTopology(QueueType.Quorum);

            //#region NoDelayedRetries
            //var recoverability = endpointConfiguration.Recoverability();
            //recoverability.Delayed(delayed => delayed.NumberOfRetries(0));
            //#endregion

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

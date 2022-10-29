using NServiceBus;
using System;
using System.Threading.Tasks;

namespace Sales
{
    class Program
    {
        static async Task Main()
        {
            Console.Title = "Sales";

            var endpointConfiguration = new EndpointConfiguration("Sales");

            var transport = endpointConfiguration.UseTransport<LearningTransport>();

            //var recoverability = endpointConfiguration.Recoverability();
            //recoverability.Immediate(
            //    immediate =>
            //    {
            //        immediate.NumberOfRetries(3);
            //    });

            var endpointInstance = await Endpoint.Start(endpointConfiguration)
                .ConfigureAwait(false);

            Console.WriteLine("Press Enter to exit.");
            Console.ReadLine();

            await endpointInstance.Stop()
                .ConfigureAwait(false);
        }
    }
}

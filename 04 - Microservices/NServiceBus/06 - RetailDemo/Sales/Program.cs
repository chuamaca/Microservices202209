﻿using NServiceBus;
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

            #region NoDelayedRetries
            var recoverability = endpointConfiguration.Recoverability();
            recoverability.Delayed(delayed => delayed.NumberOfRetries(0));
            #endregion
            
            var endpointInstance = await Endpoint.Start(endpointConfiguration)
                .ConfigureAwait(false);

            Console.WriteLine("Press Enter to exit.");
            Console.ReadLine();

            await endpointInstance.Stop()
                .ConfigureAwait(false);
        }
    }
}

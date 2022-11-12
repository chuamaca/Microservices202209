using NServiceBus;
using NServiceBus.Persistence.Sql;
using System;
using System.Data.SqlClient;
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
            //var persistence = endpointConfiguration.UsePersistence<LearningPersistence>();

            var connection = @"Data Source=.; Initial Catalog=NServiceBusSaga;User ID=sa;Password=Password1234;";
            var persistence = endpointConfiguration.UsePersistence<SqlPersistence>();
            var subscriptions = persistence.SubscriptionSettings();
            subscriptions.CacheFor(TimeSpan.FromMinutes(1));
            persistence.SqlDialect<SqlDialect.MsSqlServer>();
            persistence.ConnectionBuilder(
                connectionBuilder: () =>
                {
                    return new SqlConnection(connection);
                });


            var transport = endpointConfiguration.UseTransport<RabbitMQTransport>();
            transport.ConnectionString("host=localhost;username=test;password=test");
            //transport.UsePublisherConfirms(true);
            transport.UseDirectRoutingTopology(QueueType.Quorum);

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

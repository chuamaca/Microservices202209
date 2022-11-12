
using Commands;
using NServiceBus;
using NServiceBus.Logging;
using System.Net;

namespace ClientUI;
public class Program
{
    static ILog log = LogManager.GetLogger<Program>();
    static void Main(string[] args)
    {
        AsyncMain().GetAwaiter().GetResult();
    }

    static async Task AsyncMain()
    {
        Console.Title = "ClientUI";
        var endpointConfiguration = new EndpointConfiguration("HandleMessages"); //Nombre de referencia no es necesario que tenga el nombre de algun ensamblado

        //Esta configuración define el transporte que NServiceBus usará para enviar y recibir mensajes.
        //Estamos utilizando el transporte de aprendizaje, que se incluye en la biblioteca principal de NServiceBus 
        //como un transporte de inicio para aprender a usar NServiceBus sin dependencias adicionales.
        //Todos los demás transportes se proporcionan mediante diferentes paquetes NuGet.
        var transport = endpointConfiguration.UseTransport<LearningTransport>();


        var endpointInstance = await Endpoint.Start(endpointConfiguration).ConfigureAwait(false);

        await RunLoop(endpointInstance).ConfigureAwait(false);

        await endpointInstance.Stop().ConfigureAwait(false);
    }

    static async Task RunLoop(IEndpointInstance endpointInstance)
    {
        while (true)
        {
            Console.WriteLine();
            log.Info("Press (S)imple, (C)omplex, (O)rder,(Q)uit.");
            var key = Console.ReadKey();
            Console.WriteLine();

            switch (key.Key)
            {
                case ConsoleKey.S:
                    var simpleCommand = new DoSomethingCommand
                    {
                        SomeProperty = "SomeProperty"
                    };

                    log.Info($"Sending Simple command, SomeProperty = {simpleCommand.SomeProperty}");
                    await endpointInstance.SendLocal(simpleCommand).ConfigureAwait(false);

                    break;

                case ConsoleKey.C:
                    var complexCommand = new DoSomethingComplexCommand
                    {
                        SomeId = 1,
                        ChildStuff = new ChildClass { SomeProperty = "Complex SomeProperty" },
                        ListOfStuff = new List<ChildClass> { new ChildClass { SomeProperty = "Item Complex SomeProperty" } }
                    };

                    log.Info($"Sending Comlex command, ChildStuff.SomeProperty = {complexCommand.ChildStuff.SomeProperty}");
                    await endpointInstance.SendLocal(complexCommand).ConfigureAwait(false);

                    break;

                case ConsoleKey.O:
                    // Instantiate the command
                    var command = new PlaceOrderCommand
                    {
                        OrderId = Guid.NewGuid().ToString()
                    };

                    // Send the command to the local endpoint
                    log.Info($"Sending PlaceOrder command, OrderId = {command.OrderId}");
                    await endpointInstance.SendLocal(command)
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
using Microservices.Infrastructure.Crosscutting;

namespace Microservices.Infrastructure.MessageBus
{
    public interface IMessageBus
    {
        void Publish<T>(T @event) where T : Event;
        void Send<T>(T command) where T : class, ICommand;
    }
}
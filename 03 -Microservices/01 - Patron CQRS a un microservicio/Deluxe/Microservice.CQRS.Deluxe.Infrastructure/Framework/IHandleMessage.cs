namespace Microservice.CQRS.Deluxe.Infrastructure.Framework
{
    public interface IHandleMessage<in T>
    {
        void Handle(T message);
    }
}
namespace Microservices.Infrastructure.Crosscutting
{
    public interface IHandle<in T> where T:Event
    {
    }
}
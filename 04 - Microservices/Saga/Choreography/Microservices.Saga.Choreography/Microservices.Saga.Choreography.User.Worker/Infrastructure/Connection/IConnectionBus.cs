using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservices.Saga.Choreography.User.Worker.Infrastructure.Connection
{
    public interface IConnectionBus
    {
        void PublishMessage(string queue, byte[] message);
        void Received(string queue, Action<string> action);
    }
}

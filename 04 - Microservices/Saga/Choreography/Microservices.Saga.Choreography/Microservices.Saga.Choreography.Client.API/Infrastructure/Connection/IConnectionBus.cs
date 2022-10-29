using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservices.Saga.Choreography.Client.API.Infrastructure.Connection
{
    public interface IConnectionBus
    {
        void PublishMessage(string queue, byte[] message);
    }
}

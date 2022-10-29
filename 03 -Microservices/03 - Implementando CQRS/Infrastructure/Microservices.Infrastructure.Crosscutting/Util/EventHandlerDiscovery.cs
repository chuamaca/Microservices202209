using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Microservices.Infrastructure.Crosscutting.Util
{
    public class EventHandlerDiscovery
    {
        public Dictionary<string, EventHandlerData> Handlers
        {
            get; private set;
        }

        public EventHandlerDiscovery()
        {
            Handlers = new Dictionary<string, EventHandlerData>();
        }

        public EventHandlerDiscovery Scan(Aggregate aggregate)
        {
            var handlerInterface = typeof(IHandle<>);
            var aggType = aggregate.GetType();

            var interfaces = aggType.GetInterfaces();

            var instances = from i in aggType.GetInterfaces()
                            where (i.IsGenericType && handlerInterface.IsAssignableFrom(i.GetGenericTypeDefinition()))
                            select i.GenericTypeArguments[0];

            foreach (var i in instances)
            {
                Handlers.Add(i.Name, new EventHandlerData
                {
                    TypeParameter = i,
                    AggregateHandler = aggregate
                });
            }

            return this;
        }
    }

    public class EventHandlerData
    {
        public Type TypeParameter { get; set; }
        public Aggregate AggregateHandler { get; set; }
    }
}

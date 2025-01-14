﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microservice.CQRS.Deluxe.Infrastructure.Framework.EventStore;
using Microsoft.AspNetCore.Http;

namespace Microservice.CQRS.Deluxe.Infrastructure.Framework
{
    public class InMemoryBus : IBus
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public IEventStore EventStore { get; private set; }

        private readonly IDictionary<Type, Type> RegisteredSagas = new Dictionary<Type, Type>();
        private readonly IList<Type> RegisteredHandlers = new List<Type>();

        public InMemoryBus(IEventStore eventStore, IHttpContextAccessor httpContextAccessor)
        {
            //if (eventStore == null)
            //{
            //    throw new ArgumentNullException("eventStore");
            //}
            EventStore = eventStore;
            _httpContextAccessor = httpContextAccessor;
        }


        #region IBus
        void IBus.RegisterSaga<T>()  
        {
            var sagaType = typeof(T);
            if (sagaType.GetInterfaces().Count(i => i.Name.StartsWith(typeof(IStartWithMessage<>).Name)) != 1)
            {
                throw new InvalidOperationException("The specified saga must implement the IStartWithMessage<T> interface.");
            }
            var messageType = sagaType.
                GetInterfaces().First(i => i.Name.StartsWith(typeof(IStartWithMessage<>).Name)).
                GenericTypeArguments.
                First();
            RegisteredSagas.Add(messageType, sagaType);
        }
        void IBus.RegisterHandler<T>()
        {
            RegisteredHandlers.Add(typeof(T));
        }

        void IBus.Send<T>(T message)
        {
            SendInternal(message);
        }
        void IBus.RaiseEvent<T>(T theEvent)  
        {
            if (EventStore != null)
                EventStore.Save(theEvent);
            SendInternal(theEvent);
        }
        #endregion


        #region Private Members
        private void SendInternal<T>(T message) where T : Message
        {
            LaunchSagasThatStartWithMessage(message);
            DeliverMessageToRunningSagas(message);
            DeliverMessageToRegisteredHandlers(message);

            // Saga and handlers are similar things. Handlers are  one-off event handlers
            // whereas saga may be persisted and survive sessions, wait for more messages and so forth.
            // Saga are mostly complex workflows; handlers are plain one-off event handlers.
        }

        private void LaunchSagasThatStartWithMessage<T>(T message) where T : Message
        {
            var messageType = message.GetType();
            var openInterface = typeof(IStartWithMessage<>);
            var closedInterface = openInterface.MakeGenericType(messageType);
            var sagasToLaunch = from s in RegisteredSagas.Values
                                 where closedInterface.IsAssignableFrom(s)
                                 select s;
            foreach (var s in sagasToLaunch)
            {
                dynamic sagaInstance = Activator.CreateInstance(s, this, EventStore, _httpContextAccessor);
                sagaInstance.Handle(message);
            }
        }

        private void DeliverMessageToRunningSagas<T>(T message) where T : Message
        {
            var messageType = message.GetType();
            var openInterface = typeof(IHandleMessage<>);
            var closedInterface = openInterface.MakeGenericType(messageType);
            var sagasToNotify = from s in RegisteredSagas.Values
                                where closedInterface.IsAssignableFrom(s)
                                select s;
            foreach (var s in sagasToNotify)
            {
                dynamic sagaInstance = Activator.CreateInstance(s, this, EventStore, _httpContextAccessor);
                sagaInstance.Handle(message);
            }
        }
        
        private void DeliverMessageToRegisteredHandlers<T>(T message) where T : Message
        {
            var messageType = message.GetType();
            var openInterface = typeof(IHandleMessage<>);
            var closedInterface = openInterface.MakeGenericType(messageType);
            var handlersToNotify = from h in RegisteredHandlers
                                   where closedInterface.IsAssignableFrom(h)
                                   select h;
            foreach (var h in handlersToNotify)
            {
                dynamic sagaInstance = Activator.CreateInstance(h, EventStore,_httpContextAccessor);     // default ctor is enough
                sagaInstance.Handle(message);
            }
        }
        #endregion
    }
}
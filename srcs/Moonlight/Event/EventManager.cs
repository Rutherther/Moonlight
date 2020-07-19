using System;
using System.Collections.Generic;
using System.Linq;
using Moonlight.Extensions;

namespace Moonlight.Event
{
    internal class EventManager : IEventManager
    {
        private readonly Dictionary<Type, List<ListenerData>> _handlers;

        public EventManager() => _handlers = new Dictionary<Type, List<ListenerData>>();

        public void Emit<T>(T notification) where T : IEventNotification
        {
            List<ListenerData> handlers = _handlers.GetValueOrDefault(typeof(T));
            if (handlers == null)
            {
                return;
            }

            handlers.ForEach(x => x.Listener.Handle(notification));
            handlers.RemoveAll(x => x.Once);
        }

        public void RegisterListener<T>(EventListener<T> listener, bool once = false) where T : IEventNotification
        {
            Type type = typeof(T);
            List<ListenerData> handlers = _handlers.GetValueOrDefault(type);
            if (handlers == null)
            {
                handlers = new List<ListenerData>();
                _handlers[type] = handlers;
            }

            handlers.Add(new ListenerData(listener, once));
        }

        public void RemoveListener<T>(EventListener<T> listener) where T : IEventNotification
        {
            Type type = typeof(T);
            List<ListenerData> handlers = _handlers.GetValueOrDefault(type);

            if (handlers == null)
            {
                return;
            }

            handlers.RemoveAll(x => x.Listener == listener);
        }

        public void RegisterOnceListener<T>(EventListener<T> listener) where T : IEventNotification
        {
            RegisterListener(listener, true);
        }

        private class ListenerData
        {
            public ListenerData(IEventListener listener, bool once = false)
            {
                Listener = listener;
                Once = once;
            }
            
            public bool Once { get; set; }
            
            public IEventListener Listener { get; set; }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TMN
{
    public static class EventManager
    {
        private static readonly EventHub EventHub = new EventHub();

        public static T Get<T>() where T : IEvent,
        new()
        {
            return EventHub.Get<T>();
        }
    }

    public class EventHub
    {
        private Dictionary<Type, IEvent> _events = new Dictionary<Type, IEvent>();

        public T Get<T>() where T : IEvent,
        new()
        {
            var eType = typeof(T);
            if (_events.TryGetValue(eType, out var eventToReturn))
                return (T)eventToReturn;

            eventToReturn = (AEvent)Activator.CreateInstance(eType);
            _events.Add(eType, eventToReturn);
            return (T)eventToReturn;
        }
    }

    public interface IEvent
    {
    }

    public abstract class AEvent : IEvent
    {
        private Action _callback;
        private readonly Dictionary<GameObject, Action> _subscriberHandlerDictionary = new Dictionary<GameObject, Action>();

        public virtual void AddListener(Action handler, GameObject subscriber = null)
        {
            if (subscriber == null) _callback += handler;
            else
            {
                if (_subscriberHandlerDictionary.ContainsKey(subscriber)) return;
                _subscriberHandlerDictionary.Add(subscriber, handler);
                _callback += handler;
            }
        }

        public void RemoveListener(Action handler, GameObject subscriber = null)
        {
            if (subscriber == null) _callback -= handler;
            else
            {
                if (_subscriberHandlerDictionary.ContainsKey(subscriber))
                {
                    _subscriberHandlerDictionary.Remove(subscriber);
                    _callback -= handler;
                }
            }
        }

        public void Execute()
        {
            var subscribersToRemove = _subscriberHandlerDictionary.Where(x => x.Key == null || !x.Key.activeInHierarchy).ToArray();
            foreach (var subscriber in subscribersToRemove)
            {
                _callback -= _subscriberHandlerDictionary[subscriber.Key];
                _subscriberHandlerDictionary.Remove(subscriber.Key);
            }

            _callback?.Invoke();
        }
    }

    public abstract class AEvent<T1> : AEvent
    {
        private Action<T1> _callback;

        public void AddListener(Action<T1> handler)
        {
            _callback += handler;
        }

        public void RemoveListener(Action<T1> handler)
        {
            _callback -= handler;
        }

        public void Execute(T1 arg1)
        {
            _callback?.Invoke(arg1);
        }
    }

    public abstract class AEvent<T1, T2> : AEvent
    {
        private Action<T1, T2> _callback;

        public void AddListener(Action<T1, T2> handler)
        {
            _callback += handler;
        }

        public void RemoveListener(Action<T1, T2> handler)
        {
            _callback -= handler;
        }

        public void Execute(T1 arg1, T2 arg2)
        {
            _callback?.Invoke(arg1, arg2);
        }
    }
}
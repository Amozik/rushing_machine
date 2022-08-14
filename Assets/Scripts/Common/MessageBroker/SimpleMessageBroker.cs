using System;
using System.Collections.Generic;
using System.Reflection;

namespace RushingMachine.Common.MessageBroker
{
    public static class SimpleMessageBroker
    {
        private static readonly Dictionary<string, Dictionary<string, Dictionary<int, SubscriberInfo>>> _messageBrokerMap
            = new Dictionary<string, Dictionary<string, Dictionary<int, SubscriberInfo>>>();
        
        public static void Subscribe<T>(string id, Action<T> callback) => SubscribeInternal<T>(id, callback);

        public static void Subscribe<T>(Action<T> callback) => SubscribeInternal<T>(string.Empty, callback);

        private static void SubscribeInternal<T>(string id, Action<T> callback)
        {
            var typeKey = typeof(T).ToString();
            if (!_messageBrokerMap.ContainsKey(typeKey))
            {
                _messageBrokerMap.Add(typeKey, new Dictionary<string, Dictionary<int, SubscriberInfo>>());
            }

            if (!_messageBrokerMap[typeKey].ContainsKey(id))
            {
                _messageBrokerMap[typeKey].Add(id, new Dictionary<int, SubscriberInfo>());
            }

            var callbackHashCode = callback.GetHashCode();
            if (!_messageBrokerMap[typeKey][id].ContainsKey(callbackHashCode))
            {
                var subData = new SubscriberInfo
                {
                    Target = callback.Target,
                    MethodCallback = callback.Method
                };

                _messageBrokerMap[typeKey][id].Add(callbackHashCode, subData);
            }
        }
        
        public static void Publish<T>(string id, T data) => PublishInternal<T>(id, data);

        public static void Publish<T>(T data) => PublishInternal<T>(string.Empty, data);

        private static void PublishInternal<T>(string id, T data)
        {
            var typeKey = typeof(T).ToString();
            if (!_messageBrokerMap.ContainsKey(typeKey)) return;
            if (!_messageBrokerMap[typeKey].ContainsKey(id)) return;
            
            Dictionary<int, SubscriberInfo> subscribers = _messageBrokerMap[typeKey][id];
            foreach (var item in subscribers.Values)
            {
                item.MethodCallback.Invoke(item.Target, new object[] { data });
            }
        }

        public static void Unsubscribe<T>(string id, Action<T> callback) => UnsubscribeInternal<T>(id, callback);

        public static void Unsubscribe<T>(Action<T> callback) => UnsubscribeInternal<T>(string.Empty, callback);

        private static void UnsubscribeInternal<T>(string id, Action<T> callback)
        {
            var typeKey = typeof(T).ToString();
            if (!_messageBrokerMap.ContainsKey(typeKey)) return;
            if (!_messageBrokerMap[typeKey].ContainsKey(id)) return;
            
            var callbackHashCode = callback.GetHashCode();
            if (_messageBrokerMap[typeKey][id].ContainsKey(callbackHashCode))
            {
                _messageBrokerMap[typeKey][id].Remove(callbackHashCode);
            }
        }
    }

    internal class SubscriberInfo
    {
        public object Target;
        public MethodInfo MethodCallback;
    }
}
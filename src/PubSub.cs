using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

using UnityEngine;

namespace Watermelona
{
    public class PubSub
    {
        private static Dictionary<string, List<object>> subscribers { get; set; }

        static PubSub()
        {
            subscribers = new Dictionary<string, List<object>>();
        }

        public static void Publish(string ch, object data)
        {
            Debug.Log("Publish :: " + ch);

            lock (subscribers)
            {
                var local = subscribers[ch];
                if (local == null)
                    return;

                foreach (var subscriber in local)
                {
                    var type = typeof(Action<>).MakeGenericType(data.GetType());

                    type.InvokeMember(
                        "Invoke",
                        BindingFlags.Public | BindingFlags.Instance | BindingFlags.InvokeMethod,
                        Type.DefaultBinder,
                        subscriber,
                        new object[] { data });
                }
            }
        }
        public static void Publish(object data)
        {
            Publish(data.GetType().Name, data);
        }

        public static void Subscribe<T>(string ch, Action<T> callback)
        {
            Debug.Log("Subscribe<" + typeof(T).Name + "> :: " + ch);

            lock (subscribers)
            {
                if (subscribers.ContainsKey(ch) == false)
                    subscribers[ch] = new List<object>();

                var local = subscribers[ch];

                local.Add(callback);
            }
        }
        public static void Unsubscribe<T>(string ch, Action<T> callback)
        {
            lock (subscribers)
            {
                var local = subscribers[ch];
                if (local == null)
                    return;

                local.Remove(callback);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

using UnityEngine;

namespace Watermelona
{
    public class GameObject : MonoBehaviour
    {
        public virtual void Awake()
        {
            var rt = this.GetType()
                .GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy)
                .Select(x => new
                {
                    attr = (SubscriberAttribute)x.GetCustomAttributes(true)
                        .Where(y => y is SubscriberAttribute)
                        .FirstOrDefault(),
                    method = x
                })
                .Where(x => x.attr != null);

            foreach (var router in rt)
            {
                var paramType = router.method.GetParameters()[0].ParameterType;

                typeof(PubSub).GetMethods()
                    .Where(x => x.Name.Contains("Subscribe"))
                    .FirstOrDefault()
                    .MakeGenericMethod(paramType)
                    .Invoke(null, new object[] {
                        router.attr.type.Name,
                        Delegate.CreateDelegate(
                            typeof(Action<>).MakeGenericType(paramType),
                            this,
                            router.method.Name)
                    });
            }
        }
    }
}

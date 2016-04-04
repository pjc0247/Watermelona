using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Watermelona
{
    public class SubscriberAttribute : Attribute
    {
        public Type type { get; set; }

        public SubscriberAttribute(Type type)
        {
            this.type = type;
        }
    }
}

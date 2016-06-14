using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using DarkRift;
using System.Linq;

namespace CommunicationLayer
{
    public static class PayloadSerializer
    {
        public static DarkRiftWriter Serialize(object payload)
        {
            DarkRiftWriter writer = null;
            using (writer = new DarkRiftWriter())
            {
                var properties = new List<PropertyInfo>(payload.GetType().GetProperties())
                    .OrderBy(x => x.Name);

                foreach (var property in properties)
                {
                    //writer.Write(property.GetValue(payload, null));
                }
            }

            return writer;
        }

        public static void Deserialize()
        {

        }
    }
}

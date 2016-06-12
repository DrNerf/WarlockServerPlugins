using System;

namespace CommunicationLayer
{
    [Serializable]
    public class GenericPayload<T>
    {
        public T Value { get; set; }
    }
}

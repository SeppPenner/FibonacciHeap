using System;
using System.Runtime.Serialization;

namespace FibonacciHeap
{
    [Serializable]
    public class EmptyHeapException : Exception
    {
        public EmptyHeapException()
        {
        }

        // ReSharper disable once UnusedMember.Global
        public EmptyHeapException(string message) : base(message)
        {
        }

        // ReSharper disable once UnusedMember.Global
        public EmptyHeapException(string message, Exception inner) : base(message, inner)
        {
        }

        protected EmptyHeapException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EmptyHeapException.cs" company="Hämmer Electronics">
//   Copyright (c) All rights reserved.
// </copyright>
// <summary>
//   A class to show empty heap exceptions.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace FibonacciHeap
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// A class to show empty heap exceptions.
    /// </summary>
    [Serializable]
    public class EmptyHeapException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EmptyHeapException"/> class.
        /// </summary>
        public EmptyHeapException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EmptyHeapException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public EmptyHeapException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EmptyHeapException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="inner">The inner exception.</param>
        public EmptyHeapException(string message, Exception inner) : base(message, inner)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EmptyHeapException"/> class.
        /// </summary>
        /// <param name="info">The serialization information.</param>
        /// <param name="context">The context.</param>
        protected EmptyHeapException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}

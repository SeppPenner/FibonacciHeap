// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HeapNode.cs" company="Hämmer Electronics">
//   Copyright (c) All rights reserved.
// </copyright>
// <summary>
//   The heap node class.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

#pragma warning disable 693
namespace FibonacciHeap
{
    /// <summary>
    /// The heap node class.
    /// </summary>
    /// <typeparam name="TValue">The value.</typeparam>
    /// <typeparam name="TPriority">The priority.</typeparam>
    public class HeapNode<TValue, TPriority>
    {
        /// <summary>
        /// Prevents a default instance of the <see cref="HeapNode{TValue,TPriority}"/> class from being created.
        /// </summary>
        private HeapNode()
        {
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        public TValue Value { get; private set; }

        /// <summary>
        /// Gets the priority.
        /// </summary>
        public TPriority Priority { get; internal set; }

        /// <summary>
        /// Gets or sets the degree.
        /// </summary>
        internal int Degree { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the heap node is marked or not.
        /// </summary>
        internal bool Marked { get; set; }

        /// <summary>
        /// Gets or sets the previous heap node.
        /// </summary>
        internal HeapNode<TValue, TPriority> Previous { get; set; }

        /// <summary>
        /// Gets or sets the next heap node.
        /// </summary>
        internal HeapNode<TValue, TPriority> Next { get; set; }

        /// <summary>
        /// Gets or sets the child heap node.
        /// </summary>
        internal HeapNode<TValue, TPriority> Child { get; set; }

        /// <summary>
        /// Gets or sets the parent heap node.
        /// </summary>
        internal HeapNode<TValue, TPriority> Parent { get; set; }

        /// <summary>
        /// Creates a new heap node.
        /// </summary>
        /// <typeparam name="TValue">The value type.</typeparam>
        /// <typeparam name="TPriority">The priority type.</typeparam>
        /// <param name="value">The value.</param>
        /// <param name="priority">The priority.</param>
        /// <returns>A new <see cref="HeapNode{TValue,TPriority}"/>.</returns>
        internal static HeapNode<TValue, TPriority> MakeNew<TValue, TPriority>(TValue value, TPriority priority)
        {
            var node = new HeapNode<TValue, TPriority>
            {
                Priority = priority,
                Value = value
            };

            node.Previous = node.Next = node;

            return node;
        }
    }
}

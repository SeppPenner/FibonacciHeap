﻿namespace FibonacciHeap
{
    public class HeapNode<TValue, TPriority>
    {
        private HeapNode()
        {
        }

        internal int Degree { get; set; }
        internal bool Marked { get; set; }

        internal HeapNode<TValue, TPriority> Prev { get; set; }
        internal HeapNode<TValue, TPriority> Next { get; set; }
        internal HeapNode<TValue, TPriority> Child { get; set; }
        internal HeapNode<TValue, TPriority> Parent { get; set; }

        public TValue Value { get; private set; }
        public TPriority Priority { get; internal set; }

#pragma warning disable 693
        internal static HeapNode<TValue, TPriority> MakeNew<TValue, TPriority>(TValue value, TPriority priority)
#pragma warning restore 693
        {
            var node = new HeapNode<TValue, TPriority>
            {
                Priority = priority,
                Value = value
            };

            node.Prev = node.Next = node;

            return node;
        }
    }
}

using System.Collections.Generic;

namespace FibonacciHeap
{
    public interface IFibonacciHeap<TValue, TPriority> : IEnumerable<TValue>
    {
        // ReSharper disable once UnusedMember.Global
        TValue Min { get; }

        // ReSharper disable once UnusedMember.Global
        HeapNode<TValue, TPriority> Push(TValue value, TPriority priority);
        // ReSharper disable once UnusedMember.Global
        HeapNode<TValue, TPriority> Pop();

        // ReSharper disable once UnusedMember.Global
        void DecreaseKey(HeapNode<TValue, TPriority> entry, TPriority newPriorty);

        // ReSharper disable once UnusedMemberInSuper.Global
        bool IsEmpty();
    }

    // ReSharper disable once UnusedMember.Global
}

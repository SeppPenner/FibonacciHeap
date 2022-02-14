// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IFibonacciHeap.cs" company="Hämmer Electronics">
//   Copyright (c) All rights reserved.
// </copyright>
// <summary>
//   The fibonacci heap interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace FibonacciHeap;

/// <summary>
/// The fibonacci heap interface.
/// </summary>
/// <typeparam name="TValue">The value.</typeparam>
/// <typeparam name="TPriority">The priority.</typeparam>
public interface IFibonacciHeap<TValue, TPriority> : IEnumerable<TValue?>
{
    /// <summary>
    /// Gets the minimum heap value.
    /// </summary>
    TValue? MinimumValue { get; }

    /// <summary>
    /// Pushes a new value with a given priority.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <param name="priority">The priority.</param>
    /// <returns>A new <see cref="HeapNode{TValue,TPriority}"/>.</returns>
    HeapNode<TValue, TPriority> Push(TValue value, TPriority priority);

    /// <summary>
    /// Checks whether the size of the heap is zero or not.
    /// </summary>
    /// <returns><c>true</c> if the fibonacci heap is empty or <c>false</c> else.</returns>
    bool IsEmpty();

    /// <summary>
    /// Pops one value from the heap and returns it.
    /// </summary>
    /// <returns>The popped <see cref="HeapNode{TValue,TPriority}"/>.</returns>
    HeapNode<TValue, TPriority> Pop();

    /// <summary>
    /// Decreases the key.
    /// </summary>
    /// <param name="entry">The entry.</param>
    /// <param name="newPriority">The new priority.</param>
    void DecreaseKey(HeapNode<TValue, TPriority> entry, TPriority newPriority);
}

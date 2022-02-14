// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FibonacciHeapTests.cs" company="Hämmer Electronics">
//   Copyright (c) All rights reserved.
// </copyright>
// <summary>
//   A class to test the <see cref="FibonacciHeap{TValue,TPriority}" /> class.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace FibonacciHeap.Tests;

/// <summary>
/// A class to test the <see cref="FibonacciHeap{TValue,TPriority}"/> class.
/// </summary>
[TestClass]
public class FibonacciHeapTests
{
    /// <summary>
    /// Checks whether the push action returns the proper minimum value.
    /// </summary>
    [TestMethod]
    public void PushIntegersReturnsProperMinimum()
    {
        var heap = new FibonacciHeap<string, int>();

        heap.Push("a", 2);
        heap.Push("b", 1);

        Assert.AreEqual("b", heap.Pop().Value);
        Assert.AreEqual("a", heap.Pop().Value);
    }

    /// <summary>
    /// Checks whether the decrease key action returns the proper minimum value.
    /// </summary>
    [TestMethod]
    public void DecreaseKeyIntegersReturnsProperMinimum()
    {
        var heap = new FibonacciHeap<string, int>();

        var node = heap.Push("a", 20);
        heap.Push("b", 10);

        heap.DecreaseKey(node, 5);

        Assert.AreEqual("a", heap.Pop().Value);
        Assert.AreEqual("b", heap.Pop().Value);
    }

    /// <summary>
    /// Checks whether the pop action returns the proper minimum value.
    /// </summary>
    [TestMethod]
    public void PopIntegersReturnsProperMinimum()
    {
        var heap = new FibonacciHeap<string, int>();

        heap.Push("a", 20);
        heap.Push("b", 10);

        heap.Pop();

        Assert.AreEqual("a", heap.Pop().Value);
    }

    /// <summary>
    /// Checks whether the pop action returns the proper minimum value with a custom comparer.
    /// </summary>
    [TestMethod]
    public void PopOwnComparerReturnsProperMinimum()
    {
        var heap = new FibonacciHeap<string, int>(new ReversedIntComparer());

        heap.Push("a", 20);
        heap.Push("b", 10);

        Assert.AreEqual("a", heap.MinimumValue);
    }

    /// <summary>
    /// Checks whether the minimum value action throws an exception when the heap is empty.
    /// </summary>
    [TestMethod, ExpectedException(typeof(EmptyHeapException))]
    public void WhenEmptyHeapThrowsException()
    {
        var heap = new FibonacciHeap<string, int>();
        _ = heap.MinimumValue;
    }
}

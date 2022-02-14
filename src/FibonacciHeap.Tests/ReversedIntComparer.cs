// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReversedIntComparer.cs" company="Hämmer Electronics">
//   Copyright (c) All rights reserved.
// </copyright>
// <summary>
//   A custom class to compare <see cref="int"/>s.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace FibonacciHeap.Tests;

/// <summary>
/// A custom class to compare <see cref="int"/>s.
/// </summary>
public class ReversedIntComparer : IComparer<int>
{
    /// <summary>
    /// The default comparer.
    /// </summary>
    private readonly IComparer<int> comparer;

    /// <summary>
    /// Initializes a new instance of the <see cref="ReversedIntComparer"/> class.
    /// </summary>
    public ReversedIntComparer()
    {
        this.comparer = Comparer<int>.Default;
    }

    /// <inheritdoc cref="IComparer{T}"/>
    /// <summary>
    /// Compares two <see cref="int"/>s.
    /// </summary>
    /// <param name="x">The first <see cref="int"/>.</param>
    /// <param name="y">The second <see cref="int"/>.</param>
    /// <returns>The comparison result.</returns>
    /// <seealso cref="IComparer{T}"/>
    public int Compare(int x, int y)
    {
        return -1 * this.comparer.Compare(x, y);
    }
}

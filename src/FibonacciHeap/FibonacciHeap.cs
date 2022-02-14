// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FibonacciHeap.cs" company="Hämmer Electronics">
//   Copyright (c) All rights reserved.
// </copyright>
// <summary>
//   The fibonacci heap class.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace FibonacciHeap;

/// <inheritdoc cref="IFibonacciHeap{TValue, TPriority}"/>
/// <summary>
/// The fibonacci heap class.
/// </summary>
/// <typeparam name="TValue">The value type.</typeparam>
/// <typeparam name="TPriority">The priority.</typeparam>
/// <seealso cref="IFibonacciHeap{TValue, TPriority}"/>
public class FibonacciHeap<TValue, TPriority> : IFibonacciHeap<TValue, TPriority>
{
    /// <summary>
    /// The comparer.
    /// </summary>
    private readonly IComparer<TPriority> comparer;

    /// <summary>
    /// The nodes.
    /// </summary>
    private readonly HashSet<HeapNode<TValue, TPriority>> nodes;

    /// <summary>
    /// The minimum value.
    /// </summary>
    private HeapNode<TValue, TPriority>? minimumValue;

    /// <summary>
    /// The size.
    /// </summary>
    private int size;

    /// <summary>
    /// Initializes a new instance of the <see cref="FibonacciHeap{TValue,TPriority}"/> class.
    /// </summary>
    public FibonacciHeap() : this(Comparer<TPriority>.Default)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FibonacciHeap{TValue,TPriority}"/> class.
    /// </summary>
    /// <param name="comparer">The comparer.</param>
    public FibonacciHeap(IComparer<TPriority> comparer)
    {
        this.minimumValue = null;
        this.size = 0;

        this.comparer = comparer;
        this.nodes = new HashSet<HeapNode<TValue, TPriority>>();
    }

    /// <inheritdoc cref="IFibonacciHeap{TValue, TPriority}"/>
    /// <summary>
    /// Gets the minimum heap value.
    /// </summary>
    /// <seealso cref="IFibonacciHeap{TValue, TPriority}"/>
    public TValue? MinimumValue
    {
        get
        {
            if (this.minimumValue is null)
            {
                throw new EmptyHeapException();
            }

            return this.minimumValue.Value;
        }
    }

    /// <inheritdoc cref="IFibonacciHeap{TValue, TPriority}"/>
    /// <summary>
    /// Pushes a new value with a given priority.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <param name="priority">The priority.</param>
    /// <returns>A new <see cref="HeapNode{TValue,TPriority}"/>.</returns>
    /// <seealso cref="IFibonacciHeap{TValue, TPriority}"/>
    public HeapNode<TValue, TPriority> Push(TValue value, TPriority priority)
    {
        var newNode = HeapNode<TValue, TPriority>.MakeNew(value, priority);
        this.minimumValue = this.Merge(this.minimumValue, newNode);
        this.size++;

        this.nodes.Add(newNode);

        return newNode;
    }

    /// <inheritdoc cref="IFibonacciHeap{TValue, TPriority}"/>
    /// <summary>
    /// Checks whether the size of the heap is zero or not.
    /// </summary>
    /// <returns><c>true</c> if the fibonacci heap is empty or <c>false</c> else.</returns>
    /// <seealso cref="IFibonacciHeap{TValue, TPriority}"/>
    public bool IsEmpty()
    {
        return this.size == 0;
    }

    /// <inheritdoc cref="IFibonacciHeap{TValue, TPriority}"/>
    /// <summary>
    /// Pops one value from the heap and returns it.
    /// </summary>
    /// <returns>The popped <see cref="HeapNode{TValue,TPriority}"/>.</returns>
    /// <seealso cref="IFibonacciHeap{TValue, TPriority}"/>
    public HeapNode<TValue, TPriority> Pop()
    {
        if (this.IsEmpty() || this.minimumValue is null)
        {
            throw new EmptyHeapException();
        }

        this.size--;

        var minElem = this.minimumValue;

        if (this.minimumValue.Next == this.minimumValue)
        {
            this.minimumValue = null;
        }
        else
        {
            if (this.minimumValue.Previous is not null)
            {
                this.minimumValue.Previous.Next = this.minimumValue.Next;
            }

            if (this.minimumValue.Next is not null)
            {
                this.minimumValue.Next.Previous = this.minimumValue.Previous;
            }
           
            this.minimumValue = this.minimumValue.Next;
        }

        if (minElem.Child != null)
        {
            var current = minElem.Child;

            do
            {
                if (current is not null)
                {
                    current.Parent = null;
                    current = current.Next;
                }
            }
            while (current != minElem.Child);
        }

        this.minimumValue = this.Merge(this.minimumValue, minElem.Child);

        var tree = new Dictionary<int, HeapNode<TValue, TPriority>?>();
        var toVisit = new LinkedList<HeapNode<TValue, TPriority>?>();

        if (this.minimumValue == null)
        {
            this.nodes.Remove(minElem);
            return minElem;
        }

        if (toVisit.IsNullOrEmpty())
        {
            throw new EmptyHeapException();
        }

        if (toVisit.First is null)
        {
            throw new EmptyHeapException();
        }

        for (var current = this.minimumValue;
             toVisit.Count == 0 || toVisit.First.Value != current;
             current = current?.Next)
        {
            toVisit.AddLast(current);
        }

        foreach (var globalCurrent in toVisit)
        {
            var current = globalCurrent;

            while (true)
            {
                if (current is not null)
                {
                    if (!tree.ContainsKey(current.Degree))
                    {
                        tree.Add(current.Degree, null);
                    }

                    if (tree[current.Degree] == null)
                    {
                        tree[current.Degree] = current;
                        break;
                    }

                    var other = tree[current.Degree];
                    tree[current.Degree] = null;

                    if (other is not null)
                    {
                        var min = this.comparer.Compare(other.Priority, current.Priority) < 0 ? other : current;
                        var max = this.comparer.Compare(other.Priority, current.Priority) < 0 ? current : other;

                        if (max.Next is not null)
                        {
                            max.Next.Previous = max.Previous;
                        }

                        if (max.Previous is not null)
                        {
                            max.Previous.Next = max.Next;
                        }

                        max.Next = max.Previous = max;
                        min.Child = this.Merge(min.Child, max);
                        max.Parent = min;

                        max.Marked = false;
                        ++min.Degree;
                        current = min;
                    }
                }
            }

            if (this.comparer.Compare(current.Priority, this.minimumValue.Priority) <= 0)
            {
                this.minimumValue = current;
            }
        }

        this.nodes.Remove(minElem);
        return minElem;
    }

    /// <summary>
    /// Decreases the key.
    /// </summary>
    /// <param name="entry">The entry.</param>
    /// <param name="newPriority">The new priority.</param>
    public void DecreaseKey(HeapNode<TValue, TPriority> entry, TPriority newPriority)
    {
        if (!this.nodes.Contains(entry))
        {
            throw new ArgumentException("Node belongs to other heap", nameof(entry));
        }

        entry.Priority = newPriority;
        if (entry.Parent != null
            && this.comparer.Compare(entry.Priority, entry.Parent.Priority) <= 0)
        {
            this.Cut(entry);
        }

        if (this.minimumValue is not null)
        {
            if (this.comparer.Compare(entry.Priority, this.minimumValue.Priority) <= 0)
            {
                this.minimumValue = entry;
            }
        }
    }

    /// <summary>
    /// Gets an enumerator.
    /// </summary>
    /// <returns>The <see cref="IEnumerator"/> of the <see cref="TValue"/>s.</returns>
    public IEnumerator<TValue?> GetEnumerator()
    {
        return this.nodes.Select(n => n.Value).GetEnumerator();
    }

    /// <summary>
    /// Gets an enumerator.
    /// </summary>
    /// <returns>The <see cref="IEnumerator"/>.</returns>
    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }

    /// <summary>
    /// Cuts the heap.
    /// </summary>
    /// <param name="entry">The entry.</param>
    private void Cut(HeapNode<TValue, TPriority> entry)
    {
        entry.Marked = false;

        if (entry.Parent is null)
        {
            return;
        }

        if (entry.Next != entry)
        {
            if (entry.Next is not null)
            {
                entry.Next.Previous = entry.Previous;
            }

            if (entry.Previous is not null)
            {
                entry.Previous.Next = entry.Next;
            }
        }

        if (entry.Parent.Child == entry)
        {
            entry.Parent.Child = entry.Next != entry ? entry.Next : null;
        }

        entry.Parent.Degree--;
        entry.Previous = entry.Next = entry;
        this.minimumValue = this.Merge(this.minimumValue, entry);

        if (entry.Parent.Marked)
        {
            this.Cut(entry.Parent);
        }
        else
        {
            entry.Parent.Marked = true;
        }

        entry.Parent = null;
    }

    /// <summary>
    /// Merges two heap nodes.
    /// </summary>
    /// <param name="first">The first node.</param>
    /// <param name="second">The second node.</param>
    /// <returns>The merged node.</returns>
    private HeapNode<TValue, TPriority>? Merge(HeapNode<TValue, TPriority>? first, HeapNode<TValue, TPriority>? second)
    {
        if (first is null && second is null)
        {
            return null;
        }

        if (first is not null && second is null)
        {
            return first;
        }

        if (first is null && second is not null)
        {
            return second;
        }

        var firstNext = first!.Next;
        first.Next = second!.Next;

        if (first.Next is not null)
        {
            first.Next.Previous = first;
        }

        second.Next = firstNext;

        if (second.Next is not null)
        {
            second.Next.Previous = second;
        }

        return this.comparer.Compare(first.Priority, second.Priority) < 0
            ? first
            : second;
    }
}

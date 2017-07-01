using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace FibonacciHeap
{
    // ReSharper disable once UnusedMember.Global
    public class FibonacciHeap<TValue, TPriority> : IFibonacciHeap<TValue, TPriority>
    {
        private readonly IComparer<TPriority> _comparer;
        private readonly HashSet<HeapNode<TValue, TPriority>> _nodes;

        private HeapNode<TValue, TPriority> _min;
        private int _size;

        public FibonacciHeap()
            : this(Comparer<TPriority>.Default)
        {
        }

        public FibonacciHeap(IComparer<TPriority> comparer)
        {
            _min = null;
            _size = 0;

            _comparer = comparer;
            _nodes = new HashSet<HeapNode<TValue, TPriority>>();
        }

        public HeapNode<TValue, TPriority> Push(TValue value, TPriority priority)
        {
            var newNode = HeapNode< TValue, TPriority>.MakeNew(value, priority);

            _min = Merge(_min, newNode);
            _size++;

            _nodes.Add(newNode);

            return newNode;
        }

        public TValue Min
        {
            get
            {
                if (_min == null) throw new EmptyHeapException();

                return _min.Value;
            }
        }

        public bool IsEmpty()
        {
            return _size == 0;
        }

        public HeapNode<TValue, TPriority> Pop()
        {
            if (IsEmpty()) throw new EmptyHeapException();
            _size--;

            var minElem = _min;
            
            if (_min.Next == _min)
            {
                _min = null;
            }
            else
            {
                _min.Prev.Next = _min.Next;
                _min.Next.Prev = _min.Prev;
                _min = _min.Next;
            }

            if (minElem.Child != null)
            {
                var current = minElem.Child;
                do
                {
                    current.Parent = null;
                    current = current.Next;
                } while (current != minElem.Child);
            }

            _min = Merge(_min, minElem.Child);

            var tree = new Dictionary<int, HeapNode<TValue, TPriority>>();
            var toVisit = new LinkedList<HeapNode<TValue, TPriority>>();

            if (_min == null)
            {
                _nodes.Remove(minElem);
                return minElem;
            }
            

            for (var current = _min;
                toVisit.Count == 0 || toVisit.First.Value != current; 
                current = current.Next)
            {
                toVisit.AddLast(current);
            }

            foreach (var globalCurr in toVisit)
            {
                var curr = globalCurr;
                while (true)
                {
                    if (!tree.ContainsKey(curr.Degree))
                    {
                        tree.Add(curr.Degree, null);
                    }

                    if (tree[curr.Degree] == null)
                    {
                        tree[curr.Degree] = curr;
                        break;
                    }

                    var other = tree[curr.Degree];
                    tree[curr.Degree] = null;

                    var min = _comparer.Compare(other.Priority, curr.Priority) < 0 ? other : curr;
                    var max = _comparer.Compare(other.Priority, curr.Priority) < 0 ? curr : other;

                    max.Next.Prev = max.Prev;
                    max.Prev.Next = max.Next;

                    max.Next = max.Prev = max;
                    min.Child = Merge(min.Child, max);
                    max.Parent = min;

                    max.Marked = false;
                    ++min.Degree;
                    curr = min;
                }

                if (_comparer.Compare(curr.Priority, _min.Priority) <= 0)
                {
                    _min = curr;
                }
            }

            _nodes.Remove(minElem);
            return minElem;
        }

        public void DecreaseKey(HeapNode<TValue, TPriority> entry, TPriority newPriorty)
        {
            if (!_nodes.Contains(entry)) throw new ArgumentException("Node belongs to other heap", nameof(entry));

            entry.Priority = newPriorty;
            if (entry.Parent != null
                && _comparer.Compare(entry.Priority, entry.Parent.Priority) <= 0)
            {
                Cut(entry);
            }
            if (_comparer.Compare(entry.Priority, _min.Priority) <= 0)
            {
                _min = entry;
            }
        }

        public IEnumerator<TValue> GetEnumerator()
        {
            return _nodes.Select(n => n.Value).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private void Cut(HeapNode<TValue, TPriority> entry)
        {
            entry.Marked = false;
            if (entry.Parent == null)
            {
                return;
            }

            if (entry.Next != entry)
            {
                entry.Next.Prev = entry.Prev;
                entry.Prev.Next = entry.Next;
            }

            if (entry.Parent.Child == entry)
            {
                entry.Parent.Child = entry.Next != entry ? entry.Next : null;
            }

            entry.Parent.Degree--;
            entry.Prev = entry.Next = entry;
            _min = Merge(_min, entry);

            if (entry.Parent.Marked)
                Cut(entry.Parent);
            else
                entry.Parent.Marked = true;

            entry.Parent = null;
        }

        private HeapNode<TValue, TPriority> Merge(
            HeapNode<TValue, TPriority> first,
            HeapNode<TValue, TPriority> second)
        {

            if (first == null && second == null)
            {
                return null;
            }

            if (first != null && second == null)
            {
                return first;
            }

            if (first == null)
            {
                return second;
            }

            var firstNext = first.Next;
            first.Next = second.Next;
            first.Next.Prev = first;
            second.Next = firstNext;
            second.Next.Prev = second;
            return _comparer.Compare(first.Priority, second.Priority) < 0 
                ? first 
                : second;
        }
    }
}
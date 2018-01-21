FibonacciHeap
====================================

FibonacciHeap is an assembly/ library to build and use Fibonacci heaps in C#.
The assembly was written and tested in .Net 4.7.

[![Build status](https://ci.appveyor.com/api/projects/status/4r73mgd973f5bek9?svg=true)](https://ci.appveyor.com/project/SeppPenner/fibonacciheap)

## Basic usage:
```csharp
public void Test()
{
	FibonacciHeap heap = new FibonacciHeap();
	bool empty = heap.IsEmpty();
	var minValue = heap.Min;
	var heapNode = heap.Pop();
	//...
}
```

## Available methods:
```csharp
    public TValue Min {}

    public bool IsEmpty();

    public HeapNode<TValue, TPriority> Pop();

    public void DecreaseKey(HeapNode<TValue, TPriority> entry, TPriority newPriorty);

    public IEnumerator<TValue> GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator();

    private void Cut(HeapNode<TValue, TPriority> entry);
    
    private HeapNode<TValue, TPriority> Merge(
        HeapNode<TValue, TPriority> first,
        HeapNode<TValue, TPriority> second);
```

Change history
--------------

* **Version 1.0.0.0 (2017-01-07)** : 1.0 release.

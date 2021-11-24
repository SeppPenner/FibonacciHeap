FibonacciHeap
====================================

FibonacciHeap is an assembly/ library to build and use Fibonacci heaps in C#.

[![Build status](https://ci.appveyor.com/api/projects/status/4r73mgd973f5bek9?svg=true)](https://ci.appveyor.com/project/SeppPenner/fibonacciheap)
[![GitHub issues](https://img.shields.io/github/issues/SeppPenner/FibonacciHeap.svg)](https://github.com/SeppPenner/FibonacciHeap/issues)
[![GitHub forks](https://img.shields.io/github/forks/SeppPenner/FibonacciHeap.svg)](https://github.com/SeppPenner/FibonacciHeap/network)
[![GitHub stars](https://img.shields.io/github/stars/SeppPenner/FibonacciHeap.svg)](https://github.com/SeppPenner/FibonacciHeap/stargazers)
[![License: MIT](https://img.shields.io/badge/License-MIT-blue.svg)](https://raw.githubusercontent.com/SeppPenner/FibonacciHeap/master/License.txt)
[![Known Vulnerabilities](https://snyk.io/test/github/SeppPenner/FibonacciHeap/badge.svg)](https://snyk.io/test/github/SeppPenner/FibonacciHeap)

## Basic usage
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

## Available methods
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

See the [Changelog](https://github.com/SeppPenner/FibonacciHeap/blob/master/Changelog.md).
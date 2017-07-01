﻿using System.Collections.Generic;
using FibonacciHeap;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FibonacciHeapTests
{
    [TestClass]
    public class FibonacciHeapTests
    {
        [TestMethod]
        public void Push_SampleInts_RetunrsProperMin()
        {
            var heap = new FibonacciHeap<string, int>();

            heap.Push("a", 2);
            heap.Push("b", 1);

            Assert.AreEqual("b", heap.Pop().Value);
            Assert.AreEqual("a", heap.Pop().Value);
        }

        [TestMethod]
        public void DecreaseKey_SampleInts_RetunrsProperMin()
        {
            var heap = new FibonacciHeap<string, int>();

            var aNode = heap.Push("a", 20);
            heap.Push("b", 10);

            heap.DecreaseKey(aNode, 5);

            Assert.AreEqual("a", heap.Pop().Value);
            Assert.AreEqual("b", heap.Pop().Value);
        }


        [TestMethod]
        public void Pop_SampleInts_ReturnsProperMin()
        {
            var heap = new FibonacciHeap<string, int>();

            heap.Push("a", 20);
            heap.Push("b", 10);

            heap.Pop();
            
            Assert.AreEqual("a", heap.Pop().Value);
        }

        [TestMethod]
        public void Pop_OwnComparer_ReturnsProperMin()
        {
            var heap = new FibonacciHeap<string, int>(new ReversedIntComparer());

            heap.Push("a", 20);
            heap.Push("b", 10);

            Assert.AreEqual("a", heap.Min);
        }
        

        [TestMethod, ExpectedException(typeof (EmptyHeapException))]
        public void Min_WhenEmptyHeap_ThrowsException()
        {
            var heap = new FibonacciHeap<string, int>();
            // ReSharper disable once UnusedVariable
            var min = heap.Min;
        }

        private class ReversedIntComparer : IComparer<int>
        {
            private readonly IComparer<int> _defaultComparer;

            public ReversedIntComparer()
            {
                _defaultComparer = Comparer<int>.Default;
            }

            public int Compare(int x, int y)
            {
                return (-1)*_defaultComparer.Compare(x, y);
            }
        }
    }
}

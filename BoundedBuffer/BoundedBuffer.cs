using System;
using System.Collections.Generic;
using System.Threading;

namespace BoundedBuffer
{
    internal class BoundedBuffer
    {
        private Queue<Item> buffer;
        private SemaphoreSlim empty;
        private SemaphoreSlim full;
        private object lockObj;

        public BoundedBuffer(int capacity)
        {
            buffer = new Queue<Item>(capacity);
            empty = new SemaphoreSlim(0, capacity);
            full = new SemaphoreSlim(capacity, capacity);
            lockObj = new object();
        }

        public void Insert(Item item)
        {
            full.Wait(); 
            lock (lockObj)
            {
                buffer.Enqueue(item);
            }
            empty.Release(); 
        }

        public Item Take()
        {
            empty.Wait(); 
            Item item;
            lock (lockObj)
            {
                item = buffer.Dequeue();
            }
            full.Release(); 
            return item;
        }
    }
}
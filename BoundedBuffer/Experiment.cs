using System;
using System.Collections.Generic;
using System.Threading;

namespace BoundedBuffer
{
    internal class Experiment
    {
        private Queue<Item> _queue = new Queue<Item>();
        private Random _random = new Random();

        public void Producer()
        {
            while (true)
            {
                int value = _random.Next(100); 
                Item newItem = new Item(value);
                lock (_queue)
                {
                    _queue.Enqueue(newItem);
                }
                Console.WriteLine($"Produced: {newItem.Id}, Value: {newItem.Value}");
                Thread.Sleep(_random.Next(20)); 
            }
        }

        public void Consumer()
        {
            while (true)
            {
                Item item = null;
                lock (_queue)
                {
                    if (_queue.Count > 0)
                    {
                        item = _queue.Dequeue();
                    }
                }
                if (item != null)
                {
                    Console.WriteLine($"Consumed: {item.Id}, Value: {item.Value}");
                }
                Thread.Sleep(_random.Next(10)); 
            }
        }

        public void Start()
        {
            
            for (int i = 0; i < 4; i++)
            {
                Thread producerThread = new Thread(Producer);
                producerThread.Start();
            }

            
            for (int i = 0; i < 2; i++)
            {
                Thread consumerThread = new Thread(Consumer);
                consumerThread.Start();
            }
        }
    }
}
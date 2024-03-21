using Kitchen.CookingSimulator.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TraineeTasks.CookingSimulator.CookingProcesses;

namespace Kitchen.CookingSimulator.HelperClasses
{
    internal class CustomThreadPool
    {
        private Queue<IDishRecipe> dishQueue;
        [ThreadStatic]private static IDishRecipe dish;

        private ParameterizedThreadStart threadFunc;

        private AutoResetEvent pauseAutoResetEvent = new AutoResetEvent(true);
        private object dishQueueLock = new object();
        private bool isEnd = false;

        private Thread[] threads;
        public CustomThreadPool(int maxThread, ParameterizedThreadStart threadStart)
        {
            dishQueue = new Queue<IDishRecipe>();

            threadFunc = new ParameterizedThreadStart(threadStart);

            threads = new Thread[maxThread];

            for (int i = 0; i < maxThread; i++)
            {
                threads[i] = new Thread(ThreadLifeCycle);
                threads[i].Start();
            }
        }

        public void AddDish(IDishRecipe value)
        {
            dishQueue.Enqueue(value);
            pauseAutoResetEvent.Set();
        }

        public void EndWork()
        {
            isEnd = true;
            pauseAutoResetEvent.Set();
        }

        private void ThreadLifeCycle()
        {
            while (true)
            {
                if (dishQueue.Count <= 0)
                {
                    pauseAutoResetEvent.WaitOne();
                    if (isEnd) 
                    {
                        pauseAutoResetEvent.Set();
                        break;
                    }
                    else Console.WriteLine("---------------------\nDish Queue is empty\n---------------------");
                }
                else
                {
                    lock (dishQueue)
                    {
                        dish = dishQueue.Dequeue();
                    }
                    threadFunc(dish);
                }
            }
        }
    }
}

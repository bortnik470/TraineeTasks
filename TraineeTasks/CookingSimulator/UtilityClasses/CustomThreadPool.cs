using RecipeRequirement.Interfaces;

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
                threads[i] = new Thread(ThreadLifeCycle) { Name = $"{i + 1} cook"};
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
                    lock (dishQueueLock)
                    {
                        dish = dishQueue.Dequeue();
                    }
                    threadFunc(dish);
                }
            }
        }
    }
}
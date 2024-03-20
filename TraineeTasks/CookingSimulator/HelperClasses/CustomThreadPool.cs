using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraineeTasks.CookingSimulator.CookingProcesses;

namespace Kitchen.CookingSimulator.HelperClasses
{
    internal class CustomThreadPool
    {
        private Thread[] threads;
        public CustomThreadPool(int maxThread, ParameterizedThreadStart threadStart)
        {
            threads = new Thread[maxThread];

            for (int i = 0; i < maxThread; i++)
            {
                threads[i] = new Thread(threadStart);
            }
        }

        public void Start(object value)
        {
            for (int i = 0; i < threads.Length; i++)
            {
                if (!threads[i].IsAlive)
                {
                    CookingProcesses._humanSimulationSemaphore.WaitOne();
                    threads[i].Start(value);
                    CookingProcesses._humanSimulationSemaphore.Release();

                    break;
                }
            }
        }
    }
}

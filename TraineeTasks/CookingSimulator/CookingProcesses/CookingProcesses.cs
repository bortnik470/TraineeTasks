using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraineeTasks.CookingSimulator.CookingProcesses
{
    internal static class CookingProcesses
    {
        public static Semaphore _humanSimulationSemaphore = new Semaphore(3, 3);
        private static object fryLock = new object();
        private static object panLock = new object();

        public static void Fry(int fryTime = 6000)
        {
            _humanSimulationSemaphore.WaitOne();
            lock (fryLock)
            {
                Console.WriteLine($"{Thread.CurrentThread.Name} Start frying\n");

                _humanSimulationSemaphore.Release();

                Thread.Sleep(fryTime);
                
                Console.WriteLine($"{Thread.CurrentThread.Name} Frying is end\n");
            }
        }

        public static void Boil(int boilTime = 10000)
        {
            _humanSimulationSemaphore.WaitOne();
            lock (panLock)
            {
                Console.WriteLine($"{Thread.CurrentThread.Name} Start boiling\n");

                _humanSimulationSemaphore.Release();

                Thread.Sleep(boilTime);
                
                Console.WriteLine($"{Thread.CurrentThread.Name} Boiling is end\n");
            }
        }

        public static void Peel(int peelTime = 3000)
        {
            _humanSimulationSemaphore.WaitOne();
            Console.WriteLine($"{Thread.CurrentThread.Name} Peeling\n");
            
            Thread.Sleep(peelTime);

            Console.WriteLine($"{Thread.CurrentThread.Name} Peeling is end\n");
            _humanSimulationSemaphore.Release();
        }

        public static void Wash(int wahsTime = 8000)
        {
            _humanSimulationSemaphore.WaitOne();
            Console.WriteLine($"{Thread.CurrentThread.Name} Washing\n");
            
            Thread.Sleep(wahsTime);

            Console.WriteLine($"{Thread.CurrentThread.Name} Washing is end\n");
            _humanSimulationSemaphore.Release();
        }

        public static void Cut(int cutTime = 5000)
        {
            _humanSimulationSemaphore.WaitOne();
            Console.WriteLine($"{Thread.CurrentThread.Name} Cutting\n");
            
            Thread.Sleep(cutTime);

            Console.WriteLine($"{Thread.CurrentThread.Name} Cutting is end\n");
            _humanSimulationSemaphore.Release();
        }

        public static void Mix(int mixTime = 5000)
        {
            _humanSimulationSemaphore.WaitOne();
            Console.WriteLine($"{Thread.CurrentThread.Name} Start mixing\n");
            
            Thread.Sleep(mixTime);

            Console.WriteLine($"{Thread.CurrentThread.Name} Mixing is end\n");
            _humanSimulationSemaphore.Release();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraineeTasks.CookingSimulator.CookingProcesses
{
    internal static class CookingProcesses
    {
        //controls the maximum number of threads that used in kitchen simulation
        public static int maxThreadValue = 3;

        public static Semaphore _humanSimulationSemaphore = new Semaphore(maxThreadValue, maxThreadValue);
        private static AutoResetEvent fryResetEvent = new AutoResetEvent(true);
        private static AutoResetEvent panResetEvent = new AutoResetEvent(true);

        public static async Task Fry(int fryTime = 6000)
        {
            _humanSimulationSemaphore.WaitOne();
            string cookName = Thread.CurrentThread.Name;

            fryResetEvent.WaitOne();
            Console.WriteLine($"{cookName} Start frying\n");

            _humanSimulationSemaphore.Release();

            await Task.Delay(fryTime);

            Console.WriteLine($"{cookName} Frying is end\n");
            fryResetEvent.Set();
        }

        public static async Task Boil(int boilTime = 10000)
        {
            _humanSimulationSemaphore.WaitOne();

            string cookName = Thread.CurrentThread.Name;

            panResetEvent.WaitOne();
            Console.WriteLine($"{cookName} Start boiling\n");

            _humanSimulationSemaphore.Release();

            await Task.Delay(boilTime);

            Console.WriteLine($"{cookName} Boiling is end\n");
            panResetEvent.Set();
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

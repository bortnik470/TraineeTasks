using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraineeTasks.CookingSimulator.CookingProcesses
{
    internal static class CookingProcesses
    {
        private static object fryLock = new object();
        private static object panLock = new object();

        public static void Fry(int fryTime = 6000)
        {
            lock (fryLock)
            {
                Console.Write(Thread.CurrentThread.ManagedThreadId + " ");
                Console.WriteLine("Start frying");
                
                Thread.Sleep(fryTime);
                
                Console.Write(Thread.CurrentThread.ManagedThreadId + " ");
                Console.WriteLine("Frying is end");
            }
        }

        public static void Boil(int boilTime = 10000)
        {
            lock (panLock)
            {
                Console.Write(Thread.CurrentThread.ManagedThreadId + " ");
                Console.WriteLine("Start boiling");
                
                Thread.Sleep(boilTime);
                
                Console.Write(Thread.CurrentThread.ManagedThreadId + " ");
                Console.WriteLine("Boiling is end");
            }
        }

        public static void Peel(int peelTime = 3000)
        {
            Console.Write(Thread.CurrentThread.ManagedThreadId + " ");
            Console.WriteLine("Peeling");
            
            Thread.Sleep(peelTime);

            Console.Write(Thread.CurrentThread.ManagedThreadId + " ");
            Console.WriteLine("Peeling is end");
        }

        public static void Wash(int wahsTime = 8000)
        {
            Console.Write(Thread.CurrentThread.ManagedThreadId + " ");
            Console.WriteLine("Washing");
            
            Thread.Sleep(wahsTime);

            Console.Write(Thread.CurrentThread.ManagedThreadId + " ");
            Console.WriteLine("Washing is end");
        }

        public static void Cut(int cutTime = 5000)
        {
            Console.Write(Thread.CurrentThread.ManagedThreadId + " ");
            Console.WriteLine("Cutting");
            
            Thread.Sleep(cutTime);

            Console.Write(Thread.CurrentThread.ManagedThreadId + " ");
            Console.WriteLine("Cutting is end");
        }

        public static void Mix(int mixTime = 5000)
        {
            Console.Write(Thread.CurrentThread.ManagedThreadId + " ");
            Console.WriteLine("Start mixing");
            
            Thread.Sleep(mixTime);

            Console.Write(Thread.CurrentThread.ManagedThreadId + " ");
            Console.WriteLine("Mixing is end");
        }
    }
}

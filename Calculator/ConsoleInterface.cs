using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    public class ConsoleInterface
    {
        public void Start()
        {

        }

        public double GetDouble(string message)
        {
            Console.WriteLine($"Enter a double for {message}");
            while (true)
            {
                var value = Console.ReadLine();
                double result;
                if (double.TryParse(value, out result))
                {
                    return result;
                }
                else { Console.WriteLine("Enter a double value"); }
            }
        }

        public void WriteResult(double result)
        {
            Console.WriteLine($"Result of calculation is {result}");
        }
    }
}

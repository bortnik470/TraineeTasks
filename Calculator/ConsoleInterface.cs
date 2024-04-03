using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    public class ConsoleInterface
    {
        public readonly TextWriter _writer;

        public ConsoleInterface(TextWriter writer)
        {
            _writer = writer;
        }

        public void Start()
        {

        }

        public double GetDouble(string message)
        {
            _writer.WriteLine($"Enter a double for {message}");
            while (true)
            {
                var value = Console.ReadLine();
                double result;
                if (double.TryParse(value, out result))
                {
                    return result;
                }
                else { _writer.WriteLine("Enter a double value"); }
            }
        }

        public void WriteResult(double result)
        {
            _writer.WriteLine($"Result of calculation is {result}");
        }
    }
}

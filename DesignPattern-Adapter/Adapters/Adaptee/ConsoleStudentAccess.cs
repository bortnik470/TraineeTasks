using DesignPattern_Adapter.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern_Adapter.Adapters.Adaptee
{
    internal class ConsoleStudentAccess
    {
        public ConsoleStudentAccess() { }

        public Student CreateStudent()
        {
            Student student;

            Console.WriteLine("Write studentId:");
            int studentId = int.Parse(Console.ReadLine());

            Console.WriteLine("Write student first name:");
            string FirstName = Console.ReadLine();

            Console.WriteLine("Write student last name:");
            string LastName = Console.ReadLine();

            Console.WriteLine("Write student group:");
            string groupName = Console.ReadLine();

            Console.WriteLine("Write student phone number:");
            string phoneNumber = Console.ReadLine();

            student = new Student(studentId, FirstName, LastName, phoneNumber, groupName);

            return student;
        }
    }
}

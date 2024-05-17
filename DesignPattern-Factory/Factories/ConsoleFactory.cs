using DesignPattern_Factory.DataModels.Users;
using DesignPattern_Factory.Enums;
using StudentsInfo.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern_Factory.Factories
{
    internal class ConsoleFactory : BaseFactoryMethod
    {
        protected override BaseUser CreateUser(int userId = 0)
        {
            Console.WriteLine("Enter role:");
            var role = Console.ReadLine();

            var roleType = (Roles)Enum.Parse(typeof(Roles), role);

            switch (roleType)
            {
                case Roles.Admin:
                    return new Admin();
                case Roles.Student:
                    return CreateStudent();
                case Roles.Teacher:
                    return CreateTeacher();
                default:
                    return null;
            }
        }

        private Teacher CreateTeacher()
        {
            var baseInfo = GetBaseInfo();
            List<string> groupes = new();
            int i = 0;
            do
            {
                Console.WriteLine("If you want to add groupes enter 1");
                i = int.Parse(Console.ReadLine());
                if (i == 1)
                {
                    groupes.Add(Console.ReadLine());
                }
            } while (i == 1);

            return new Teacher(baseInfo[0], baseInfo[1], baseInfo[2], groupes);
        }

        private Student CreateStudent()
        {
            var baseInfo = GetBaseInfo();
            Console.WriteLine("Enter group name");
            var groupName = Console.ReadLine();
            var student = new Student(baseInfo[0], baseInfo[1], baseInfo[2], null, groupName);

            int i = 0;
            do
            {
                Console.WriteLine("If you want to add courses enter 1");
                i = int.Parse(Console.ReadLine());
                if (i == 1)
                {
                    student.AddCourse(student.EnterCourseInfo());
                }
            } while (i == 1);

            return student;
        }

        private List<string> GetBaseInfo()
        {
            Console.WriteLine("Enter First name");
            string firstName = Console.ReadLine();

            Console.WriteLine("Enter Last name");
            string lastName = Console.ReadLine();

            Console.WriteLine("Enter Phone number");
            string phoneNumber = Console.ReadLine();

            return new List<string>() { firstName, lastName, phoneNumber };
        } 
    }
}

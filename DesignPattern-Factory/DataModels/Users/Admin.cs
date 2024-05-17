using ADO_Net_demo.DAL;
using StudentsInfo.DataModels;
using System.Text;

namespace DesignPattern_Factory.DataModels.Users
{
    internal class Admin : BaseUser
    {
        public Admin(string firstName, string lastName, string phoneNumber) : base(firstName, lastName, phoneNumber)
        {
        }

        public Admin()
        {
        }

        public void DeleteStudent(int studentId)
        {
            DataAccessConnected dac = new DataAccessConnected();

            dac.Delete(studentId);
        }

        public void CreateStudent()
        {
            Console.WriteLine("Enter student First name");
            string firstName = Console.ReadLine();

            Console.WriteLine("Enter student Last name");
            string lastName = Console.ReadLine();

            Console.WriteLine("Enter student Phone number");
            string phoneNumber = Console.ReadLine();

            Console.WriteLine("Enter student Group name");
            string groupName = Console.ReadLine();

            DataAccessConnected dac = new DataAccessConnected();

            ADO_Net_demo.Student studentToAdd = new ADO_Net_demo.Student(firstName, lastName, phoneNumber, groupName, null);

            dac.Insert(studentToAdd);
        }

        public override void ChoseExecutionMethod()
        {
            int i = 0;
            Console.WriteLine("To delete student enter 1;\nTo Create student enter 2;");

            i = int.Parse(Console.ReadLine());

            switch (i)
            {
                case 1:
                    Console.WriteLine("Enter studentId");
                    int studentId = int.Parse(Console.ReadLine());
                    DeleteStudent(studentId);
                    break;
                case 2:
                    CreateStudent();
                    break;
                default:
                    break;
            }
        }

        public override string GetInfo()
        {
            DataAccessConnected dac = new DataAccessConnected();

            var students = dac.GetList();

            StringBuilder sb = new StringBuilder();

            foreach (var student in students)
            {
                sb.AppendLine(student.ToString());
            }

            return sb.ToString();
        }
    }
}

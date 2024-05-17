using ADO_Net_demo.DAL;
using StudentsInfo.DataModels;
using System.Text;

namespace DesignPattern_Factory.DataModels.Users
{
    internal class Teacher : BaseUser
    {
        public readonly List<string> Groupes;

        public Teacher(string firstName, string lastName, string phoneNumber, List<string> groupes) : base(firstName, lastName, phoneNumber)
        {
            Groupes = groupes;
        }

        public void SetScore(int courseId, string score)
        {
            DataAccessConnected dataAccessConnected = new DataAccessConnected();

            dataAccessConnected.UpdateCourseScore(courseId, score);
        }

        public List<Student> GetStudents()
        {
            List<Student> students = new List<Student>();

            DataAccessConnected dataAccessConnected = new DataAccessConnected();

            var studentsData = dataAccessConnected.GetList().
                Where(x => Groupes.Contains(x.GroupName)).ToList();

            foreach (var student in studentsData)
            {
                students.Add(new Student(student.FirstName, student.LastName,
                    student.PhoneNumber, new List<Course>(),student.GroupName));
            }

            return students;
        }

        public override void ChoseExecutionMethod()
        {
            int i = 0;
            Console.WriteLine("To set course score enter 1;\nTo print students of your groupes enter 2;");

            i = int.Parse(Console.ReadLine());

            switch (i)
            {
                case 1:
                    Console.WriteLine("Enter course ID");
                    int courseID = int.Parse(Console.ReadLine());
                    Console.WriteLine("Enter course score");
                    string score = Console.ReadLine();
                    SetScore(courseID, score);
                    break;
                case 2:
                    foreach(var student in GetStudents())
                    {
                        Console.WriteLine(student.ToString());
                    }
                    break;
                default:
                    break;
            }
        }

        public override string GetInfo()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(base.ToString());

            foreach (var item in Groupes)
            {
                sb.AppendLine($"Group - {item}");
            }

            return sb.ToString();
        }
    }
}

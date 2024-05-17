using StudentsInfo.DataModels;
using System.Text;

namespace DesignPattern_Factory.DataModels.Users
{
    internal class Student : BaseUser
    {
        public List<Course> Courses { get; set; }
        public string GroupName { get; private set; }

        public Student()
        {
            Courses = new List<Course>();
        }

        public Student(string firstName, string lastName, string phoneNumber, List<Course> disciplines, string groupName) : base(firstName, lastName, phoneNumber)
        {
            if (disciplines == null)
                Courses = new();
            else
                Courses = disciplines;

            GroupName = groupName;
        }

        public void AddCourse(Course course)
        {
            Courses.Add(course);
        }

        public void RemoveCourse(Course course)
        {
            if (course.StartDate > DateTime.Now)
            {
                Console.WriteLine("The course has already started.");
                return;
            }
            Courses.Remove(course);
        }

        public override void ChoseExecutionMethod()
        {
            int i = 0;
            Console.WriteLine("To add course enter 1;\nTo remove course enter 2;");

            i = int.Parse(Console.ReadLine());

            switch (i)
            {
                case 1:
                    AddCourse(EnterCourseInfo());
                    break;
                case 2:
                    RemoveCourse(EnterCourseInfo());
                    break;
                default:
                    break;
            }
        }

        public Course EnterCourseInfo()
        {
            Console.WriteLine("Enter course name");
            string courseName = Console.ReadLine();

            Console.WriteLine("Enter course score");
            string score = Console.ReadLine();

            Console.WriteLine("Enter course start date. (mm/dd/yyyy)");
            DateTime startDate = DateTime.Parse(Console.ReadLine());

            Console.WriteLine("Enter course end date. (mm/dd/yyyy)");
            DateTime endDate = DateTime.Parse(Console.ReadLine());

            return new Course(courseName, score, startDate, endDate);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Student`s name is: {FirstName} {LastName}");
            sb.AppendLine($"His phone number is: {PhoneNumber}");
            sb.AppendLine($"His group is: {GroupName}");

            return sb.ToString();
        }

        public override string GetInfo()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(base.ToString());

            foreach (var item in Courses)
            {
                sb.AppendLine($"Course - {item.CourseName}");
                sb.AppendLine($"Start date - {item.StartDate}");
                sb.AppendLine($"End date - {item.EndDate}");
                sb.AppendLine($"Score - {item.Score}");
            }

            return sb.ToString();
        }
    }
}

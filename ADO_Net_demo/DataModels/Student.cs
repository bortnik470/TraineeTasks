using System.Text;

namespace ADO_Net_demo
{
    public class Student
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string GroupName { get; set; }
        public List<Course> Courses { get; set; }

        public Student(string firstName, string lastName, string phoneNumber, string groupName, List<Course> courses)
        {
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            GroupName = groupName;
            Courses = courses;
        }

        public Student(int id, string firstName, string lastName, string phoneNumber, string groupName, List<Course> courses)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            GroupName = groupName;
            Courses = courses;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Student`s name is: {FirstName} {LastName}");
            sb.AppendLine($"His phone number is: {PhoneNumber}");
            sb.AppendLine($"His group is: {GroupName}");
            sb.AppendLine($"He has next disciplines: ");
            foreach (var course in Courses)
            {
                sb.AppendLine($"\tDiscipline name: {course.Name}");
                sb.AppendLine($"\tDiscipline period: from {course.StartDate} " +
                    $"to {course.EndDate}");
                if (!course.Score.Equals("None"))
                {
                    sb.AppendLine($"\tStudent`s score is {course.Score}\n");
                }
                else sb.AppendLine($"\tStudent hasn`t completed the discipline\n");
            }

            return sb.ToString();
        }
    }
}
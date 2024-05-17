using System.Text;

namespace DesignPattern_Adapter.DataModels
{
    public class Student
    {
        public int StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string GroupName { get; set; }

        public Student(string firstName, string lastName, string phoneNumber, string groupName)
        {
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            GroupName = groupName;
        }

        public Student(int studentId, string firstName, string lastName, string phoneNumber, string groupName)
        {
            StudentId = studentId;
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            GroupName = groupName;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Student with id: {StudentId} has next properties:");
            sb.AppendLine($"Student`s name is: {FirstName} {LastName}");
            sb.AppendLine($"His phone number is: {PhoneNumber}");
            sb.AppendLine($"His group is: {GroupName}");
            
            return sb.ToString();
        }
    }
}
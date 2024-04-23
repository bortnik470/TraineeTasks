namespace ADO_Net_demo
{
    public class Student
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string GroupName {  get; set; }
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
    }
}
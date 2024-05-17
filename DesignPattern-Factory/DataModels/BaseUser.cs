using System.Text;

namespace StudentsInfo.DataModels
{
    public abstract class BaseUser
    {
        public BaseUser() { }
        public BaseUser(string firstName, string lastName, string phoneNumber)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.PhoneNumber = phoneNumber;
        }

        public string FirstName { get; protected set; }
        public string LastName { get; protected set; }
        public string PhoneNumber { get; protected set; }

        public abstract void ChoseExecutionMethod();

        public abstract string GetInfo();

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"First name - {FirstName}");
            sb.AppendLine($"Last name - {LastName}");
            sb.AppendLine($"Phone number - {PhoneNumber}");

            return sb.ToString();
        }
    }
}
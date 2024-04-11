using StudentsInfo.DataModels.Disciplines;
using StudentsInfo.Interfaces;

namespace StudentsInfo.AbstractClases
{
    [Serializable]
    public abstract class BaseStudent : IStudent
    {
        protected BaseStudent() { }
        protected BaseStudent(string firstName, string lastName, string phoneNumber, string groupName, List<Discipline> disciplines)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.phoneNumber = phoneNumber;
            this.groupName = groupName;
            this.disciplines = disciplines;
        }

        public string firstName {  get; set; }
        public string lastName {  get; set; }
        public string phoneNumber { get; set; }
        public string groupName {  get; set; }
        public List<Discipline> disciplines {  get; set; }
    }
}
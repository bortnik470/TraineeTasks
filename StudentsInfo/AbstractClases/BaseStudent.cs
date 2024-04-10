using StudentsInfo.DataModels.Disciplines;
using StudentsInfo.Interfaces;

namespace StudentsInfo.AbstractClases
{
    [Serializable]
    public abstract class BaseStudent : IStudent
    {
        protected BaseStudent() { }
        protected BaseStudent(string firsName, string lastName, int phoneNumber, string groupName, List<Discipline> disciplines)
        {
            this.firsName = firsName;
            this.lastName = lastName;
            this.phoneNumber = phoneNumber;
            this.groupName = groupName;
            this.disciplines = disciplines;
        }

        public string firsName {  get; set; }
        public string lastName {  get; set; }
        public int phoneNumber { get; set; }
        public string groupName {  get; set; }
        public List<Discipline> disciplines {  get; set; }
    }
}
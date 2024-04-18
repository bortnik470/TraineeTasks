using StudentsInfo.Interfaces;

namespace StudentsInfo.DataModels
{
    [Serializable]
    public class StudentModel : IStudent
    {
        public StudentModel() { }
        public StudentModel(string firstName, string lastName, string phoneNumber, string groupName, List<DisciplineModel> disciplines)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.phoneNumber = phoneNumber;
            this.groupName = groupName;
            this.disciplines = disciplines;

            tableName = "Student";
        }

        public string firstName { get; set; }
        public string lastName { get; set; }
        public string phoneNumber { get; set; }
        public string groupName { get; set; }
        public List<DisciplineModel> disciplines { get; set; }
        public string tableName { get; private set; }

        public void FillDbModel(object[] values)
        {
            throw new NotImplementedException();
        }
    }
}

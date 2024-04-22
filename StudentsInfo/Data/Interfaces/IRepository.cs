using StudentsInfo.Data.DataModels;
using StudentsInfo.DataModels;

namespace StudentsInfo.Interfaces
{
    public interface IRepository
    {
        string connectionString { get; }

        public List<Student> GetStudentDataFromTable(string tableName, string discTableName);

        public List<Student> GetSomeStudentDataFromTable(string tableName, string discTableName,
            List<KeyValueType> valuesForFinder);

        public int GetStudentId(string tableName, Student student);

        public List<DisciplineModel> GetDisciplinesBySomeDataFromTable(string tableName,
            List<KeyValueType> valuesForFinder);

        public List<DisciplineModel> GetDisciplinesDataFromTable(string tableName);

        public int GetDisciplineId(string tableName, int studentId, DisciplineModel discipline);

        public void DeleteData(string tableName,
            List<KeyValueType> valuesForFinder);

        public void UpdateData(string tableName,
            List<KeyValueType> valuesForFinder,
            List<KeyValueType> updateData);

        public void InsertData(string tableName, List<KeyValueType> data);
    }
}
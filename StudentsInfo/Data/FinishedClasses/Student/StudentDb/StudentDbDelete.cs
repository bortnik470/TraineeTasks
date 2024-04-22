using StudentsInfo.Data.DataModels;

namespace StudentsInfo.Data.FinishedClasses.StudentClass
{
    public partial class StudentDbAccessor
    {
        public void DeleteStudent(int id)
        {
            var KVT = new List<KeyValueType>
            {
                new KeyValueType("studentId", id, "number")
            };

            DeleteDisciplinesByStudentId(id);

            dbRepository.DeleteData(stTableName, KVT);
        }

        public void DeleteDisciplinesByStudentId(int studentId)
        {
            var KVT = new List<KeyValueType>
            {
                new KeyValueType("studentId", studentId, "number")
            };

            dbRepository.DeleteData(discTableName, KVT);
        }
    }
}

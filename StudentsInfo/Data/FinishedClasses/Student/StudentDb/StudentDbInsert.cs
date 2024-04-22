using StudentsInfo.Data.DataModels;
using StudentsInfo.DataModels;

namespace StudentsInfo.Data.FinishedClasses.StudentClass
{
    public partial class StudentDbAccessor
    {
        public void AddStudentToDb(Student student)
        {
            dbRepository.InsertData(stTableName, new List<KeyValueType>
            {
                new KeyValueType (null, student.firstName),
                new KeyValueType (null, student.lastName),
                new KeyValueType (null, student.phoneNumber),
                new KeyValueType (null, student.groupName),
            });

            int studentId = GetStudentId(student);

            AddDisciplines(student.disciplines, studentId);
        }

        public void AddDiscipline(DisciplineModel discipline, int studentId)
        {
            dbRepository.InsertData(discTableName, new List<KeyValueType>
            {
                new KeyValueType (null, discipline.disciplineName.ToString()),
                new KeyValueType (null, discipline.score.ToString()),
                new KeyValueType (null, DateTimeToString(discipline.startDate)),
                new KeyValueType (null, DateTimeToString(discipline.endDate)),
                new KeyValueType (null, studentId, "number")
            });
        }

        public void AddDiscipline(DisciplineModel discipline, Student student)
        {
            int studentId = GetStudentId(student);
            AddDiscipline(discipline, studentId);
        }

        public void AddDisciplines(IEnumerable<DisciplineModel> disciplines, int studentId)
        {
            foreach (var discipline in disciplines)
                AddDiscipline(discipline, studentId);
        }

        public void AddDisciplines(IEnumerable<DisciplineModel> disciplines, Student student)
        {
            int studentId = GetStudentId(student);
            AddDisciplines(disciplines, studentId);
        }
    }
}
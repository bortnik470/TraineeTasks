using StudentsInfo.Data.DataModels;
using StudentsInfo.DataModels;
using StudentsInfo.Enums;
using StudentsInfo.Interfaces;

namespace StudentsInfo.Data.FinishedClasses.StudentClass
{
    public partial class StudentDbAccessor
    {
        private readonly IRepository dbRepository;
        private readonly string stTableName;
        private readonly string discTableName;

        public StudentDbAccessor(IRepository dbRepository, string stTableName, string discTableName)
        {
            this.dbRepository = dbRepository;
            this.stTableName = stTableName;
            this.discTableName = discTableName;
        }

        private string DateTimeToString(DateTime date)
        {
            return $"{date.Year}-{date.Month}-{date.Day}";
        }

        #region StudentGetters
        public List<Student> GetAllStudents()
        {
            List<Student> result = dbRepository.GetStudentDataFromTable(stTableName, discTableName);

            return result;
        }

        public Student GetStudentBy(List<KeyValueType> keyValueTypes)
        {
            var student = dbRepository.GetSomeStudentDataFromTable(stTableName,
                discTableName, keyValueTypes)[0];

            return student;
        }

        public Student GetStudentByID(int id)
        {
            var student = GetStudentBy(new List<KeyValueType> {
                new KeyValueType("studentId", id, "number")
                });

            return student;
        }

        public Student GetStudentByPhoneNumber(string phoneNumber)
        {
            var student = GetStudentBy(new List<KeyValueType> {
                new KeyValueType("phoneNumber", phoneNumber)
                });

            return student;
        }

        public Student GetStudentByFLGName(string firtsName, string lastName, string groupName)
        {
            var student = GetStudentBy(new List<KeyValueType> {
                new KeyValueType("firtsName", firtsName),
                new KeyValueType("lastName", lastName),
                new KeyValueType("groupName", groupName)
                });

            return student;
        }

        public int GetStudentId(Student student)
        {
            int studentId = dbRepository.GetStudentId(stTableName, student);

            return studentId;
        }
        #endregion

        #region DisciplineGetters
        public List<DisciplineModel> GetDisciplines(int studentId)
        {
            List<DisciplineModel> result = dbRepository.
                GetDisciplinesBySomeDataFromTable(stTableName, new List<KeyValueType>
            {
                 new KeyValueType("studentId", studentId, "number"),
            });

            return result;
        }

        public DisciplineModel GetDiscipline(int studentId, DateTime startDate,
            DateTime endDate, DisciplineName disciplineName)
        {
            var discipline = dbRepository.GetDisciplinesBySomeDataFromTable(discTableName, new List<KeyValueType>
            {
                 new KeyValueType("studentId", studentId, "number"),
                 new KeyValueType("disciplineName", disciplineName.ToString()),
                 new KeyValueType("startDate", DateTimeToString(startDate)),
                 new KeyValueType("endDate", DateTimeToString(endDate))
            })[0];

            return discipline;
        }

        public List<DisciplineModel> GetStudentDisciplinesByDName(int studentId, DisciplineName disciplineName)
        {
            var disciplines = dbRepository.GetDisciplinesBySomeDataFromTable(discTableName,
                new List<KeyValueType>
            {
                 new KeyValueType("studentId", studentId, "number"),
                 new KeyValueType("disciplineName", disciplineName.ToString())
            });

            return disciplines;
        }

        public int GetdisciplineId(int studentId, DisciplineModel discipline)
        {
            int disciplineId = dbRepository.GetDisciplineId(discTableName,
                studentId, discipline);

            return disciplineId;
        }
        #endregion
    }
}
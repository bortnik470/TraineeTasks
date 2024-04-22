using StudentsInfo.Data.DataModels;
using StudentsInfo.DataModels;
using StudentsInfo.Enums;
using StudentsInfo.Interfaces;

namespace StudentsInfo.Data.FinishedClasses.Student
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

        private StudentModel CreateStudentModel(object[] student)
        {
            return new StudentModel(student[1].ToString(), student[2].ToString(),
                    student[3].ToString(), student[4].ToString(), GetDisciplines((int)student[0]));
        }

        private DisciplineModel CreateDisciplineModel(object[] discipline)
        {
            return new DisciplineModel(
                    (DisciplineName)Enum.Parse(typeof(DisciplineName), discipline[1].ToString()),
                    (Score)Enum.Parse(typeof(Score), discipline[2].ToString()),
                    (DateTime)discipline[3], (DateTime)discipline[4]);
        }

        #region StudentGetters

        public List<StudentModel> GetAllStudents()
        {
            List<StudentModel> result = new List<StudentModel>();
            var students = dbRepository.GetDataFromTable(stTableName);

            foreach (var student in students)
            {
                StudentModel studentModel = CreateStudentModel(student);

                result.Add(studentModel);
            }

            return result;
        }

        public object[] GetStudentObjByPhone(string phoneNumber)
        {
            var student = dbRepository.GetSomeDateFromTable(stTableName, new List<KeyValueType>
                {
                    new KeyValueType("phoneNumber", phoneNumber)
                })[0];

            return student;
        }

        public object[] GetStudentObjByFLGName(string firtsName, string LastName, string groupName)
        {
            var student = dbRepository.GetSomeDateFromTable(stTableName, new List<KeyValueType>
                {
                    new KeyValueType("firstName", firtsName),
                    new KeyValueType("lastName", LastName),
                    new KeyValueType("groupName", groupName)
                })[0];

            return student;
        }

        public StudentModel GetStudentByID(int id)
        {
            var student = dbRepository.GetSomeDateFromTable(stTableName, new List<KeyValueType>
                {
                    new KeyValueType("studentId", id, "number")
                })[0];

            return CreateStudentModel(student);
        }

        public StudentModel GetStudentByPhoneNumber(string phoneNumber)
        {
            var student = GetStudentObjByPhone(phoneNumber);

            return CreateStudentModel(student);
        }

        public StudentModel GetStudentByFLGName(string firtsName, string LastName, string groupName)
        {
            var student = GetStudentObjByFLGName(firtsName, LastName, groupName);

            return CreateStudentModel(student);
        }

        public int GetStudentId(StudentModel student)
        {
            object studentId;

            if (student.phoneNumber != null)
            {
                studentId = GetStudentObjByPhone(student.phoneNumber)[0];
            }
            else
            {
                studentId = GetStudentObjByFLGName(student.firstName, 
                    student.lastName, student.groupName)[0];
            }

            return (int)studentId;
        }
        #endregion

        #region DisciplineGetters
        public List<DisciplineModel> GetDisciplines(int studentId)
        {
            List<DisciplineModel> result = new List<DisciplineModel>();
            var disciplines = dbRepository.GetSomeDateFromTable(discTableName, new List<KeyValueType>
                {
                    new KeyValueType("studentId", studentId, "number")
                });

            foreach (var discipline in disciplines)
            {
                DisciplineModel dis = CreateDisciplineModel(discipline);

                result.Add(dis);
            }

            return result;
        }

        public object[] GetDisciplineObj(int studentId, DateTime startDate,
            DateTime endDate, DisciplineName disciplineName)
        {
            var discipline = dbRepository.GetSomeDateFromTable(stTableName, new List<KeyValueType>
            {
                 new KeyValueType("studentId", studentId, "number"),
                 new KeyValueType("disciplineName", disciplineName.ToString()),
                 new KeyValueType("startDate", DateTimeToString(startDate)),
                 new KeyValueType("endDate", DateTimeToString(endDate))
            })[0];

            return discipline;
        }

        public DisciplineModel GetDiscipline(int studentId, DateTime startDate,
            DateTime endDate, DisciplineName disciplineName)
        {
            var discipline = GetDisciplineObj(studentId, startDate, endDate, disciplineName);

            return CreateDisciplineModel(discipline);
        }

        public List<DisciplineModel> GetStudentDisciplinesByDName(int studentId, DisciplineName disciplineName)
        {
            List<DisciplineModel> result = new List<DisciplineModel>();
            var disciplines = dbRepository.GetSomeDateFromTable(stTableName, new List<KeyValueType>
            {
                 new KeyValueType("studentId", studentId, "number"),
                 new KeyValueType("disciplineName", disciplineName.ToString())
            });

            foreach (var discipline in disciplines)
            {
                DisciplineModel dis = CreateDisciplineModel(discipline);

                result.Add(dis);
            }

            return result;
        }

        public int GetDisciplineId(int studentId, DateTime startDate,
            DateTime endDate, DisciplineName disciplineName)
        {
            var result = GetDisciplineObj(studentId, startDate,
                endDate, disciplineName)[0];

            return (int)result;
        }
        #endregion
    }
}
using StudentsInfo.Data.DataModels;
using StudentsInfo.DataModels;
using StudentsInfo.Enums;
using StudentsInfo.Interfaces;

namespace StudentsInfo.Data.FinishedClasses.Student
{
    public class StudentDbAccessor
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

        #region InsertRegion
        public void AddStudentToDb(StudentModel student)
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
                new KeyValueType (null, discipline.disciplineName.ToString(), null),
                new KeyValueType (null, discipline.score.ToString(), null),
                new KeyValueType (null, DateTimeToString(discipline.startDate), null),
                new KeyValueType (null, DateTimeToString(discipline.endDate), null),
                new KeyValueType (null, studentId, "number")
            });
        }

        public void AddDiscipline(DisciplineModel discipline, StudentModel student)
        {
            int studentId = GetStudentId(student);
            AddDiscipline(discipline, studentId);
        }

        public void AddDisciplines(IEnumerable<DisciplineModel> disciplines, int studentId)
        {
            foreach (var discipline in disciplines)
                AddDiscipline(discipline, studentId);
        }

        public void AddDisciplines(IEnumerable<DisciplineModel> disciplines, StudentModel student)
        {
            int studentId = GetStudentId(student);
            AddDisciplines(disciplines, studentId);
        }
        #endregion

        #region CreatorsRegion
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
        #endregion

        #region Getters
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
            var student = dbRepository.GetSomeDateFromTable(stTableName, new List<KeyValueType>
                {
                    new KeyValueType("phoneNumber", phoneNumber)
                })[0];

            return CreateStudentModel(student);
        }

        public StudentModel GetStudentByFLGName(string firtsName, string LastName, string groupName)
        {
            var student = dbRepository.GetSomeDateFromTable(stTableName, new List<KeyValueType>
                {
                    new KeyValueType("firstName", firtsName),
                    new KeyValueType("lastName", LastName),
                    new KeyValueType("groupName", groupName)
                })[0];

            return CreateStudentModel(student);
        }

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

        public DisciplineModel GetDiscipline(int studentId, DateTime startDate, 
            DateTime endDate, DisciplineName disciplineName)
        {
            var discipline = dbRepository.GetSomeDateFromTable(stTableName, new List<KeyValueType>
            {
                 new KeyValueType("studentId", studentId, "number"),
                 new KeyValueType("disciplineName", disciplineName.ToString()),
                 new KeyValueType("startDate", DateTimeToString(startDate)),
                 new KeyValueType("endDate", DateTimeToString(endDate))
            })[0];

            return CreateDisciplineModel(discipline);
        }

        public List<DisciplineModel> GetStudentDisciplineByDName(int studentId, DisciplineName disciplineName)
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
            var result = dbRepository.GetSomeDateFromTable(stTableName, new List<KeyValueType>
            {
                 new KeyValueType("studentId", studentId, "number"),
                 new KeyValueType("disciplineName", disciplineName.ToString()),
                 new KeyValueType("startDate", DateTimeToString(startDate)),
                 new KeyValueType("endDate", DateTimeToString(endDate))
            })[0][0];

            return (int)result;
        }

        public int GetStudentId(StudentModel student)
        {
            object studentId;

            if (student.phoneNumber != null)
            {
                studentId = dbRepository.GetSomeDateFromTable(stTableName, new List<KeyValueType>
                {
                    new KeyValueType("phoneNumber", student.phoneNumber)
                })[0][0];
            }
            else
            {
                studentId = dbRepository.GetSomeDateFromTable(stTableName, new List<KeyValueType>
                {
                    new KeyValueType("firstName", student.firstName),
                    new KeyValueType("lastName", student.lastName),
                    new KeyValueType("groupName", student.groupName)
                })[0][0];
            }

            return (int)studentId;
        }
        #endregion

        #region DeleteRegion
        public void DeleteStudentById(int id)
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

        #endregion

        #region UpdateRegion

        private void BasicStudentUpdate(int id, List<KeyValueType> setKVT)
        {
            var whereKVT = new List<KeyValueType>
            {
                new KeyValueType("studentId", id, "number")
            };

            dbRepository.UpdateData(stTableName, whereKVT, setKVT);
        }

        private void BasicDisciplineUpdate(int id, List<KeyValueType> setKVT)
        {
            var whereKVT = new List<KeyValueType>
            {
                new KeyValueType("id", id, "number")
            };

            dbRepository.UpdateData(discTableName, whereKVT, setKVT);
        }

        public void UpdateStudentNameInfoById(int id, string firstName, string lastName)
        {
            var setKVT = new List<KeyValueType>
            {
                new KeyValueType("firstName", firstName),
                new KeyValueType("lastName", lastName)
            };

            BasicStudentUpdate(id, setKVT);
        }

        public void UpdateStudentPhoneNumInfoById(int id, string phoneNumber)
        {
            var setKVT = new List<KeyValueType>
            {
                new KeyValueType("phoneNumber", phoneNumber)
            };

            BasicStudentUpdate(id, setKVT);
        }

        public void UpdateStudentGroupNameInfoById(int id, string groupName)
        {
            var setKVT = new List<KeyValueType>
            {
                new KeyValueType("groupName", groupName)
            };

            BasicStudentUpdate(id, setKVT);
        }

        public void UpdateDisciplineDateById(int id, DateTime startDate, DateTime endDate)
        {
            var setKVT = new List<KeyValueType>
            {
                new KeyValueType("startDate", DateTimeToString(startDate)),
                new KeyValueType("endDate", DateTimeToString(endDate))
            };

            BasicDisciplineUpdate(id, setKVT);
        }

        public void UpdateDisciplineScoreById(int id, Score score)
        {
            var setKVT = new List<KeyValueType>
            {
                new KeyValueType("score", score.ToString())
            };

            BasicDisciplineUpdate(id, setKVT);
        }

        #endregion
    }
}

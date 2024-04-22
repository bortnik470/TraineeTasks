using StudentsInfo.Data.DataModels;
using StudentsInfo.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentsInfo.Data.FinishedClasses.Student
{
    public partial class StudentDbAccessor
    {
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
                new KeyValueType (null, discipline.disciplineName.ToString()),
                new KeyValueType (null, discipline.score.ToString()),
                new KeyValueType (null, DateTimeToString(discipline.startDate)),
                new KeyValueType (null, DateTimeToString(discipline.endDate)),
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
    }
}

using StudentsInfo.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentsInfo
{
    public partial class DisADO
    {
        public void DeleteStudent(StudentModel student)
        {
            var id = GetStudentId(student);

            var updRow = dataSet.Tables["Student"].Rows.Find(id);
            updRow.Delete();
        }

        public void DeleteDisciline(StudentModel student, DisciplineModel discipline)
        {
            var id = GetStudentId(student);

            var studentId = GetStudentId(student);

            var disciplineId = GetDisciplineId(studentId, discipline);

            var updRow = dataSet.Tables["Discipline"].Rows.Find(disciplineId);

            updRow.Delete();
        }
    }
}

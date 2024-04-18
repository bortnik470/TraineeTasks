using StudentsInfo.DataModels;

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

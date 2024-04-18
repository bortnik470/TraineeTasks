using StudentsInfo.DataModels;
using System.Data;

namespace StudentsInfo
{
    public partial class DisADO
    {
        public void AddNewStudentRow(StudentModel studentModel)
        {
            if (dataSet.Tables["Student"].AsEnumerable().
                FirstOrDefault(x => x["pNumber"].Equals(studentModel.phoneNumber)) != default)
            {
                Console.WriteLine("Phone number exist");
                return;
            }

            DataRow dataRow = dataSet.Tables["Student"].NewRow();

            dataRow[1] = studentModel.firstName;
            dataRow[2] = studentModel.lastName;
            dataRow[3] = studentModel.phoneNumber;
            dataRow[4] = studentModel.groupName;

            dataSet.Tables["Student"].Rows.Add(dataRow);
        }

        public int GetStudentId(StudentModel studentModel)
        {
            var id = dataSet.Tables["Student"].AsEnumerable().
                Where(x => x["pNumber"].Equals(studentModel.phoneNumber)).
                Select(x => x["id"]).
                FirstOrDefault();

            if (id == default)
            {
                Console.WriteLine("Student with this phone number doesnt exist");
                return -1;
            }

            return (int)id;
        }

        public int GetDisciplineId(int studentId, DisciplineModel discipline)
        {
            var id = dataSet.Tables["Discipline"].AsEnumerable().
                Where(x => x["studentID"].Equals(studentId) &&
                x["dName"].Equals(discipline.disciplineName.ToString()) &&
                x["score"].Equals(discipline.score.ToString())).
                Select(x => x["id"]).
                FirstOrDefault();

            if (id == default)
            {
                Console.WriteLine("Student with this discipline doesnt exist");
                return -1;
            }

            return (int)id;
        }

        public void AddNewDisciplines(StudentModel student)
        {
            int studenId = GetStudentId(student);

            foreach (var discipline in student.disciplines)
            {
                DataRow dataRow = dataSet.Tables["Discipline"].NewRow();
                dataRow[1] = discipline.disciplineName.ToString();
                dataRow[2] = DateOnly.FromDateTime(discipline.startDate);
                dataRow[3] = DateOnly.FromDateTime(discipline.endDate);
                dataRow[4] = discipline.score.ToString();
                dataRow[5] = studenId;

                dataSet.Tables["Discipline"].Rows.Add(dataRow);
            }
        }

        public void UpdateDiscipline(StudentModel studentModel, DisciplineModel discipline, DisciplineModel newDiscipline)
        {
            var studentId = GetStudentId(studentModel);

            var disciplineId = GetDisciplineId(studentId, discipline);

            var updRow = dataSet.Tables["Discipline"].Rows.Find(disciplineId);

            updRow["startTime"] = newDiscipline.startDate.Date;
            updRow["endTime"] = newDiscipline.endDate.Date;
            updRow["score"] = newDiscipline.score.ToString();
        }

        public void UpdateStudent(StudentModel studentModel, StudentModel newStudentModel)
        {
            var studentId = GetStudentId(studentModel);

            var updRow = dataSet.Tables["Student"].Rows.Find(studentId);

            updRow["fName"] = newStudentModel.firstName;
            updRow["lName"] = newStudentModel.lastName;
            updRow["pNumber"] = newStudentModel.phoneNumber;
            updRow["gNumber"] = newStudentModel.groupName;
        }
    }
}

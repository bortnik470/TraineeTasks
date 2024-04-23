using System.Data;
using System.Data.SqlClient;

namespace ADO_Net_demo.DAL
{
    internal class DataAccessDisconnected : IStudentsRepo
    {
        private DataSet dataSet;

        public DataAccessDisconnected(string connString)
        {
            dataSet = new DataSet("University");
            using (var connection = new SqlConnection(connString))
            {
                string sqlCom = "select * from students";

                connection.Open();
                var command = new SqlCommand(sqlCom, connection);
                var adapter = new SqlDataAdapter(command);

                adapter.Fill(dataSet, "Students");

                sqlCom = "select * from courses";
                command.CommandText = sqlCom;

                adapter.Fill(dataSet, "Courses");
            }
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Student GetById(int id)
        {
            var studentRow = dataSet.Tables["Students"].AsEnumerable().
                Where(x => x["studentId"].Equals(id)).Single();

            int studentId = (int)studentRow["studentId"];
            string? firstName = studentRow["firstName"].ToString();
            string? lastName = studentRow["lastName"].ToString();
            string? phoneNumber = studentRow["phoneNumber"].ToString();
            string? groupName = studentRow["groupName"].ToString();

            List<Course> courses = new List<Course>();
            var coursesRow = dataSet.Tables["Courses"].AsEnumerable().
                Where(x => x["studentId"].Equals(id));

            foreach (var course in coursesRow)
            {
                int courseId = (int)course["courseId"];
                string? courseName = course["courseName"].ToString();
                string? score = course["score"].ToString();
                DateOnly startDate = DateOnly.FromDateTime((DateTime)course["startDate"]);
                DateOnly endDate = DateOnly.FromDateTime((DateTime)course["endDate"]);

                courses.Add(new Course(courseId, studentId, courseName, score, startDate, endDate));
            }

            Student result = new Student(studentId, firstName, lastName, phoneNumber, groupName, courses);

            return result;
        }

        public List<Student> GetList()
        {
            throw new NotImplementedException();
        }

        public Student Insert(Student student)
        {
            throw new NotImplementedException();
        }

        public Student Update(Student student)
        {
            throw new NotImplementedException();
        }
    }
}

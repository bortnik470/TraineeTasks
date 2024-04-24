using System.Data;
using System.Data.SqlClient;

namespace ADO_Net_demo.DAL
{
    internal class DataAccessDisconnected : IStudentsRepo
    {
        private DataSet dataSet;
        private DataRelation studentCourseRelation;

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

                dataSet.Tables["Students"].Columns["studentId"].
                    Unique = true;
                dataSet.Tables["Students"].Columns["studentId"].
                    AutoIncrement = true;
                dataSet.Tables["Students"].Columns["studentId"].
                    AutoIncrementSeed = (int)dataSet.Tables["Students"].
                        Rows[dataSet.Tables["Students"].Rows.Count - 1]["studentId"] + 1;


                dataSet.Tables["Courses"].Columns["courseId"].
                    Unique = true;
                dataSet.Tables["Courses"].Columns["courseId"].
                    AutoIncrement = true;
                dataSet.Tables["Courses"].Columns["courseId"].
                    AutoIncrementSeed = (int)dataSet.Tables["Courses"].
                        Rows[dataSet.Tables["Courses"].Rows.Count - 1]["courseId"] + 1;

                studentCourseRelation = dataSet.Relations.Add(
                    dataSet.Tables["Students"].Columns["studentId"],
                    dataSet.Tables["Courses"].Columns["studentId"]);
            }
        }

        public void Delete(int id)
        {
            dataSet.Tables["Students"].AsEnumerable().
                Where(x => x["studentId"].Equals(id)).
                Single().Delete();
        }

        public Student GetById(int id)
        {
            var studentRow = dataSet.Tables["Students"].AsEnumerable().
                Where(x => x["studentId"].Equals(id)).Single();

            var student = CreateStudent(studentRow);

            return student;
        }

        public List<Student> GetList()
        {
            var studentRows = dataSet.Tables["Students"].Select();

            List<Student> students = new List<Student>();

            foreach (var row in studentRows)
            {
                students.Add(CreateStudent(row));
            }

            return students;
        }

        public Student Insert(Student student)
        {
            var studentRow = dataSet.Tables["Students"].NewRow();

            studentRow = SetStudentField(studentRow, student);

            dataSet.Tables["Students"].Rows.Add(studentRow);

            int studentId = (int)dataSet.Tables["Students"].
                Rows[dataSet.Tables["Students"].Rows.Count - 1]["studentId"];

            student.Id = studentId;

            foreach (var course in student.Courses)
            {
                var courseRow = dataSet.Tables["Courses"].NewRow();

                course.StudentId = studentId;

                courseRow = SetCourseField(courseRow, course);

                dataSet.Tables["Courses"].Rows.Add(courseRow);

                int courseId = (int)dataSet.Tables["Courses"].
                    Rows[dataSet.Tables["Courses"].Rows.Count - 1]["courseId"];

                course.Id = courseId;
            }

            return student;
        }

        public Student Update(Student student)
        {
            var studentRow = dataSet.Tables["Students"].AsEnumerable().
                Where(x => x["studentId"].Equals(student.Id)).Single();

            studentRow.BeginEdit();
            try
            {
                SetStudentField(studentRow, student);
                foreach (var course in student.Courses)
                {
                    var courseRow = dataSet.Tables["Courses"].AsEnumerable().
                        Where(x => x["courseId"].Equals(course.Id)).FirstOrDefault();

                    if (courseRow == null)
                    {
                        AddCourse(course, student.Id);
                    }
                    else
                    {
                        courseRow.BeginEdit();

                        try
                        {
                            SetCourseField(courseRow, course);
                            courseRow.AcceptChanges();
                        }
                        catch (Exception ex)
                        {
                            courseRow.CancelEdit();
                            throw new Exception(ex.Message);
                        }
                        finally { courseRow.EndEdit(); }
                    }
                }
                studentRow.AcceptChanges();
            }
            catch (Exception ex)
            {
                studentRow.CancelEdit();
                throw new Exception(ex.Message);
            }
            finally { studentRow.EndEdit(); }

            return student;
        }

        public Course AddCourse(Course course, int studentId)
        {
            var courseRow = dataSet.Tables["Courses"].NewRow();

            course.StudentId = studentId;

            courseRow = SetCourseField(courseRow, course);

            dataSet.Tables["Courses"].Rows.Add(courseRow);

            int courseId = (int)dataSet.Tables["Courses"].
                Rows[dataSet.Tables["Courses"].Rows.Count - 1]["courseId"];

            course.Id = courseId;

            return course;
        }

        private Student CreateStudent(DataRow studentRow)
        {
            int studentId = (int)studentRow["studentId"];
            string? firstName = studentRow["firstName"].ToString();
            string? lastName = studentRow["lastName"].ToString();
            string? phoneNumber = studentRow["phoneNumber"].ToString();
            string? groupName = studentRow["groupName"].ToString();

            List<Course> courses = new List<Course>();

            foreach (var course in studentRow.GetChildRows(studentCourseRelation))
            {
                int courseId = (int)course["courseId"];
                string? courseName = course["courseName"].ToString();
                string? score = course["score"].ToString();
                DateOnly startDate = DateOnly.FromDateTime((DateTime)course["startDate"]);
                DateOnly endDate = DateOnly.FromDateTime((DateTime)course["endDate"]);

                courses.Add(new Course(courseId, studentId, courseName, score, startDate, endDate));
            }

            Student student = new Student(studentId, firstName, lastName, phoneNumber, groupName, courses);

            return student;
        }

        private DataRow SetStudentField(DataRow studentRow, Student student)
        {
            studentRow.SetField(1, student.FirstName);
            studentRow.SetField(2, student.LastName);
            studentRow.SetField(3, student.PhoneNumber);
            studentRow.SetField(4, student.GroupName);

            return studentRow;
        }

        private DataRow SetCourseField(DataRow courseRow, Course course)
        {
            courseRow.SetField(1, course.Name);
            courseRow.SetField(2, course.Score);
            courseRow.SetField(3, course.StartDate.
                ToDateTime(TimeOnly.MinValue));
            courseRow.SetField(4, course.EndDate.
                ToDateTime(TimeOnly.MinValue));
            courseRow.SetField(5, course.StudentId);

            return courseRow;
        }
    }
}

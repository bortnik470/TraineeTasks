using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ADO_Net_demo.DAL
{
    internal class DataAccessDisconnected : IStudentsRepo
    {
        private DataSet DataSet;
        private DataRelation StudentCourseRelation;

        private SqlDataAdapter StudentsDataAdapter;
        private SqlDataAdapter CoursesDataAdapter;

        private SqlCommandBuilder StudentsCommandBuilder;
        private SqlCommandBuilder CoursesCommandBuilder;

        public DataAccessDisconnected()
        {
            var connString = ConfigurationManager.ConnectionStrings["SqlConnectionString"].ConnectionString;
            DataSet = new DataSet("University");
            using (var connection = new SqlConnection(connString))
            {
                var studentSqlCom = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "selectStudent",
                    Connection = connection
                };

                studentSqlCom.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@studentId",
                    Direction = ParameterDirection.Output,
                    DbType = DbType.Int32
                });

                var coursesSqlCom = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "selectCourses",
                    Connection = connection
                };

                coursesSqlCom.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@courseId",
                    Direction = ParameterDirection.Output,
                    DbType = DbType.Int32
                });

                connection.Open();
                studentSqlCom.ExecuteNonQuery();
                coursesSqlCom.ExecuteNonQuery();

                StudentsDataAdapter = new SqlDataAdapter("select * from students", connString);
                CoursesDataAdapter = new SqlDataAdapter("select * from Courses", connString);

                StudentsCommandBuilder = new SqlCommandBuilder(StudentsDataAdapter);
                CoursesCommandBuilder = new SqlCommandBuilder(CoursesDataAdapter);

                StudentsDataAdapter.Fill(DataSet, "Students");

                CoursesDataAdapter.Fill(DataSet, "Courses");

                DataSet.Tables["Students"].Columns["studentId"].
                    Unique = true;
                DataSet.Tables["Students"].Columns["studentId"].
                    AutoIncrement = true;
                DataSet.Tables["Students"].Columns["studentId"].
                    AutoIncrementStep = 1;
                DataSet.Tables["Students"].Columns["studentId"].
                    AutoIncrementSeed = (int)studentSqlCom.Parameters["@studentId"].Value + 1;


                DataSet.Tables["Courses"].Columns["courseId"].
                    Unique = true;
                DataSet.Tables["Courses"].Columns["courseId"].
                    AutoIncrement = true;
                DataSet.Tables["Courses"].Columns["courseId"].
                    AutoIncrementStep = 1;
                DataSet.Tables["Courses"].Columns["courseId"].
                    AutoIncrementSeed = (int)coursesSqlCom.Parameters["@courseId"].Value + 1;


                StudentCourseRelation = DataSet.Relations.Add(
                    DataSet.Tables["Students"].Columns["studentId"],
                    DataSet.Tables["Courses"].Columns["studentId"]);
            }
        }

        public void Delete(int id)
        {
            var rowToDelete = DataSet.Tables["Students"].AsEnumerable().
                Where(x => x["studentId"].Equals(id)).
                Single();

            foreach (DataRow row in rowToDelete.GetChildRows(StudentCourseRelation))
            {
                row.Delete();
            }
            rowToDelete.Delete();

            CoursesDataAdapter.Update(DataSet, "Courses");
            StudentsDataAdapter.Update(DataSet, "Students");
        }

        public Student GetById(int id)
        {
            var studentRow = DataSet.Tables["Students"].AsEnumerable().
                Where(x => x["studentId"].Equals(id)).Single();

            var student = CreateStudent(studentRow);

            return student;
        }

        public List<Student> GetList()
        {
            var studentRows = DataSet.Tables["Students"].Select();

            List<Student> students = new List<Student>();

            foreach (var row in studentRows)
            {
                students.Add(CreateStudent(row));
            }

            return students;
        }

        public Student Insert(Student student)
        {
            var studentRow = DataSet.Tables["Students"].NewRow();

            studentRow = SetStudentField(studentRow, student);

            DataSet.Tables["Students"].Rows.Add(studentRow);

            int studentId = (int)DataSet.Tables["Students"].
                Rows[DataSet.Tables["Students"].Rows.Count - 1]["studentId"];

            student.StudentId = studentId;

            foreach (var course in student.Courses)
            {
                var courseRow = DataSet.Tables["Courses"].NewRow();

                course.StudentId = studentId;

                courseRow = SetCourseField(courseRow, course);

                DataSet.Tables["Courses"].Rows.Add(courseRow);

                int courseId = (int)DataSet.Tables["Courses"].
                    Rows[DataSet.Tables["Courses"].Rows.Count - 1]["courseId"];

                course.CourseId = courseId;
            }

            StudentsDataAdapter.Update(DataSet, "Students");
            CoursesDataAdapter.Update(DataSet, "Courses");

            return student;
        }

        public Student Update(Student student)
        {
            var studentRow = DataSet.Tables["Students"].AsEnumerable().
                Where(x => x["studentId"].Equals(student.StudentId)).Single();

            studentRow.BeginEdit();
            try
            {
                SetStudentField(studentRow, student);
                foreach (var course in student.Courses)
                {
                    var courseRow = DataSet.Tables["Courses"].AsEnumerable().
                        Where(x => x["courseId"].Equals(course.CourseId)).FirstOrDefault();

                    if (courseRow == null)
                    {
                        AddCourse(course, student.StudentId);
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
                StudentsDataAdapter.Update(DataSet, "Students");
                CoursesDataAdapter.Update(DataSet, "Courses");
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
            var courseRow = DataSet.Tables["Courses"].NewRow();

            course.StudentId = studentId;

            courseRow = SetCourseField(courseRow, course);

            DataSet.Tables["Courses"].Rows.Add(courseRow);

            int courseId = (int)DataSet.Tables["Courses"].
                Rows[DataSet.Tables["Courses"].Rows.Count - 1]["courseId"];

            course.CourseId = courseId;

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

            foreach (var course in studentRow.GetChildRows(StudentCourseRelation))
            {
                int courseId = (int)course["courseId"];
                string? courseName = course["courseName"].ToString();
                string? score = course["score"].ToString();
                DateOnly startDate = DateOnly.FromDateTime((DateTime)course["startDate"]);
                DateOnly endDate = DateOnly.FromDateTime((DateTime)course["endDate"]);

                courses.Add(new Course(courseId, courseName, score, startDate, endDate, studentId));
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
            courseRow.SetField(1, course.CourseName);
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
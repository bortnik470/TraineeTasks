using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ADO_Net_demo.DAL
{
    internal class DataAccessConnected : IStudentsRepo
    {
        private const string stdTableName = "Students";
        private const string coursesTableName = "Courses";

        private string connString = ConfigurationManager.ConnectionStrings["SqlConnectionString"].ConnectionString;

        public void Delete(int id)
        {
            string sqlCom = $"Delete From {coursesTableName} Where studentId = @studentId";

            using (SqlConnection cn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand(sqlCom, cn);

                cmd.Parameters.AddWithValue("@studentId", id);

                cn.Open();

                SqlTransaction sqlTransaction = cn.BeginTransaction();
                try
                {
                    cmd.Transaction = sqlTransaction;

                    cmd.ExecuteNonQuery();

                    cmd.CommandText = $"Delete From {stdTableName} Where studentId = @studentId";

                    cmd.ExecuteNonQuery();

                    sqlTransaction.Commit();
                }
                catch (Exception ex)
                {
                    sqlTransaction.Rollback();
                    throw new Exception(ex.Message);
                }
                finally
                {
                    sqlTransaction.Dispose();
                }
            }
        }

        public Student GetById(int id)
        {
            string sqlCom = $"select * from {coursesTableName} where studentId = @studentId";

            Student student = default;

            List<Course> courses = new List<Course>();

            using (SqlConnection cn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand(sqlCom.ToString(), cn);

                cn.Open();

                cmd.Parameters.AddWithValue("@studentId", id);

                using (SqlDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        int courseId = dataReader.GetInt32(0);
                        string courseName = dataReader.GetString(1);
                        string score = dataReader.GetString(2);
                        DateOnly startDate = DateOnly.FromDateTime(dataReader.GetDateTime(3));
                        DateOnly endDate = DateOnly.FromDateTime(dataReader.GetDateTime(4));

                        courses.Add(new Course(courseId, courseName, score, startDate, endDate));
                    }
                }

                cmd.CommandText = $"select * from {stdTableName} where studentId = @studentId";

                using (SqlDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        int studentId = dataReader.GetInt32(0);
                        string firstName = dataReader.GetString(1);
                        string lastName = dataReader.GetString(2);
                        string phoneNumber = dataReader.GetString(3);
                        string groupName = dataReader.GetString(4);

                        student = new Student(studentId, firstName, lastName, phoneNumber, groupName, courses);
                    }
                }
            }

            return student;
        }

        public List<Student> GetList()
        {
            string sqlCom = $"select * from {coursesTableName}";

            List<Student> students = new List<Student>();

            List<Course> courses = new List<Course>();

            using (SqlConnection sqlConnection = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand(sqlCom.ToString(), sqlConnection);

                sqlConnection.Open();

                using (SqlDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        int courseId = dataReader.GetInt32(0);
                        int studentId = dataReader.GetInt32(5);
                        string courseName = dataReader.GetString(1);
                        string score = dataReader.GetString(2);
                        DateOnly startDate = DateOnly.FromDateTime(dataReader.GetDateTime(3));
                        DateOnly endDate = DateOnly.FromDateTime(dataReader.GetDateTime(4));

                        courses.Add(new Course(courseId, courseName, score, startDate, endDate, studentId));
                    }
                }

                cmd.CommandText = $"select * from {stdTableName}";

                using (SqlDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        int studentId = dataReader.GetInt32(0);
                        string firstName = dataReader.GetString(1);
                        string lastName = dataReader.GetString(2);
                        string phoneNumber = dataReader.GetString(3);
                        string groupName = dataReader.GetString(4);

                        students.Add(new Student(studentId, firstName, lastName,
                            phoneNumber, groupName, courses.
                            Where(x => x.StudentId.Equals(studentId)).ToList()));
                    }
                }
            }

            return students;
        }

        public Student Insert(Student student)
        {
            string sqlCom = $"Insert into {stdTableName} values (@firstName, @lastName, @phoneNumber, @groupName);";

            using (SqlConnection cn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand(sqlCom.ToString(), cn);

                cmd.Parameters.AddRange([
                    new SqlParameter("@firstName", student.FirstName),
                    new SqlParameter("@lastName", student.LastName),
                    new SqlParameter("@phoneNumber", student.PhoneNumber),
                    new SqlParameter("@groupName", student.GroupName)
                    ]);

                cn.Open();
                cmd.ExecuteNonQuery();

                student.StudentId = GetLastStudentId();

                sqlCom = $"Insert into {coursesTableName} values (@courseName, @score, @startDate, @endDate, @studentId)";

                cmd.CommandText = sqlCom;

                foreach (var course in student.Courses)
                {
                    string startDate = DateOnlyToSqlString(course.StartDate);
                    string endDate = DateOnlyToSqlString(course.EndDate);

                    cmd.Parameters.AddRange([
                            new SqlParameter("@courseName", course.CourseName),
                            new SqlParameter("@score", course.Score),
                            new SqlParameter("@startDate", startDate),
                            new SqlParameter("@endDate", endDate),
                            new SqlParameter("@studentId", student.StudentId)
                    ]);

                    course.StudentId = student.StudentId;

                    cmd.ExecuteNonQuery();

                    cmd.Parameters.Clear();
                }
            }

            return student;
        }

        public Student Update(Student student)
        {
            string sqlCom = $"Update {stdTableName} set firstName = @firstName, " +
                $"lastName = @lastName, phoneNumber = @phoneNumber, groupName = @groupName " +
                $"where studentId = @studentId";

            using (SqlConnection cn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand(sqlCom, cn);

                cn.Open();

                cmd.Parameters.AddRange([
                    new SqlParameter("@firstName", student.FirstName),
                    new SqlParameter("@lastName", student.LastName),
                    new SqlParameter("@phoneNumber", student.PhoneNumber),
                    new SqlParameter("@groupName", student.GroupName),
                    new SqlParameter("@studentId", student.StudentId)
                    ]);

                SqlTransaction sqlTransaction = cn.BeginTransaction();
                cmd.Transaction = sqlTransaction;

                try
                {
                    cmd.ExecuteNonQuery();

                    UpdateCourse(student.Courses.ToList(), student.StudentId, cmd);

                    sqlTransaction.Commit();
                }
                catch (Exception ex)
                {
                    sqlTransaction.Rollback();
                    throw new Exception(ex.Message);
                }
                finally
                {
                    sqlTransaction.Dispose();
                }
            }

            return student;
        }

        private void UpdateCourse(List<Course> courses, int studentId, SqlCommand cmd)
        {
            foreach (var course in courses)
            {
                cmd.Parameters.Clear();

                int courseId = course.CourseId;
                string courseName = course.CourseName;
                string score = course.Score;
                string startDate = DateOnlyToSqlString(course.StartDate);
                string endDate = DateOnlyToSqlString(course.EndDate);

                cmd.Parameters.AddRange([
                        new SqlParameter("@courseName", courseName),
                            new SqlParameter("@score", score),
                            new SqlParameter("@startDate", startDate),
                            new SqlParameter("@endDate", endDate),
                            new SqlParameter("@studentId", studentId),
                            new SqlParameter("@courseId", courseId)
                    ]);


                if (courseId == default)
                {
                    cmd.CommandText = $"Insert table {coursesTableName} values (courseName = @courseName, " +
                                  $"@courseName, @startDate, @endDate, @studentId)";

                    course.CourseId = GetLastCourseId();
                }
                else
                {
                    cmd.CommandText = $"Update {coursesTableName} set courseName = @courseName, " +
                                  $"score = @score, startDate = @startDate, " +
                                  $"endDate = @endDate where courseId = @courseId;";
                }

                cmd.ExecuteNonQuery();
            }
        }

        private int GetLastCourseId()
        {
            int courseId;

            string sqlCom = $"select max(studentId) from {coursesTableName}";

            using (SqlConnection cn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand(sqlCom, cn);

                cn.Open();

                courseId = int.Parse(cmd.ExecuteScalar().ToString());
            }

            return courseId;
        }

        private int GetLastStudentId()
        {
            int studentId;

            string sqlCom = $"select max(studentId) from {stdTableName}";

            using (SqlConnection cn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand(sqlCom, cn);

                cn.Open();

                studentId = int.Parse(cmd.ExecuteScalar().ToString());
            }

            return studentId;
        }

        private string DateOnlyToSqlString(DateOnly date)
        {
            var sqlString = $"{date.Year}-{date.Month}-{date.Day}";

            return sqlString;
        }
    }
}
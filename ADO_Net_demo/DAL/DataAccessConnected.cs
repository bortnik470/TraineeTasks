using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO_Net_demo.DAL
{
    internal class DataAccessConnected : IStudentsRepo
    {
        private const string stdTableName = "Students";
        private const string coursesTableName = "Courses";

        private string connString = ConfigurationManager.ConnectionStrings["SqlConnectionString"].ConnectionString;

        public void Delete(int id)
        {
            string sqlCom = $"Delete From {coursesTableName} Where studentId = {id}";

            using (SqlConnection cn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand(sqlCom, cn);

                cn.Open();

                SqlTransaction sqlTransaction = cn.BeginTransaction();
                try
                {
                    cmd.Transaction = sqlTransaction;

                    cmd.ExecuteNonQuery();

                    cmd.CommandText = $"Delete From {stdTableName} Where studentId = {id}";

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
            string sqlCom = $"select * from {coursesTableName} " +
                $"where studentId = {id}";

            Student student = default;

            List<Course> courses = new List<Course>();

            using (SqlConnection cn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand(sqlCom.ToString(), cn);

                cn.Open();

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

                cmd.CommandText = $"select * from {stdTableName} " +
                $"where studentId = {id}";

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

                        courses.Add(new Course(courseId, studentId, courseName, score, startDate, endDate));
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
                            Where(x => x.studentId.Equals(studentId)).ToList()));
                    }
                }
            }

            return students;
        }

        public Student Insert(Student student)
        {
            string sqlCom = $"Insert into {stdTableName} values (" +
                $"'{student.FirstName}', '{student.LastName}', " +
                $"'{student.PhoneNumber}', '{student.GroupName}');";

            StringBuilder sb = new StringBuilder();

            using (SqlConnection cn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand(sqlCom.ToString(), cn);

                cn.Open();

                cmd.ExecuteNonQuery();

                student.Id = GetLastStudentId();

                sb.Append($"Insert into {coursesTableName} values ");
                foreach (var course in student.Courses)
                {
                    sb.Append($"('{course.Name}', '{course.Score}', " +
                        $"'{DateOnlyToSqlString(course.StartDate)}', " +
                        $"'{DateOnlyToSqlString(course.EndDate)}', " +
                        $"{student.Id}), ");
                }
                sb.Remove(sb.Length - 2, 1);

                cmd.CommandText = sb.ToString();

                cmd.ExecuteNonQuery();
            }

            return student;
        }

        public Student Update(Student student)
        {
            string sqlCom = $"Update {stdTableName} set " +
                $"firstName = '{student.FirstName}', " +
                $"lastName = '{student.LastName}', " +
                $"phoneNumber = '{student.PhoneNumber}', " +
                $"groupName = '{student.GroupName}' " +
                $"where studentId = {student.Id}";

            using (SqlConnection cn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand(sqlCom, cn);

                cn.Open();

                SqlTransaction sqlTransaction = cn.BeginTransaction();
                cmd.Transaction = sqlTransaction;

                try
                {
                    cmd.ExecuteNonQuery();

                    UpdateCourse(student.Courses, cmd);

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

        private void UpdateCourse(List<Course> courses, SqlCommand cmd)
        {
            foreach (var course in courses)
            {
                int courseId = course.Id;
                string courseName = course.Name;
                string score = course.Score;
                string startDate = DateOnlyToSqlString(course.StartDate);
                string endDate = DateOnlyToSqlString(course.EndDate);

                if (courseId == default)
                {
                    cmd.CommandText = $"Insert table {coursesTableName} values (" +
                                  $"courseName = '{courseName}', " +
                                  $"score = '{score}', " +
                                  $"startDate = '{startDate}', " +
                                  $"endDate = '{endDate}')";
                }
                else
                {
                    cmd.CommandText = $"Update {coursesTableName} set " +
                                      $"courseName = '{courseName}', " +
                                      $"score = '{score}', " +
                                      $"startDate = '{startDate}', " +
                                      $"endDate = '{endDate}' " +
                                      $"where courseId = {courseId};";
                }

                cmd.ExecuteNonQuery();
            }
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
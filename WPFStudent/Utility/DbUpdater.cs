using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFStudent.Models;

namespace WPFStudent.Utility
{
    public class DbUpdater
    {
        private readonly string ConnectionString =
                   ConfigurationManager.ConnectionStrings["SqlConnectionString"].ConnectionString;

        public void UpdateDb(IEnumerable<StudentModel> students, IEnumerable<CourseModel> courses)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;

                connection.Open();

                foreach (var student in students)
                {
                    if (student.IsNew)
                        InsertStudent(student, cmd);
                    else if (student.IsUpdated)
                        UpdateStudent(student, cmd);
                }

                foreach (var course in courses)
                {
                    if (course.IsNew)
                        InsertCourse(course, cmd);
                    else if (course.IsUpdated)
                        UpdateCourse(course, cmd);
                }
            }
        }

        private void InsertCourse(CourseModel course, SqlCommand cmd)
        {
            string sqlCom = @"Insert into courses values(@courseName, @score, @startDate, @endDate, @studentId)";

            cmd.CommandText = sqlCom;
            cmd.Parameters.Clear();

            cmd.Parameters.AddRange(GetCourseParameters(course));

            cmd.ExecuteNonQuery();
        }

        private void UpdateCourse(CourseModel course, SqlCommand cmd)
        {
            string sqlCom = @$"Update Courses set studentId = @studentId, courseName = @courseName, score = @score, startDate = @startDate, endDate = @endDate where courseId = @courseId";

            cmd.CommandText = sqlCom;
            cmd.Parameters.Clear();

            cmd.Parameters.AddRange(GetCourseParameters(course));

            cmd.ExecuteNonQuery();
        }

        private void UpdateStudent(StudentModel student, SqlCommand cmd)
        {
            string sqlCom = @"Update Students set firstName = @firstName, lastName = @lastName, phoneNumber = @phoneNumber, groupName = @groupName where studentId = @studentId";

            cmd.CommandText = sqlCom;
            cmd.Parameters.Clear();

            cmd.Parameters.AddRange(GetStudentParameters(student));

            cmd.ExecuteNonQuery();
        }

        private void InsertStudent(StudentModel student, SqlCommand cmd)
        {
            string sqlCom = @"Insert into students values(@firstName, @lastName, @phoneNumber, @groupName)";

            cmd.CommandText = sqlCom;
            cmd.Parameters.Clear();

            cmd.Parameters.AddRange(GetStudentParameters(student));

            cmd.ExecuteNonQuery();
        }

        private SqlParameter[] GetStudentParameters(StudentModel student)
        {
            int studentId = student.StudentId;
            string firstName = student.FirstName;
            string lastName = student.LastName;
            string phoneNumber = student.PhoneNumber;
            string groupName = student.GroupName;

            return
            [

                new SqlParameter
                {
                    SqlDbType = System.Data.SqlDbType.Int,
                    ParameterName = "@studentId",
                    Value = studentId
                },
                new SqlParameter
                {
                    SqlDbType = System.Data.SqlDbType.VarChar,
                    ParameterName = "@firstName",
                    Value = firstName
                },
                new SqlParameter
                {
                    SqlDbType = System.Data.SqlDbType.VarChar,
                    ParameterName = "@lastName",
                    Value = lastName
                },
                new SqlParameter
                {
                    SqlDbType = System.Data.SqlDbType.VarChar,
                    ParameterName = "@phoneNumber",
                    Value = phoneNumber
                },
                new SqlParameter
                {
                    SqlDbType = System.Data.SqlDbType.VarChar,
                    ParameterName = "@groupName",
                    Value = groupName
                }
            ];
        }

        private SqlParameter[] GetCourseParameters(CourseModel course)
        {
            return
            [
                new SqlParameter
                {
                    SqlDbType = System.Data.SqlDbType.Int,
                    ParameterName = "@courseId",
                    Value = course.CourseId
                },
                new SqlParameter
                {
                    SqlDbType = System.Data.SqlDbType.Int,
                    ParameterName = "@studentId",
                    Value = course.StudentId
                },
                new SqlParameter
                {
                    SqlDbType = System.Data.SqlDbType.VarChar,
                    ParameterName = "@courseName",
                    Value = course.CourseName
                },
                new SqlParameter
                {
                    SqlDbType = System.Data.SqlDbType.VarChar,
                    ParameterName = "@score",
                    Value = course.Score
                },
                new SqlParameter
                {
                    SqlDbType = System.Data.SqlDbType.Date,
                    ParameterName = "@startDate",
                    Value = DateTime.Parse(course.StartDate.ToString())
                },
                new SqlParameter
                {
                    SqlDbType = System.Data.SqlDbType.Date,
                    ParameterName = "@endDate",
                    Value = DateTime.Parse(course.EndDate.ToString())
                }
            ];
        }
    }
}

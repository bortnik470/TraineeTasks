using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using WPFStudent.Models;

namespace WPFStudent.Utility
{
    public class StudentDbLoader : IStudentLoader
    {
        private readonly string ConnectionString =
           ConfigurationManager.ConnectionStrings["SqlConnectionString"].ConnectionString;

        public async Task<IEnumerable<StudentModel>> LoadAsync()
        {
            string sqlCom = @"Select * from students";

            var students = new List<StudentModel>();
            var courses = new List<CourseModel>();

            using (SqlConnection cn = new SqlConnection(ConnectionString))
            {
                cn.Open();

                SqlCommand cmd = new SqlCommand(sqlCom, cn);

                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        int Id = reader.GetInt32(0);
                        string FirstName = reader.GetString(1);
                        string LastName = reader.GetString(2);
                        string PhoneNumber = reader.GetString(3);
                        string GroupName = reader.GetString(4);

                        var student = new StudentModel()
                        {
                            Id = Id,
                            FirstName = FirstName,
                            LastName = LastName,
                            PhoneNumber = PhoneNumber,
                            GroupName = GroupName,
                        };

                        students.Add(student);
                    }
                }

                sqlCom = @"Select * from courses";

                cmd.CommandText = sqlCom;

                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        int CourseId = reader.GetInt32(0);
                        int StudentId = reader.GetInt32(5);
                        string CourseName = reader.GetString(1);
                        string Score = reader.GetString(2);
                        DateOnly StartDate = DateOnly.FromDateTime(reader.GetDateTime(3));
                        DateOnly EndDate = DateOnly.FromDateTime(reader.GetDateTime(4));

                        var course = new CourseModel()
                        {
                            CourseId = CourseId,
                            StudentId = StudentId,
                            CourseName = CourseName,
                            Score = Score,
                            StartDate = StartDate,
                            EndDate = EndDate
                        };

                        courses.Add(course);
                    }
                }
            }

            foreach (var student in students)
            {
                student.Courses = courses.
                    Where(x => x.StudentId.Equals(student.Id)).
                    ToList();
            }

            return students;
        }
    }
}

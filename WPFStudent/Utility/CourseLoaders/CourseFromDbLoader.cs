using System.Configuration;
using System.Data.SqlClient;
using WPFStudent.Models;

namespace WPFStudent.Utility.CourseLoaders
{
    public class CourseFromDbLoader : ICourseLoader
    {
        private readonly string ConnectionString =
           ConfigurationManager.ConnectionStrings["SqlConnectionString"].ConnectionString;

        public async Task<IEnumerable<CourseModel>> LoadAsync()
        {
            string sqlCom = @"Select * from courses";

            List<CourseModel> courses = new List<CourseModel>();

            using (SqlConnection cn = new SqlConnection(ConnectionString))
            {
                cn.Open();

                SqlCommand cmd = new SqlCommand(sqlCom, cn);

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

            return courses;
        }
    }
}

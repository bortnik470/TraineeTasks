using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using WPFStudent.Models;

namespace WPFStudent.Utility.StudentLoaders
{
    public class StudentFromDbLoader : IStudentLoader
    {
        private readonly string ConnectionString =
           ConfigurationManager.ConnectionStrings["SqlConnectionString"].ConnectionString;

        public async Task<IEnumerable<StudentModel>> LoadAsync()
        {
            string sqlCom = @"Select * from students";

            var students = new List<StudentModel>();

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
                            StudentId = Id,
                            FirstName = FirstName,
                            LastName = LastName,
                            PhoneNumber = PhoneNumber,
                            GroupName = GroupName,
                        };

                        students.Add(student);
                    }
                }
            }

            return students;
        }

        public int GetLastStudentId()
        {
            string sqlCom = "GetLastStudentId";

            int result = 0;

            using (SqlConnection cn = new SqlConnection(ConnectionString))
            {
                cn.Open();

                SqlCommand cmd = new SqlCommand(sqlCom, cn);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter()
                {
                    SqlDbType = SqlDbType.Int,
                    ParameterName = "@studentId",
                    Direction = ParameterDirection.Output
                });

                cmd.ExecuteNonQuery();

                result = int.Parse(cmd.Parameters[0].Value.ToString());
            }

            return result;
        }
    }
}

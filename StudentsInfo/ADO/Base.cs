using StudentsInfo.DataModels;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace StudentsInfo.ADO
{
    public class Base
    {
        string connectionString = ConfigurationManager.ConnectionStrings[0].ConnectionString;

        public void PrintAllData()
        {

            var sql = "SELECT * FROM Student";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand(sql, connection);

                using (DbDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Console.WriteLine(dr[0]);
                    }
                }
            }
        }

        public void PrintSpecificData()
        {
            SqlParameter sqlParameter = new SqlParameter("@PhoneNumber", SqlDbType.NVarChar);

            sqlParameter.Value = "380983471677";

            sqlParameter.Direction = ParameterDirection.Input;

            var sql = "SELECT * FROM Student" +
                      "WHERE Student.PhoneNumber LIKE @PhoneNumber";

            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                cn.Open();

                SqlCommand cd = new SqlCommand(sql, cn);

                using(SqlDataReader dr = cd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Console.WriteLine(dr[1]);
                    }
                }
            }
        }

        public void InsertData(StudentModel student)
        {

        }
    }
}

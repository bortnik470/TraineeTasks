using DesignPattern_Adapter.DataModels;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DesignPattern_Adapter.Adapters.Adaptee
{
    internal class DBStudentAccess : IDBStudentAccess
    {
        private readonly string connStr = ConfigurationManager.ConnectionStrings["SqlConnectionString"].ConnectionString;

        public List<Student> GetStudents()
        {
            List<Student> students = new();

            using (SqlConnection sqlConnection = new SqlConnection(connStr))
            {
                sqlConnection.Open();

                string sqlQuery = @"Select * from students";

                SqlCommand cmd = new SqlCommand(sqlQuery, sqlConnection);

                using (SqlDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        int studentId = dataReader.GetInt32("studentId");
                        string firstName = dataReader.GetString("firstName");
                        string lastName = dataReader.GetString("lastName");
                        string phoneNumber = dataReader.GetString("phoneNumber");
                        string groupName = dataReader.GetString("groupName");

                        students.Add(new Student(studentId, firstName, lastName, phoneNumber, groupName));
                    }
                }
            }

            return students;
        }

        public Student GetStudent(int id)
        {
            Student student = null;

            using (SqlConnection sqlConnection = new SqlConnection(connStr))
            {
                sqlConnection.Open();

                string sqlQuery = @"Select * from Students where studentId = @studentId";

                SqlCommand cmd = sqlConnection.CreateCommand();

                cmd.CommandText = sqlQuery;

                SqlParameter sqlParam = new SqlParameter("@studentId", SqlDbType.Int);

                sqlParam.Value = id;

                cmd.Parameters.Add(sqlParam);

                using (SqlDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        int studentId = dataReader.GetInt32("studentId");
                        string firstName = dataReader.GetString("firstName");
                        string lastName = dataReader.GetString("lastName");
                        string phoneNumber = dataReader.GetString("phoneNumber");
                        string groupName = dataReader.GetString("groupName");

                        student = new Student(studentId, firstName, lastName, phoneNumber, groupName);
                    }
                }
            }

            return student;
        }
    }
}
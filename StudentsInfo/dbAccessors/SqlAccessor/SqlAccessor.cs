using StudentsInfo.Interfaces;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using System.Text;

namespace StudentsInfo.dbAccessors.SqlAccessor
{
    public class SqlAccessor : IRepository
    {
        public string connectionString { get; private set; }

        public SqlAccessor(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void DeleteData(string tableName, object key, string prKeyName)
        {
            var sqlCom = $"Delete From {tableName} Where {prKeyName} = {key}";

            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(sqlCom, cn);

                cn.Open();

                cmd.ExecuteNonQuery();
            }

            Console.WriteLine($"Row with key: {key} deleted");
        }

        public List<object> GetDataFromTable(string tableName)
        {
            var sqlCom = $"Select * From {tableName}";

            List<object> result = new List<object>();

            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(sqlCom, cn);

                cn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    object[] objects = new object[reader.FieldCount];
                    while (reader.Read())
                    {
                        reader.GetValues(objects);
                        result.Add(objects.Clone());
                    }
                }
            }

            return result;
        }

        public void InsertData(string tableName, params object[] data)
        {
            StringBuilder sqlCom = new StringBuilder();
            sqlCom.Append($"Insert into {tableName} values (");
            for(int i =  0; i < data.Length - 1; i++)
            {
                sqlCom.Append($"{data[i]}, ");
            }
            sqlCom.Append($"{data[data.Length - 1]});");

            Console.WriteLine(sqlCom.ToString());

            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(sqlCom.ToString(), cn);

                cn.Open();

                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateData(string tableName, object key, string prKeyName, string columnName, object value)
        {
            var sqlCom = $"Update {tableName} set {columnName} = {value} " +
                         $"Where {prKeyName} = {key};";

            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(sqlCom, cn);

                cn.Open();

                cmd.ExecuteNonQuery();
            }
        }
    }
}

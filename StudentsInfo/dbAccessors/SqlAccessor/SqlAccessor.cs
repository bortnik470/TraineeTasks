using StudentsInfo.ExtraModules.DateTypes;
using StudentsInfo.Interfaces;
using System.Data.SqlClient;
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
            for (int i = 0; i < data.Length - 1; i++)
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

        public List<object> GetSomeDateFromTable(string tableName, List<KeyValue> valuesForFinder)
        {
            StringBuilder sqlCom = new StringBuilder();
            sqlCom.Append($"Select * From {tableName} Where ");
            if (valuesForFinder.Count > 1)
            {
                for (int i = 0; i < valuesForFinder.Count - 1; i++)
                {
                    sqlCom.Append($"{valuesForFinder[i].keyName} = {valuesForFinder[i].keyValue} And ");
                }
            }

            sqlCom.Append($"{valuesForFinder[valuesForFinder.Count - 1].keyName}" +
                          $" = {valuesForFinder[valuesForFinder.Count - 1].keyValue}");

            List<object> result = new List<object>();

            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(sqlCom.ToString(), cn);

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

        public void DeleteData(string tableName, List<KeyValue> valuesForFinder)
        {
            StringBuilder sqlCom = new StringBuilder();
            sqlCom.Append($"Delete From {tableName} Where ");
            if (valuesForFinder.Count > 1)
            {
                for (int i = 0; i < valuesForFinder.Count - 1; i++)
                {
                    sqlCom.Append($"{valuesForFinder[i].keyName} = {valuesForFinder[i].keyValue} And ");
                }
            }

            sqlCom.Append($"{valuesForFinder[valuesForFinder.Count - 1].keyName}" +
                          $" = {valuesForFinder[valuesForFinder.Count - 1].keyValue}");

            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(sqlCom.ToString(), cn);

                cn.Open();

                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateData(string tableName, List<KeyValue> valuesForFinder, List<KeyValue> updateData)
        {
            StringBuilder sqlCom = new StringBuilder();
            sqlCom.Append($"Update {tableName} Set ");
            if (updateData.Count > 1)
            {
                for (int i = 0; i < valuesForFinder.Count - 1; i++)
                {
                    sqlCom.Append($"{valuesForFinder[i].keyName} = {valuesForFinder[i].keyValue} And ");
                }
            }

            sqlCom.Append($"{valuesForFinder[valuesForFinder.Count - 1].keyName}" +
                          $" = {valuesForFinder[valuesForFinder.Count - 1].keyValue} where ");

            if (valuesForFinder.Count > 1)
            {
                for (int i = 0; i < valuesForFinder.Count - 1; i++)
                {
                    sqlCom.Append($"{valuesForFinder[i].keyName} = {valuesForFinder[i].keyValue} And ");
                }
            }

            sqlCom.Append($"{valuesForFinder[valuesForFinder.Count - 1].keyName}" +
                          $" = {valuesForFinder[valuesForFinder.Count - 1].keyValue}");

            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(sqlCom.ToString(), cn);

                cn.Open();

                cmd.ExecuteNonQuery();
            }
        }
    }
}

using StudentsInfo.Data.DataModels;
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

        public List<object[]> GetDataFromTable(string tableName)
        {
            var sqlCom = $"Select * From {tableName}";

            List<object[]> result = new List<object[]>();

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
                        result.Add((object[])objects.Clone());
                    }
                }
            }

            return result;
        }

        public void InsertData(string tableName, List<KeyValueType> data)
        {
            StringBuilder sqlCom = new StringBuilder();
            sqlCom.Append($"Insert into {tableName} values (");
            for (int i = 0; i < data.Count - 1; i++)
            {
                if (data[i].Type.Equals("number"))
                    sqlCom.Append($"{data[i].Value}, ");
                else sqlCom.Append($"'{data[i].Value}', ");
            }
            if (data[data.Count - 1].Type.Equals("number"))
                sqlCom.Append($"{data[data.Count - 1].Value})");
            else sqlCom.Append($"'{data[data.Count - 1].Value}');");

            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(sqlCom.ToString(), cn);

                cn.Open();

                cmd.ExecuteNonQuery();
            }
        }

        public List<object[]> GetSomeDateFromTable(string tableName, List<KeyValueType> valuesForFinder)
        {
            StringBuilder sqlCom = new StringBuilder();
            sqlCom.Append($"Select * From {tableName} Where ");

            ImplementValue(sqlCom, valuesForFinder, "and");

            sqlCom.Append(';');

            List<object[]> result = new List<object[]>();

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
                        result.Add((object[])objects.Clone());
                    }
                }
            }

            return result;
        }

        public void DeleteData(string tableName, List<KeyValueType> valuesForFinder)
        {
            StringBuilder sqlCom = new StringBuilder();
            sqlCom.Append($"Delete From {tableName} Where ");
            ImplementValue(sqlCom, valuesForFinder, "and");

            sqlCom.Append(';');

            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(sqlCom.ToString(), cn);

                cn.Open();

                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateData(string tableName, List<KeyValueType> valuesForFinder, List<KeyValueType> updateData)
        {
            StringBuilder sqlCom = new StringBuilder();
            sqlCom.Append($"Update {tableName} Set ");
            ImplementValue(sqlCom, valuesForFinder, ",");

            sqlCom.Append(" where ");
            ImplementValue(sqlCom, valuesForFinder, "and");

            sqlCom.Append(';');

            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(sqlCom.ToString(), cn);

                cn.Open();

                cmd.ExecuteNonQuery();
            }
        }

        private void ImplementValue(StringBuilder sb, List<KeyValueType> data, string symbol)
        {
            for (int i = 0; i < data.Count - 1; i++)
            {
                if (data[i].Type.Equals("number"))
                    sb.Append($"{data[i].Key} = {data[i].Value} {symbol} ");
                else sb.Append($"{data[i].Key} = '{data[i].Value}' {symbol} ");
            }

            if (data[data.Count - 1].Type.Equals("number"))
                sb.Append($"{data[data.Count - 1].Key} = {data[data.Count - 1].Value}");
            else sb.Append($"{data[data.Count - 1].Key} = '{data[data.Count - 1].Value}'");

            if (data.Count > 1)
            {
                sb.Append(')');
            }
        }
    }
}

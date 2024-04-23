using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentsInfo
{
    public partial class DisADO
    {
        public string connectionString;

        public DisADO(string dataSetName, string connectionString)
        {
            this.dataSet = new DataSet(dataSetName);
            this.connectionString = connectionString;
        }

        public void CheckConString()
        {
            if (string.IsNullOrEmpty(connectionString)) throw new ArgumentNullException("Connection string is null");
        }

        public void UpdTableFromSql(string tableName)
        {
            CheckConString();

            using (SqlConnection sc = new SqlConnection(connectionString))
            {
                SqlDataAdapter sda = new SqlDataAdapter();
                SqlCommand cmd = sc.CreateCommand();
                cmd.CommandText = $"Select * from {tableName}";
                sda.SelectCommand = cmd;

                sc.Open();

                sda.Fill(dataSet);
            }
        }

        public void UpdTablesFromSql(List<string> tableNames)
        {
            CheckConString();

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                SqlDataAdapter dataAdapter = new SqlDataAdapter();
                SqlCommand cmd = sqlConnection.CreateCommand();

                sqlConnection.Open();

                foreach (string tableName in tableNames)
                {
                    cmd.CommandText = $"Select * from {tableName}";
                    dataAdapter.SelectCommand = cmd;

                    dataAdapter.Fill(dataSet);
                }
            }
        }

        public void LoadSqlDbToDataSet()
        {
            CheckConString() ;

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                SqlDataAdapter dataAdapter = new SqlDataAdapter();
                SqlCommand cmd = sqlConnection.CreateCommand();
                cmd.CommandText = $"Select TABLE_NAME from INFORMATION_SCHEMA.TABLES Where TABLE_TYPE='BASE TABLE'";
                dataAdapter.SelectCommand = cmd;

                sqlConnection.Open();


            }
        }
    }
}

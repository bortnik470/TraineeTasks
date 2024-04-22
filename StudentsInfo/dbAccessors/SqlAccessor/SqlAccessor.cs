using StudentsInfo.Data.DataModels;
using StudentsInfo.DataModels;
using StudentsInfo.Enums;
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

        public List<Student> GetStudentDataFromTable(string tableName, string discTableName)
        {
            var sqlCom = $"Select * From {tableName}";

            List<Student> result = new List<Student>();

            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(sqlCom, cn);

                cn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var student = new Student();
                        student.firstName = reader.GetString(1);
                        student.lastName = reader.GetString(2);
                        student.phoneNumber = reader.GetString(3);
                        student.groupName = reader.GetString(4);

                        student.disciplines = GetDisciplinesBySomeDataFromTable(discTableName, new List<KeyValueType>
                        {
                             new KeyValueType("studentId", reader.GetInt32(0), "number"),
                        });

                        result.Add(student);
                    }
                }
            }

            return result;
        }

        public List<Student> GetSomeStudentDataFromTable(string tableName, string discTableName, List<KeyValueType> valuesForFinder)
        {
            StringBuilder sqlCom = new StringBuilder();
            sqlCom.Append($"Select * From {tableName} Where ");

            ImplementValue(sqlCom, valuesForFinder, "and");

            sqlCom.Append(';');

            List<Student> result = new List<Student>();

            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(sqlCom.ToString(), cn);

                cn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var student = new Student();
                        student.firstName = reader.GetString(1);
                        student.lastName = reader.GetString(2);
                        student.phoneNumber = reader.GetString(3);
                        student.groupName = reader.GetString(4);

                        student.disciplines = GetDisciplinesBySomeDataFromTable(discTableName, new List<KeyValueType>
                        {
                            new KeyValueType("studentId", reader.GetInt32(0), "number"),
                        });

                        result.Add(student);
                    }
                }
            }

            return result;
        }

        public int GetStudentId(string tableName, Student student)
        {
            int studentId = 0;

            StringBuilder sb = new StringBuilder($"Select studentId From {tableName} where ");

            if (student.phoneNumber != null)
            {
                sb.Append($"phoneNumber = '{student.phoneNumber}';");
            }
            else
            {
                sb.Append($"firstName = '{student.firstName}' and ");
                sb.Append($"lastName = '{student.lastName}' and ");
                sb.Append($"groupName = '{student.groupName}';");
            }

            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(sb.ToString(), cn);

                cn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        studentId = reader.GetInt32(0);
                    }
                }
            }

            return studentId;
        }

        public List<DisciplineModel> GetDisciplinesDataFromTable(string tableName)
        {
            var sqlCom = $"Select * From {tableName}";

            List<DisciplineModel> result = new List<DisciplineModel>();

            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(sqlCom, cn);

                cn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var discipline = new DisciplineModel();
                        discipline.disciplineName = (DisciplineName)Enum.Parse(typeof(DisciplineName), reader.GetString(1));
                        discipline.score = (Score)Enum.Parse(typeof(Score), reader.GetString(2));
                        discipline.startDate = reader.GetDateTime(3);
                        discipline.endDate = reader.GetDateTime(4);

                        result.Add(discipline);
                    }
                }
            }

            return result;
        }

        public List<DisciplineModel> GetDisciplinesBySomeDataFromTable(string tableName, List<KeyValueType> valuesForFinder)
        {
            StringBuilder sqlCom = new StringBuilder();
            sqlCom.Append($"Select * From {tableName} Where ");

            ImplementValue(sqlCom, valuesForFinder, "and");

            sqlCom.Append(';');

            List<DisciplineModel> result = new List<DisciplineModel>();

            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(sqlCom.ToString(), cn);

                cn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var discipline = new DisciplineModel();
                        discipline.disciplineName = (DisciplineName)Enum.Parse(typeof(DisciplineName), reader.GetString(1));
                        discipline.score = (Score)Enum.Parse(typeof(Score), reader.GetString(2));
                        discipline.startDate = reader.GetDateTime(3);
                        discipline.endDate = reader.GetDateTime(4);

                        result.Add(discipline);
                    }
                }
            }

            return result;
        }

        public int GetDisciplineId(string tableName, int studentId, DisciplineModel discipline)
        {
            int disciplineId = 0;

            StringBuilder sb = new StringBuilder($"Select disciplineId From {tableName} where ");

            ImplementValue(sb, new List<KeyValueType>
            {
                new KeyValueType("studentId", studentId, "number"),
                new KeyValueType("disciplineName", discipline.disciplineName),
                new KeyValueType("score", discipline.score),
                new KeyValueType("startDate", discipline.startDate),
                new KeyValueType("endDate", discipline.endDate),
            }, "and");

            sb.Append(';');

            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(sb.ToString(), cn);

                cn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        disciplineId = reader.GetInt32(0);
                    }
                }
            }

            return disciplineId;
        }

        public void InsertData(string tableName, List<KeyValueType> data)
        {
            StringBuilder sqlCom = new StringBuilder();
            sqlCom.Append($"Insert into {tableName} values (");
            for (int i = 0; i < data.Count - 1; i++)
            {
                if (!string.IsNullOrEmpty(data[i].Type))
                    sqlCom.Append($"{data[i].Value}, ");
                else sqlCom.Append($"'{data[i].Value}', ");
            }

            if (!string.IsNullOrEmpty(data[data.Count - 1].Type))
                sqlCom.Append($"{data[data.Count - 1].Value})");
            else sqlCom.Append($"'{data[data.Count - 1].Value}');");

            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(sqlCom.ToString(), cn);

                cn.Open();

                cmd.ExecuteNonQuery();
            }
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
                if (!string.IsNullOrEmpty(data[i].Type))
                    sb.Append($"{data[i].Key} = {data[i].Value} {symbol} ");
                else sb.Append($"{data[i].Key} = '{data[i].Value}' {symbol} ");
            }

            if (!string.IsNullOrEmpty(data[data.Count - 1].Type))
                sb.Append($"{data[data.Count - 1].Key} = {data[data.Count - 1].Value}");
            else sb.Append($"{data[data.Count - 1].Key} = '{data[data.Count - 1].Value}'");

            if (data.Count > 1)
            {
                sb.Append(')');
            }
        }
    }
}
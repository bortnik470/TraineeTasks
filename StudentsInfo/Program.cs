using StudentsInfo;
using StudentsInfo.Data.FinishedClasses.StudentClass;
using StudentsInfo.dbAccessors.SqlAccessor;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

string connectionString = ConfigurationManager.ConnectionStrings["SqlConnectionString"].ConnectionString;


DisADO disADO = new DisADO("University", connectionString);

disADO.LoadSqlDbToDataSet();

StudentDbAccessor s = new StudentDbAccessor(new SqlAccessor(connectionString),
    "Students", "Disciplines");

Console.WriteLine(s.GetStudentByID(3));

Console.WriteLine(1);
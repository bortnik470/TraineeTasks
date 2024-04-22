using StudentsInfo;
using StudentsInfo.Data.FinishedClasses.Student;
using StudentsInfo.dbAccessors.SqlAccessor;
using System.Configuration;

string connectionString = ConfigurationManager.ConnectionStrings["SqlConnectionString"].ConnectionString;

StudentDbAccessor studentDbAccessor = new StudentDbAccessor(new SqlAccessor(connectionString), "Students", "Disciplines");

studentDbAccessor.DeleteStudentById(1);

Console.WriteLine(1);
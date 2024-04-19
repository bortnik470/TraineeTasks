using StudentsInfo;
using StudentsInfo.Data.FinishedClasses.Student;
using StudentsInfo.dbAccessors.SqlAccessor;
using System.Configuration;

string connectionString = ConfigurationManager.ConnectionStrings["SqlConnectionString"].ConnectionString;

StudentDbAccessor studentDbAccessor = new StudentDbAccessor(new SqlAccessor(connectionString), "Students", "Disciplines");

studentDbAccessor.DeleteStudentById(1);

var i = new char[] { 'A', 'B' };

Console.WriteLine(i);

//Console.WriteLine("XML Serialization\n");
//CustomXmlSerializer.Serialize(default, StudentCreator.CreateDefaultStudents().ToArray());

//var xmlStudents = CustomXmlSerializer.DeserializeStudents("data.xml");

//foreach (var student in xmlStudents)
//    Console.WriteLine(student);


//Console.WriteLine("JSON Serialization\n");
//CustomJSONSerializer.SerializeStudents(default, StudentCreator.CreateRandomStudents(6).ToArray());

//var jsonStudents = CustomJSONSerializer.DeserializeStudents(default);

//foreach (var student in jsonStudents)
//    Console.WriteLine(student);
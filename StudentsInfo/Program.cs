using StudentsInfo.dbAccessors.SqlAccessor;
using System.Configuration;

SqlAccessor sqlAccessor = new SqlAccessor(ConfigurationManager.ConnectionStrings["SqlConnectionString"].ConnectionString);

sqlAccessor.UpdateData("Students", 1, "id", "groupName", "'24B'");

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
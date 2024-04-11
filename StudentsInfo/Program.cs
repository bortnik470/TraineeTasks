using StudentsInfo;
using StudentsInfo.Serializers;

Console.WriteLine("XML Serialization\n");
XmlSerializator.Serialize(default, StudentCreator.CreateDefaultStudents().ToArray());

var xmlStudents = XmlSerializator.DeserializeStudents("data.xml");

foreach (var student in xmlStudents)
    Console.WriteLine(student);


Console.WriteLine("JSON Serialization\n");
JSONSerializator.SerializeStudents(default, StudentCreator.CreateRandomStudents(6).ToArray());

var jsonStudents = JSONSerializator.DeserializeStudents(default);

foreach (var student in jsonStudents)
    Console.WriteLine(student);
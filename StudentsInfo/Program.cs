using StudentsInfo;

DisADO adaptiveADO = new DisADO("test");

adaptiveADO.CreateDisciplineTable();
adaptiveADO.CreateStudentTable();

adaptiveADO.AddNewStudentRow(StudentCreator.CreateDefaultStudents()[1]);
adaptiveADO.AddNewStudentRow(StudentCreator.CreateDefaultStudents()[2]);

adaptiveADO.ShowAllStudent();

adaptiveADO.DeleteStudent(StudentCreator.CreateDefaultStudents()[1]);

adaptiveADO.ShowAllStudent();

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
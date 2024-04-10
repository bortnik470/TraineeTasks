using StudentsInfo.DataModels.Disciplines;
using StudentsInfo.DataModels.Student;
using StudentsInfo.DataModels.Utility;
using StudentsInfo.Enums;
using StudentsInfo.Serializers;

Student student = new Student("Dima", "Dasd", 98, "2B", new List<Discipline> {
        new Discipline(StudentsInfo.Enums.DisciplineName.None, Score.None, new DateModel(1992, 2, 24), new DateModel(1993, 2, 23))
});
Student student3 = new Student("Vlad", "Asd", 324, "34", new List<Discipline> {
        new Discipline(StudentsInfo.Enums.DisciplineName.None, Score.None, new DateModel(1992, 2, 24), new DateModel(1993, 2, 23))
});

Console.WriteLine(student.disciplines[0].startDate);
Console.WriteLine(student.disciplines[0].endDate);

XmlSerializator.Serialize(student, student3);

var student1 = XmlSerializator.DeserializeStudents("data.xml");

Console.WriteLine(student);
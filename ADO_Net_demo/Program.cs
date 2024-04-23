using ADO_Net_demo;
using ADO_Net_demo.DAL;

DataAccessConnected dataAccessConnected = new DataAccessConnected();

var st = new Student(27, "David", "Young", "32142152", "2A", new List<Course> {
                    new Course(25, "Math", "E",
                        new DateOnly(2002, 2, 11), new DateOnly(2002, 6, 20)),
                    new Course(26, "English", "C",
                        new DateOnly(2001, 9, 15), new DateOnly(2002, 1, 10))
                    });

dataAccessConnected.Update(st);

Console.WriteLine(st);
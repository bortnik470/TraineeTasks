using ADO_Net_demo;
using ADO_Net_demo.DAL;
using System.Configuration;

DataAccessConnected dataAccessConnected = new DataAccessConnected();

var st = new Student(27, "David", "Young", "32142152", "2A", new List<Course> {
                    new Course(25, "Math", "E",
                        new DateOnly(2002, 2, 11), new DateOnly(2002, 6, 20)),
                    new Course(26, "English", "C",
                        new DateOnly(2001, 9, 15), new DateOnly(2002, 1, 10))
                    });

DataAccessDisconnected dataAccessDisconnected = 
    new DataAccessDisconnected(ConfigurationManager.ConnectionStrings["SqlConnectionString"].ConnectionString);

dataAccessDisconnected.GetById(3);

Console.WriteLine(dataAccessDisconnected.GetById(3));
using ADO_Net_demo;
using ADO_Net_demo.DAL;
using System.Configuration;

DataAccessConnected dataAccessConnected = new DataAccessConnected();

var st = new Student(40, "David", "Young", "32144125", "2A", new List<Course> {
                    new Course(33, 40, "Math", "E",
                        new DateOnly(2002, 2, 11), new DateOnly(2002, 6, 20)),
                    new Course(34, 40, "English", "C",
                        new DateOnly(2001, 9, 15), new DateOnly(2002, 1, 10))
                    });

DataAccessDisconnected dataAccessDisconnected = 
    new DataAccessDisconnected(ConfigurationManager.ConnectionStrings["SqlConnectionString"].ConnectionString);

//dataAccessDisconnected.Delete(39);

//dataAccessDisconnected.Insert(st);

st.FirstName = "First";
st.Courses.Add(new Course("Art", "Fx",
                        new DateOnly(2002, 2, 11), new DateOnly(2002, 5, 21)));

st.Courses[0].Name = "Dash";

dataAccessDisconnected.Update(st);

var s = dataAccessDisconnected.GetList();

Console.WriteLine(dataAccessDisconnected.GetById(3));
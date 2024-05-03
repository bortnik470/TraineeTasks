using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace ADO_Net_demo
{
    internal class LinqToXml
    {
        private XDocument XmlDoc;

        private string path;

        public LinqToXml(string path = "XMLTest.xml") 
        {
            this.path = path;
            if (File.Exists(path))
            {
                XmlDoc = new XDocument(XDocument.Load(path));
            }
            else
            {
                XmlDoc = new XDocument(new XDeclaration("1.0", "utf-8", "yes"),
                    new XElement("University",
                        new XElement("Students"),
                        new XElement("Courses")
                    ));
            }
        }

        public void AddStudent(Student student)
        {
            var studentElem = new XElement("Student", new XAttribute("StudentId", student.StudentId),
                new XElement("FirstName", student.FirstName),
                new XElement("LastName", student.LastName),
                new XElement("PhoneNumber", student.PhoneNumber),
                new XElement("GroupName"), student.GroupName);

            foreach (var course in student.Courses)
            {
                var courseElem = new XElement("Course", new XAttribute("CourseId", course.CourseId),
                    new XAttribute("StudentId", student.StudentId),
                    new XElement("CourseName", course.CourseName),
                    new XElement("Score", course.Score),
                    new XElement("StartDate", course.StartDate),
                    new XElement("EndDate", course.EndDate)
                );

                XmlDoc.Descendants("Courses").First().Add(courseElem);
            }

            XmlDoc.Descendants("Students").First().Add(studentElem);

            Console.WriteLine(XmlDoc);

            XmlDoc.Save(path);
        }

        public List<Student> GetStudents()
        {
            List<Student> students = new();

            var xStudents = XmlDoc.Descendants("Student").ToList();
            var xCourses = XmlDoc.Descendants("Course").ToList();

            foreach (var xStudent in xStudents)
            {
                var student = new Student(int.Parse(xStudent.Attribute("StudentId").Value),
                                            xStudent.Element("FirstName").Value,
                                            xStudent.Element("LastName").Value,
                                            xStudent.Element("PhoneNumber").Value,
                                            xStudent.Element("GroupName").Value);

                var courses = xCourses.Where(x => int.Parse(x.Attribute("StudentId").Value) == student.StudentId).
                    Select(x => new Course(int.Parse(x.Attribute("CourseId").Value),
                                           x.Element("CourseName").Value,
                                           x.Element("Score").Value,
                                           DateOnly.Parse(x.Element("StartDate").Value),
                                           DateOnly.Parse(x.Element("EndDate").Value),
                                           int.Parse(x.Attribute("StudentId").Value)
                    )).ToList();

                student.Courses = courses;
                students.Add(student);
            }

            return students;
        }
    }
}

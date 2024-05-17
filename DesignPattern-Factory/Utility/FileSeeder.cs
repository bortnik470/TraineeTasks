using ADO_Net_demo.DAL;
using DesignPattern_Factory.DataModels.Users;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace DesignPattern_Factory.Utility
{
    public partial class FileSeeder
    {
        private int i = 1;

        public void XmlUserSeed(string pathToFile = null)
        {
            if (string.IsNullOrWhiteSpace(pathToFile))
                pathToFile = "UsersInfo.xml";

            string regexPattern = @"\.xml$";
            if (!Regex.IsMatch(pathToFile, regexPattern))
                pathToFile += ".xml";

            DataAccessConnected dac = new DataAccessConnected();

            var students = dac.GetList();

            var admins = new List<Admin>()
            {
                new Admin("Din", "Peterson", "1")
            };

            var teachers = new List<Teacher>()
            {
                new Teacher("Alfred", "Jackson", "79016324",
                    new List<string>() { "2A", "3A" }),
                new Teacher("Lisa", "Jackson", "89075321",
                    new List<string>() { "3B", "6C", "2B" })
            };

            XDocument xDocument = new XDocument(new XDeclaration("1.0", "utf-8", "yes"),
                new XElement("University",
                    new XElement("Users"),
                    new XElement("Courses")
                ));

            AddStudents(xDocument, students);
            AddAdmins(xDocument, admins);
            AddTeachers(xDocument, teachers);

            xDocument.Save(pathToFile);
        }

        private void AddTeachers(XDocument xDocument, List<Teacher> teachers)
        {
            foreach (var teacher in teachers)
            {
                var teacherElem = new XElement("User",
                        new XAttribute("ID", i++),
                        new XAttribute("Role", "Teacher"),
                        new XElement("FirstName", teacher.FirstName),
                        new XElement("LastName", teacher.LastName),
                        new XElement("PhoneNumber", teacher.PhoneNumber),
                        new XElement("Groupes")
                    );

                foreach (var group in teacher.Groupes)
                {
                    var groupElem = new XElement("Group", group);
                    teacherElem.Descendants("Groupes").First().Add(groupElem);
                }

                xDocument.Descendants("Users").First().Add(teacherElem);
            }
        }

        private void AddAdmins(XDocument xDocument, List<Admin> admins)
        {
            foreach (var admin in admins)
            {
                var adminElem = new XElement("User",
                        new XAttribute("ID", i++),
                        new XAttribute("Role", "Admin"),
                        new XElement("FirstName", admin.FirstName),
                        new XElement("LastName", admin.LastName),
                        new XElement("PhoneNumber", admin.PhoneNumber)
                    );

                xDocument.Descendants("Users").First().Add(adminElem);
            }
        }

        private void AddStudents(XDocument xDocument, List<ADO_Net_demo.Student> students)
        {
            foreach (var student in students)
            {
                var studentElem = new XElement("User",
                        new XAttribute("ID", i),
                        new XAttribute("Role", "Student"),
                        new XElement("FirstName", student.FirstName),
                        new XElement("LastName", student.LastName),
                        new XElement("PhoneNumber", student.PhoneNumber),
                        new XElement("GroupName"), student.GroupName);

                foreach (var course in student.Courses)
                {
                    var courseElem = new XElement("Course",
                        new XAttribute("StudentId", i),
                        new XElement("CourseName", course.CourseName),
                        new XElement("Score", course.Score),
                        new XElement("StartDate", course.StartDate),
                        new XElement("EndDate", course.EndDate)
                    );

                    xDocument.Descendants("Courses").First().Add(courseElem);
                }

                i++;
                xDocument.Descendants("Users").First().Add(studentElem);
            }
        }
    }
}

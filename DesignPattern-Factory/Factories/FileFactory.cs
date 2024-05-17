using DesignPattern_Factory.DataModels.Users;
using DesignPattern_Factory.Enums;
using StudentsInfo.DataModels;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace DesignPattern_Factory.Factories
{
    internal class FileFactory : BaseFactoryMethod
    {
        public readonly string PathToFile;

        public FileFactory(string pathToFile = "UsersInfo.xml")
        {
            string regexPattern = @"^([a-zA-Z0-9\._\+]+)\.xml$";
            if (Regex.IsMatch(pathToFile, regexPattern))
                if (File.Exists(pathToFile))
                {
                    PathToFile = pathToFile;
                    return;
                }

            throw new FileNotFoundException($"The xml file with next path {pathToFile} not found");
        }

        protected override BaseUser CreateUser(int userId)
        {
            XDocument xDocument = XDocument.Load(PathToFile);

            var xUser = xDocument.Descendants("User").Where(x =>
                    int.Parse(x.Attribute("ID").Value).Equals(userId)).
                FirstOrDefault();

            Roles role = (Roles)Enum.Parse(typeof(Roles), xUser.Attribute("Role").Value);

            switch (role)
            {
                case Roles.Admin:
                    return CreateAdmin(xUser);
                case Roles.Student:
                    var xCourses = xDocument.Descendants("Course");
                    return CreateStudent(xUser, xCourses, userId);
                case Roles.Teacher:
                    return CreateTeacher(xUser);
                default:
                    return null;
            }
        }

        private Student CreateStudent(XElement xStudent, IEnumerable<XElement> xCourses, int ID)
        {
            var baseInfo = GetBaseInfo(xStudent);

            var student = new Student(baseInfo[0], baseInfo[1], baseInfo[2], null,
                                            xStudent.Element("GroupName").Value);


            var courses = xCourses.Where(x => int.Parse(x.Attribute("StudentId").Value) == ID).
                Select(x => new Course(x.Element("CourseName").Value,
                                       x.Element("Score").Value,
                                       DateTime.Parse(x.Element("StartDate").Value),
                                       DateTime.Parse(x.Element("EndDate").Value)
                )).ToList();

            student.Courses = courses;

            return student;
        }

        private Admin CreateAdmin(XElement xAdmin)
        {
            var baseInfo = GetBaseInfo(xAdmin);

            var admin = new Admin(baseInfo[0], baseInfo[1], baseInfo[2]);

            return admin;
        }

        public Teacher CreateTeacher(XElement xTeacher)
        {
            var baseInfo = GetBaseInfo(xTeacher);

            var groupes = xTeacher.Element("Groupes").Elements().
                Select(x => x.Value).ToList();

            var teacher = new Teacher(baseInfo[0], baseInfo[1], baseInfo[2], groupes);

            return teacher;
        }

        private List<string> GetBaseInfo(XElement xElement)
        {
            string firstName = xElement.Element("FirstName").Value;
            string lastName = xElement.Element("LastName").Value;
            string phoneNumber = xElement.Element("PhoneNumber").Value;

            return new List<string>() { firstName, lastName, phoneNumber };
        }
    }
}

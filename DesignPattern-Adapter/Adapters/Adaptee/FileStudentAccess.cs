using DesignPattern_Adapter.DataModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DesignPattern_Adapter.Adapters.Adaptee
{
    internal class FileStudentAccess
    {
        public void FillFile(string pathToFile)
        {
            XDocument xDocument;

            if (File.Exists(pathToFile))
            {
                xDocument = XDocument.Load(pathToFile);
            }
            else
            {
                xDocument = new XDocument(new XDeclaration("1.0", "utf-8", "yes"),
                    new XElement("University",
                        new XElement("Students")
                        ));
            }

            DBStudentAccess dBStudentAccess = new DBStudentAccess();

            List<Student> students = dBStudentAccess.GetStudents();

            if (students.Count > 0)
            {
                foreach (Student student in students)
                {
                    var xStudent = new XElement("Student", new XAttribute("StudentId", student.StudentId),
                                 new XElement("FirstName", student.FirstName),
                                 new XElement("LastName", student.LastName),
                                 new XElement("PhoneNumber", student.PhoneNumber),
                                 new XElement("GroupName", student.GroupName)
                        );

                    xDocument.Descendants("Students").First().Add(xStudent);
                }

                xDocument.Save(pathToFile);
            }
            else
            {
                Console.WriteLine("Db is clear");
                return;
            }
        }

        public Student GetStudent(int id, string pathToFile)
        {
            if (!File.Exists(pathToFile))
                throw new FileNotFoundException($"File with next address |{pathToFile}| not found");

            XDocument xDocument = XDocument.Load(pathToFile);

            var studentElement = xDocument.Descendants("Student").
                Where(x=> int.Parse(x.Attribute("StudentId").Value).Equals(id)).
                FirstOrDefault();

            int studentId = int.Parse(studentElement.Attribute("StudentId").Value);
            string firstName = studentElement.Element("FirstName").Value;
            string lastName = studentElement.Element("LastName").Value;
            string phoneNumber = studentElement.Element("PhoneNumber").Value;
            string groupName = studentElement.Element("GroupName").Value;

            Student student = new Student(studentId, firstName, lastName, phoneNumber, groupName);

            return student;
        }
    }
}

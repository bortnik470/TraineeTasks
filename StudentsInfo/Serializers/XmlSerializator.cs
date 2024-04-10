using StudentsInfo.DataModels.Student;
using StudentsInfo.Interfaces;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace StudentsInfo.Serializers
{
    public static class XmlSerializator
    {
        public static void Serialize(params Student[] students)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Student[]));
            using (FileStream fs = File.Open("data.xml", FileMode.OpenOrCreate, FileAccess.Write))
            {
                xmlSerializer.Serialize(fs, students);
            }
        }

        public static List<Student> DeserializeStudents(string pathToFile)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Student>));
            List<Student> result;
            using (FileStream fs = File.Open(pathToFile, FileMode.Open, FileAccess.Read))
            {
                result = (List<Student>)xmlSerializer.Deserialize(fs);
            }

            return result;
        }
    }
}

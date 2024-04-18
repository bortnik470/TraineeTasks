using System.Configuration;
using System.Xml.Serialization;

namespace StudentsInfo.Serializers
{
    public static class CustomXmlSerializer
    {
        public static void Serialize(string pathToFile, params Student[] students)
        {
            if (string.IsNullOrEmpty(pathToFile)) pathToFile = $"{ConfigurationManager.AppSettings["pathToFile"]}.xml";

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Student[]));
            using (FileStream fs = File.Open(pathToFile, FileMode.Create, FileAccess.Write))
            {
                xmlSerializer.Serialize(fs, students);
            }
        }

        public static List<Student> DeserializeStudents(string pathToFile)
        {
            if (string.IsNullOrEmpty(pathToFile)) pathToFile = $"{ConfigurationManager.AppSettings["pathToFile"]}.xml";

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

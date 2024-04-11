using System.Configuration;
using System.Text.Json;

namespace StudentsInfo.Serializers
{
    public static class JSONSerializator
    {
        static JsonSerializerOptions options = new JsonSerializerOptions
        {
            WriteIndented = true,
        };

        public static void SerializeStudents(string pathToFile, params Student[] students)
        {
            if (string.IsNullOrEmpty(pathToFile)) pathToFile = $"{ConfigurationManager.AppSettings["pathToFile"]}.json";

            File.WriteAllText(pathToFile, JsonSerializer.Serialize(students, options));
        }

        public static List<Student> DeserializeStudents(string pathToFile)
        {
            if (string.IsNullOrEmpty(pathToFile)) pathToFile = $"{ConfigurationManager.AppSettings["pathToFile"]}.json";

            List<Student> result = JsonSerializer.Deserialize<List<Student>>(File.ReadAllText(pathToFile), options);
            return result;
        }
    }
}

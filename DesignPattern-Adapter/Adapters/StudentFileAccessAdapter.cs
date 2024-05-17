using DesignPattern_Adapter.Adapters.Adaptee;
using DesignPattern_Adapter.DataModels;

namespace DesignPattern_Adapter.Adapters
{
    internal class StudentFileAccessAdapter : FileStudentAccess, IDBStudentAccess
    {
        public string PathToFile { get; set; }

        public StudentFileAccessAdapter(string pahtToFile)
        {
            PathToFile = pahtToFile;
        }

        public Student GetStudent(int id)
        {
            Student student = GetStudent(id, PathToFile);

            return student;
        }
    }
}
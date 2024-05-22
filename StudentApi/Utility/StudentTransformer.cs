using ADO_Net_demo;
using StudentApi.Models;

namespace StudentApi.Utility
{
    public static class StudentTransformer
    {
        public static StudentToCreate TransformFromDbStudent(this StudentToCreate studentToCreate, Student student)
        {
            studentToCreate.FirstName = student.FirstName;
            studentToCreate.LastName = student.LastName;
            studentToCreate.PhoneNumber = student.PhoneNumber;
            studentToCreate.GroupName = student.GroupName;

            return studentToCreate;
        }

        public static Student TransformFromStudentToCreate(this Student student, StudentToCreate studentToCreate, int id)
        {
            student.StudentId = id;
            student.FirstName = studentToCreate.FirstName;
            student.LastName = studentToCreate.LastName;
            student.PhoneNumber = studentToCreate.PhoneNumber;
            student.GroupName = studentToCreate.GroupName;

            return student;
        }

        public static Student TransformFromStudentToCreate(this Student student, StudentToCreate studentToCreate)
        {
            student.FirstName = studentToCreate.FirstName;
            student.LastName = studentToCreate.LastName;
            student.PhoneNumber = studentToCreate.PhoneNumber;
            student.GroupName = studentToCreate.GroupName;

            return student;
        }
    }
}

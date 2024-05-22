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

            foreach (var course in student.Courses)
            {
                studentToCreate.CoursesToCreate.Add(new CourseToCreate().TransformFromCourse(course));
            }

            return studentToCreate;
        }

        public static Student TransformFromStudentToCreate(this Student student, StudentToCreate studentToCreate, int id)
        {
            student.StudentId = id;
            student.FirstName = studentToCreate.FirstName;
            student.LastName = studentToCreate.LastName;
            student.PhoneNumber = studentToCreate.PhoneNumber;
            student.GroupName = studentToCreate.GroupName;
            student.Courses = new List<Course>().TransformAllFromCourseToCreate(studentToCreate.CoursesToCreate);

            return student;
        }

        public static Student TransformFromStudentToCreate(this Student student, StudentToCreate studentToCreate)
        {
            student.FirstName = studentToCreate.FirstName;
            student.LastName = studentToCreate.LastName;
            student.PhoneNumber = studentToCreate.PhoneNumber;
            student.GroupName = studentToCreate.GroupName;
            student.Courses = new List<Course>().TransformAllFromCourseToCreate(studentToCreate.CoursesToCreate);

            return student;
        }
    }
}

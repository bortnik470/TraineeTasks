using ADO_Net_demo;
using StudentApi.Models;

namespace StudentApi.Utility
{
    public static class CourseTransformer
    {
        public static Course TransformFromCourseToCreate(this Course course, CourseToCreate courseToCreate)
        {
            course.StartDate = courseToCreate.StartDate;
            course.EndDate = courseToCreate.EndDate;
            course.CourseName = courseToCreate.CourseName;
            course.StudentId = courseToCreate.StudentId;
            course.Score = courseToCreate.Score;

            return course;
        }

        public static CourseToCreate TransformFromCourse(this CourseToCreate courseToCreate, Course course)
        {
            courseToCreate.StartDate = course.StartDate;
            courseToCreate.EndDate = course.EndDate;
            courseToCreate.CourseName = course.CourseName;
            courseToCreate.StudentId = course.StudentId;
            courseToCreate.Score = course.Score;

            return courseToCreate;
        }

        public static List<Course> TransformAllFromCourseToCreate(this List<Course> courses, List<CourseToCreate> courseToCreates)
        {
            return TransformAllFromCourseToCreate(courses, courseToCreates , courseToCreates[0].StudentId);
        }

        public static List<Course> TransformAllFromCourseToCreate(this List<Course> courses, List<CourseToCreate> courseToCreates, int studentId)
        {
            for (int i = 0; i < courseToCreates.Count; i++)
            {
                if (courses.Count >= i + 1)
                {
                    courses[i] = courses[i].TransformFromCourseToCreate(courseToCreates[i]);
                }
                else
                {
                    courses.Add(new Course(courseToCreates[i].CourseName, courseToCreates[i].Score,
                        courseToCreates[i].StartDate, courseToCreates[i].EndDate, studentId));
                }
            }

            return courses;
        }
    }
}

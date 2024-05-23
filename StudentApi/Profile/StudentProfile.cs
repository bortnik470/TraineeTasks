using ADO_Net_demo;
using AutoMapper;
using StudentApi.Models;

namespace StudentApi
{
    public class StudentProfile : Profile
    {
        public StudentProfile()
        {
            CreateMap<Course, CourseToCreate>().
                ForMember(c => c.StudentId,
                          opt => opt.MapFrom(src => src.StudentId)).
                ForMember(c => c.CourseId,
                          opt => opt.MapFrom(src => src.CourseId));
            CreateMap<CourseToCreate, Course>().
                ForMember(c => c.StudentId,
                          opt => opt.MapFrom(src => src.StudentId)).
                ForMember(c => c.CourseId,
                          opt => opt.MapFrom(src => src.CourseId));

            CreateMap<Student,  StudentToCreate>().
                ForMember(c => c.CoursesToCreate,
                          opt => opt.MapFrom(src => src.Courses)).
                ForMember(c => c.StudentId,
                          opt => opt.MapFrom(src => src.StudentId));

            CreateMap<StudentToCreate, Student>().
                ForMember(c => c.Courses,
                          opt => opt.MapFrom(src => src.CoursesToCreate)).
                ForMember(c => c.StudentId, 
                          opt => opt.MapFrom(src => src.StudentId));
        }
    }
}

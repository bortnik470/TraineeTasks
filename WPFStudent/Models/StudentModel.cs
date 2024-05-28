using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFStudent.Models
{
    public class StudentModel
    {
        public int Id { get; init; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? GroupName { get; set; }
        public List<CourseModel>? Courses { get; set; }

        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }
    }
}

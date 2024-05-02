using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkTest
{
    internal class StudentModel
    {
        [Key]
        public int StudentId { get; set; }
        [StringLength(255)]
        public string FirstName { get; set; }
        [StringLength(255)]
        public string LastName { get; set; }
        [StringLength(255)]
        public string PhoneNumber { get; set; }
        [StringLength(5)]
        public string GroupName { get; set; }
        public ICollection<CourseModel> Courses { get; set; }
    }
}

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace StudentApi.Models
{
    public class CourseToCreate
    {
        public int CourseId { get; private set; }
        public int StudentId { get; private set; }
        public string CourseName { get; set; }
        public string Score { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
    }
}
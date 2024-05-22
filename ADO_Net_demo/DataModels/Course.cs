using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ADO_Net_demo
{
    [Table("Courses")]
    public class Course
    {
        [Key, Required, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CourseId { get; set; }
        [Required, ForeignKey("Student")]
        public int StudentId { get; set; }
        [StringLength(255), Required]
        public string CourseName { get; set; }
        [StringLength(5)]
        public string Score { get; set; }
        [DataType(DataType.Date), Required]
        [Column(TypeName = "Date")]
        public DateOnly StartDate { get; set; }
        [DataType(DataType.Date)]
        [Column(TypeName = "Date")]
        public DateOnly EndDate { get; set; }

        public Course(string name, string score, DateOnly startDate, DateOnly endDate)
        {
            CourseName = name;
            this.Score = score;
            this.StartDate = startDate;
            this.EndDate = endDate;
        }

        public Course(int courseId, string courseName, string score, DateOnly startDate, DateOnly endDate) :
            this(courseName, score, startDate, endDate)
        { this.CourseId = courseId; }

        public Course(int courseId, string name, string score, DateOnly startDate, DateOnly endDate, int studentId) :
            this(courseId, name, score, startDate, endDate)
        { this.StudentId = studentId; }

        public Course(string name, string score, DateOnly startDate, DateOnly endDate, int studentId) :
            this(name, score, startDate, endDate)
        { this.StudentId = studentId; }
    }
}
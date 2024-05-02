using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkTest
{
    
    internal class CourseModel
    {
        public CourseModel(int courseId, int studentId, string courseName, string score, DateOnly startDate, DateOnly endDate, StudentModel student)
        {
            this.courseId = courseId;
            this.studentId = studentId;
            this.courseName = courseName;
            this.score = score;
            this.startDate = startDate;
            this.endDate = endDate;
            Student = student;
        }

        [Key, Required, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int courseId { get; set; }
        [Required]
        public int studentId { get; set; }
        [StringLength(255)]
        public string courseName { get; set; }
        [StringLength(255)]
        public string score { get; set; }
        [DataType(DataType.Date)]
        [Column(TypeName = "Date")]
        public DateOnly startDate { get; set; }
        [DataType(DataType.Date)]
        [Column(TypeName = "Date")]
        public DateOnly endDate { get; set; }
        [ForeignKey("studentId")]
        public virtual StudentModel Student { get; set; }
    }
}

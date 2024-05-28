namespace WPFStudent.Models
{
    public class CourseModel
    {
        public int CourseId { get; init; }
        public int StudentId { get; init; }
        public string CourseName { get; set; }
        public string Score { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
    }
}

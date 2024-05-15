namespace DesignPattern_Adapter.DataModels
{
    public class Course
    {
        public int CourseId { get; set; }
        public int StudentId { get; set; }
        public string CourseName { get; set; }
        public string Score { get; set; }
        public DateOnly StartDate { get; set; }
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

        public Course(int id, string name, string score, DateOnly startDate, DateOnly endDate, int studentId) :
            this(id, name, score, startDate, endDate)
        { this.StudentId = studentId; }
    }
}
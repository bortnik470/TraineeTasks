namespace ADO_Net_demo
{
    public class Course
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public string Name { get; set; }
        public string Score { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }

        public Course(string name, string score, DateOnly startDate, DateOnly endDate)
        {
            Name = name;
            Score = score;
            StartDate = startDate;
            EndDate = endDate;
        }

        public Course(int id, string name, string score, DateOnly startDate, DateOnly endDate) :
            this(name, score, startDate, endDate)
        { Id = id; }

        public Course(int id, int studentId, string name, string score, DateOnly startDate, DateOnly endDate) :
            this(id, name, score, startDate, endDate)
        { this.StudentId = studentId; }
    }
}
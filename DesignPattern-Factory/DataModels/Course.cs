namespace StudentsInfo.DataModels
{
    [Serializable]
    public class Course
    {
        public Course()
        {
        }

        public Course(string courseName, string score, DateTime startDate, DateTime endDate)
        {
            this.CourseName = courseName;
            this.Score = score;
            this.StartDate = startDate;
            this.EndDate = endDate;
        }

        public string CourseName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Score { get; set; }
    }
}

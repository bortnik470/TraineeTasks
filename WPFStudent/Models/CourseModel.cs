using System.ComponentModel;
using WPFStudent.Views;

namespace WPFStudent.Models
{
    public class CourseModel : BaseModel, IDbValue
    {
        private string _courseName;
        private DateOnly? _startDate;
        private DateOnly? _endDate;

        public int CourseId { get; set; }
        public int StudentId { get; set; }
        public string CourseName
        {
            get => _courseName;
            set
            {
                if (string.IsNullOrEmpty(value))
                    AddError(nameof(CourseName), "Course name is empty");
                else
                    ClearError(nameof(CourseName), "Course name is empty");
                _courseName = value;

                PropertyInModelChange?.Invoke();
            }
        }
        public string Score { get; set; } = "None";
        public DateOnly? StartDate
        {
            get => _startDate;
            set
            {
                if (value == default)
                    AddError(nameof(StartDate), "Start date is empty or defalut");
                else
                    ClearError(nameof(StartDate), "Start date is empty or defalut");
                _startDate = value;

                PropertyInModelChange?.Invoke();
            }
        }
        public DateOnly? EndDate
        {
            get => _endDate;
            set
            {
                if (value == default)
                    AddError(nameof(EndDate), "End date is empty or defalut");
                else
                    ClearError(nameof(EndDate), "End date is empty or defalut");
                _endDate = value;

                PropertyInModelChange?.Invoke();
            }
        }

        public bool IsNew { get; set; } = false;
        public bool IsUpdated { get; set; } = false;

        public CourseModel Clone()
        {
            return new CourseModel
            {
                CourseId = CourseId,
                StudentId = StudentId,
                CourseName = CourseName,
                Score = Score,
                StartDate = StartDate,
                EndDate = EndDate,
                PropertyInModelChange = PropertyInModelChange
            };
        }

        public void CopyTo(CourseModel target)
        {
            target.StudentId = StudentId;
            target.CourseName = CourseName;
            target.Score = Score;
            target.StartDate = StartDate;
            target.EndDate = EndDate;
        }
    }
}
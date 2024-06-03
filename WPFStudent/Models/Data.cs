using WPFStudent.Utility.StudentLoaders;
using WPFStudent.Utility.CourseLoaders;
using WPFStudent.Views;
using WPFStudent.Commands;
using WPFStudent.Utility;

namespace WPFStudent.Models
{
    public class Data
    {
        private readonly DbUpdater _dbUpdater;
        public StudentsView StudentsViewModel { get; set; }
        public CourseView CoursesViewModel { get; set; }
        public DelegateCommand CreateCourseCommand { get; set; }
        public DelegateCommand UpdateDbCommand { get; set; }

        public Data()
        {
            CreateCourseCommand = new DelegateCommand(CreateCourse, CanCreateCourse);
            UpdateDbCommand = new DelegateCommand(UpdateDb);

            CoursesViewModel = new CourseView(new CourseFromDbLoader());
            StudentsViewModel = new StudentsView(new StudentFromDbLoader(), CreateCourseCommand.RaisePropertyChangedEvent);
            _dbUpdater = new DbUpdater();

            LoadAsync();
        }

        private async void LoadAsync()
        {
            await StudentsViewModel.LoadAsync();
            await CoursesViewModel.LoadAsync();
        }

        private void UpdateDb(object? parameter) =>
            _dbUpdater.UpdateDb(StudentsViewModel.StudentModels, CoursesViewModel.CourseModels);

        private bool CanCreateCourse(object? parameter) =>
            StudentsViewModel.CurrentStudent is not null;

        private void CreateCourse(object? parameter)
        {
            CoursesViewModel.CurrentCourse = null;

            CoursesViewModel.CurrentCourse = new CourseModel()
            {
                StudentId = StudentsViewModel.CurrentStudent.StudentId
            };

            CoursesViewModel.CurrentStudenId = StudentsViewModel.CurrentStudent.StudentId;
        }
    }
}
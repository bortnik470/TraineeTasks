using WPFStudent.Utility.StudentLoaders;
using WPFStudent.Utility.CourseLoaders;
using WPFStudent.Views;

namespace WPFStudent.Models
{
    public class Data
    {
        public StudentsView StudentsViewModel { get; set; }

        public CourseView CoursesViewModel { get; set; }

        public Data()
        {
            StudentsViewModel = new StudentsView(new StudentFromDbLoader());
            CoursesViewModel = new CourseView(new CourseFromDbLoader());

            LoadAsync();
        }

        private async void LoadAsync()
        {
            await StudentsViewModel.LoadAsync();
            await CoursesViewModel.LoadAsync();
        }
    }
}
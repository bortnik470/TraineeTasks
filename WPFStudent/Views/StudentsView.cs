using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using WPFStudent.Models;
using WPFStudent.Utility;

namespace WPFStudent.Views
{
    public class StudentsView : INotifyPropertyChanged
    {
        private IStudentLoader _studentLoader;
        private StudentModel? _currentStudent;
        private CourseModel? _currentCourse;

        public event PropertyChangedEventHandler? PropertyChanged;

        public ObservableCollection<StudentModel> StudentModels { get; set; }

        public StudentModel? CurrentStudent
        {
            get => _currentStudent;
            set
            {
                _currentStudent = value;
                RaisePropertyChangedEvent();
                RaisePropertyChangedEvent(nameof(IsStudentSelected));
            }
        }

        public CourseModel? CurrentCourse
        {
            get => _currentCourse;
            set
            {
                _currentCourse = value;
                RaisePropertyChangedEvent();
                RaisePropertyChangedEvent(nameof(IsCourseSelected));
            }
        }

        public bool IsStudentSelected => CurrentStudent != null;
        public bool IsCourseSelected => CurrentCourse != null;

        public StudentsView(IStudentLoader studentLoader)
        {
            _studentLoader = studentLoader;
            StudentModels = new();
        }

        public async void LoadAsync()
        {
            var students = await _studentLoader.LoadAsync();

            foreach (var student in students)
            {
                StudentModels.Add(student);
            }
        }

        private void RaisePropertyChangedEvent([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
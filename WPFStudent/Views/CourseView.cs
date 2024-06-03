using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WPFStudent.Commands;
using WPFStudent.Models;
using WPFStudent.Utility.CourseLoaders;

namespace WPFStudent.Views
{
    public class CourseView : BaseView
    {
        private CourseModel? _currentCourse;
        private CourseModel? _currentCourseClone;
        private ICourseLoader _courseLoader;
        private ObservableCollection<CourseModel?> _courseModels;
        private int? _currentStudenId;

        public int? CurrentStudenId
        {
            get => _currentStudenId;
            set
            {
                _currentStudenId = value;
                RaisePropertyChangedEvent();
                RaisePropertyChangedEvent(nameof(CourseModels));
            }
        }

        public ObservableCollection<CourseModel?> CourseModels
        {
            get
            {
                var courses = _courseModels.Where(x => x.StudentId.Equals(CurrentStudenId));

                ObservableCollection<CourseModel?> result = new ObservableCollection<CourseModel?>();

                foreach (var course in courses)
                    result.Add(course);

                return result;
            }
            set => _courseModels = value;
        }

        public CourseModel? CurrentCourse
        {
            get => _currentCourseClone;
            set
            {
                _currentCourse = value;

                if (_currentCourse is not null)
                    _currentCourse.PropertyInModelChange = SaveCourseCommand.RaisePropertyChangedEvent;

                _currentCourseClone = _currentCourse?.Clone();
                RaisePropertyChangedEvent();
                RaisePropertyChangedEvent(nameof(IsCourseSelected));
                DeleteCourseCommand.RaisePropertyChangedEvent();
                SaveCourseCommand.RaisePropertyChangedEvent();
            }
        }

        public bool IsCourseSelected => CurrentCourse != null;

        public DelegateCommand SaveCourseCommand { get; set; }
        public DelegateCommand DeleteCourseCommand { get; set; }
        public DelegateCommand ClearCourseCommand { get; set; }

        public CourseView(ICourseLoader loader)
        {
            _courseLoader = loader;
            _courseModels = new ObservableCollection<CourseModel?>();

            SaveCourseCommand = new DelegateCommand(SaveCourse, CanSaveCourse);
            DeleteCourseCommand = new DelegateCommand(DeleteCourse, CanDeleteCourse);
            ClearCourseCommand = new DelegateCommand(ClearCourse);
        }

        public override async Task LoadAsync()
        {
            var courses = await _courseLoader.LoadAsync();

            foreach (var course in courses)
            {
                _courseModels.Add(course);
            }
        }

        private void DeleteCourse(object? parameter)
        {
            _courseModels.Remove(_currentCourse);
            RaisePropertyChangedEvent(nameof(CourseModels));
        }

        private bool CanDeleteCourse(object? parameter) =>
            _currentCourse?.CourseId is not 0;

        private void ClearCourse(object? parameter)
        {
            _currentCourseClone = _currentCourse.Clone();
            RaisePropertyChangedEvent(nameof(CurrentCourse));
        }

        private void SaveCourse(object? parameter)
        {
            _currentCourseClone?.CopyTo(_currentCourse);
            if (_currentCourse.CourseId == 0)
            {
                int id = _courseModels.LastOrDefault().CourseId;
                _currentCourse.CourseId = ++id;
                _currentCourse.IsNew = true;
                _courseModels.Add(_currentCourse);
            }
            else
                _currentCourse.IsUpdated = true;

            RaisePropertyChangedEvent(nameof(CourseModels));
        }

        private bool CanSaveCourse(object? parameter) => 
            CurrentCourse?.HasErrors is null ? false : !CurrentCourse.HasErrors &&
            CurrentCourse?.StudentId is not null;
    }
}


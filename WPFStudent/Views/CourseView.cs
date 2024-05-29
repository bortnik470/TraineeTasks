using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFStudent.Models;
using WPFStudent.Utility.CourseLoaders;

namespace WPFStudent.Views
{
    public class CourseView : BasicView
    {
        private CourseModel? _currentCourse;
        private ICourseLoader _courseLoader;
        private List<CourseModel?> _courseModels;
        private int currentStudenId;

        public int CurrentStudenId
        {
            get => currentStudenId;
            set
            {
                currentStudenId = value;
                RaisePropertyChangedEvent();
                RaisePropertyChangedEvent(nameof(CourseModels));
            }
        }

        public List<CourseModel?> CourseModels
        {
            get => _courseModels.
                Where(x => x.StudentId.Equals(CurrentStudenId)).
                ToList();
            set => _courseModels = value;
        }

        public CourseModel? CurrentCourse
        {
            get => _currentCourse?.Clone();
            set
            {
                _currentCourse = value;
                RaisePropertyChangedEvent();
                RaisePropertyChangedEvent(nameof(IsCourseSelected));
            }
        }

        public bool IsCourseSelected => CurrentCourse != null;

        public CourseView(ICourseLoader loader)
        {
            _courseLoader = loader;
            _courseModels = new List<CourseModel?>();
        }

        public override async Task LoadAsync()
        {
            var courses = await _courseLoader.LoadAsync();

            _courseModels.AddRange(courses);
        }
    }
}

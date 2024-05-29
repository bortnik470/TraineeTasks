using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using WPFStudent.Models;
using WPFStudent.Utility.StudentLoaders;

namespace WPFStudent.Views
{
    public class StudentsView : BasicView
    {
        private StudentModel? _currentStudent;
        private IStudentLoader _studentLoader;
        private List<StudentModel?> _studentModels;
        private string textForSearch;

        public List<StudentModel?> StudentModels
        {
            get
            {
                var result = string.IsNullOrEmpty(TextForSearch) ? _studentModels :
                    _studentModels.FindAll(x => x.FirstName.ToLower().
                                                Contains(TextForSearch));

                return result;
            }
            set => _studentModels = value;
        }

        public string TextForSearch
        {
            get => textForSearch;
            set
            {
                textForSearch = value;
                RaisePropertyChangedEvent();
                RaisePropertyChangedEvent(nameof(StudentModels));
            }
        }

        public StudentModel? CurrentStudent
        {
            get => _currentStudent?.Clone();
            set
            {
                if (IsSearchState) return;

                _currentStudent = value;
                RaisePropertyChangedEvent();
                RaisePropertyChangedEvent(nameof(IsStudentSelected));
            }
        }
        public bool IsSearchState {  get; set; }

        public bool IsStudentSelected => CurrentStudent != null;

        public StudentsView(IStudentLoader studentLoader)
        {
            _studentLoader = studentLoader;
            _studentModels = new List<StudentModel?>();
            textForSearch = string.Empty;
            IsSearchState = false;
        }

        public override async Task LoadAsync()
        {
            var students = await _studentLoader.LoadAsync();

            _studentModels.AddRange(students);
        }
    }
}
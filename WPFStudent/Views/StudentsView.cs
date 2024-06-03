using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using WPFStudent.Commands;
using WPFStudent.Models;
using WPFStudent.Utility.StudentLoaders;

namespace WPFStudent.Views
{
    public class StudentsView : BaseView
    {
        private StudentModel? _currentStudent;
        private StudentModel? _currentStudentClone;
        private IStudentLoader _studentLoader;
        private ObservableCollection<StudentModel?> _studentModels;
        private string textForSearch;

        public ObservableCollection<StudentModel?> StudentModels
        {
            get
            {
                if (string.IsNullOrEmpty(TextForSearch)) return _studentModels;

                return FindStudents();
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
            get => _currentStudentClone;
            set
            {
                if (IsSearchState) return;

                _currentStudent = value;

                if (value is null)
                {
                    _currentStudentClone = null;
                }
                else
                {
                    _currentStudent.PropertyInModelChange = SaveStudentCommand.RaisePropertyChangedEvent;
                    _currentStudentClone = _currentStudent?.Clone();
                }

                RaisePropertyChangedEvent();
                RaisePropertyChangedEvent(nameof(IsStudentSelected));
                DeleteStudentCommand.RaisePropertyChangedEvent();
                SaveStudentCommand.RaisePropertyChangedEvent();
                StudentChangedEventRaise?.Invoke();
            }
        }


        public int LastStudentId { get; set; }

        public bool IsSearchState { get; set; }
        public bool IsStudentSelected => CurrentStudent != null;

        public Action StudentChangedEventRaise { get; set; }

        public DelegateCommand SaveStudentCommand { get; set; }
        public DelegateCommand DeleteStudentCommand { get; set; }
        public DelegateCommand ClearStudentCommand { get; set; }

        public StudentsView(IStudentLoader studentLoader, Action studentChangedEventRaise)
        {
            _studentLoader = studentLoader;
            _studentModels = new ObservableCollection<StudentModel?>();
            textForSearch = string.Empty;
            IsSearchState = false;
            LastStudentId = _studentLoader.GetLastStudentId();

            StudentChangedEventRaise = studentChangedEventRaise;
            SaveStudentCommand = new DelegateCommand(SaveStudent, CanSaveStudent);
            DeleteStudentCommand = new DelegateCommand(DeleteStudent, CanDeleteStudent);
            ClearStudentCommand = new DelegateCommand(ClearStudent);
        }

        public override async Task LoadAsync()
        {
            var students = await _studentLoader.LoadAsync();

            foreach (var student in students)
            {
                _studentModels.Add(student);
            }
        }

        private void DeleteStudent(object? parameter)
        {
            _studentModels.Remove(_currentStudent);
            RaisePropertyChangedEvent(nameof(StudentModels));
        }
        private bool CanDeleteStudent(object? parameter) =>
            _currentStudent?.StudentId != default;

        private void SaveStudent(object? parameter)
        {
            _currentStudentClone?.CopyTo(_currentStudent);

            if (_currentStudent?.StudentId == 0)
            {
                _currentStudent.StudentId = ++LastStudentId;
                _currentStudent.IsNew = true;
                _studentModels.Add(_currentStudent);
            }
            else
                if (!_currentStudent.IsNew)
                _currentStudent.IsUpdated = true;

            CurrentStudent = null;
            RaisePropertyChangedEvent(nameof(StudentModels));
            RaisePropertyChangedEvent(nameof(CurrentStudent));
        }

        private bool CanSaveStudent(object? parameter) => CurrentStudent?.HasErrors is null ? false : !CurrentStudent.HasErrors;

        private void ClearStudent(object? parameter)
        {
            _currentStudentClone = _currentStudent?.Clone();
            RaisePropertyChangedEvent(nameof(CurrentStudent));
        }

        private ObservableCollection<StudentModel?> FindStudents()
        {
            var result = _studentModels.Where(x => x.FirstName.ToLower().
                                                Contains(TextForSearch.ToLower()));

            var students = new ObservableCollection<StudentModel?>();

            foreach (var item in result)
                students.Add(item);

            return students;
        }
    }
}
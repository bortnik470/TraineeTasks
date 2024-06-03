using System.Collections;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WPFStudent.Views
{
    public abstract class BaseModel : INotifyPropertyChanged, INotifyDataErrorInfo
    {
        private Dictionary<string, List<string>> _errorsByPropName = new();

        public bool HasErrors => ErrorsByPropName.Any();

        public Dictionary<string, List<string>> ErrorsByPropName => _errorsByPropName;

        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;
        public event PropertyChangedEventHandler? PropertyChanged;

        public IEnumerable GetErrors(string? propertyName)
        {
            return propertyName is not null && ErrorsByPropName.ContainsKey(propertyName)
                ? ErrorsByPropName[propertyName] : Enumerable.Empty<string>();
        }

        public void RaisePropertyChangedEvent([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void SetPropertyInModelChange(Action action)
        {
            PropertyInModelChange = action;
        }

        public Action PropertyInModelChange;

        protected void AddError(string propertyName, string error)
        {
            if (!ErrorsByPropName.ContainsKey(propertyName))
                ErrorsByPropName.Add(propertyName, new());
            if (!ErrorsByPropName[propertyName].Contains(error))
            {
                ErrorsByPropName[propertyName].Add(error);
                OnErrorChanged(new DataErrorsChangedEventArgs(propertyName));
                RaisePropertyChangedEvent(propertyName);
                RaisePropertyChangedEvent(nameof(HasErrors));
            }
        }

        protected void ClearError(string propertyName, string errorName)
        {
            if (ErrorsByPropName.ContainsKey(propertyName))
            {
                ErrorsByPropName[propertyName].Remove(errorName);
                if (!ErrorsByPropName[propertyName].Any())
                {
                    ErrorsByPropName.Remove(propertyName);
                }
                OnErrorChanged(new DataErrorsChangedEventArgs(propertyName));
                RaisePropertyChangedEvent(propertyName);
                RaisePropertyChangedEvent(nameof(HasErrors));
            }
        }

        protected virtual void OnErrorChanged(DataErrorsChangedEventArgs e) =>
            ErrorsChanged?.Invoke(this, e);
    }
}
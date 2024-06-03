using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFStudent.Views;

namespace WPFStudent.Models
{
    public class StudentModel : BaseModel, IDbValue
    {
        private string _firstName;
        private string _lastName;
        private string? _groupName = "None";

        public StudentModel()
        {
            FirstName = string.Empty;
            LastName = string.Empty;
        }

        public int StudentId { get; set; }
        public string FirstName
        {
            get => _firstName;
            set
            {
                if (string.IsNullOrEmpty(value))
                    AddError(nameof(FirstName), "First name is empty");
                else
                    ClearError(nameof(FirstName), "First name is empty");

                _firstName = value;
                PropertyInModelChange?.Invoke();
            }
        }
        public string LastName
        {
            get => _lastName;
            set
            {
                if (string.IsNullOrEmpty(value))
                    AddError(nameof(LastName), "Last name is empty");
                else
                    ClearError(nameof(LastName), "Last name is empty");

                _lastName = value;
                PropertyInModelChange?.Invoke();
            }
        }
        public string? PhoneNumber { get; set; }
        public string? GroupName
        {
            get => _groupName;
            set
            {
                if (value.Length > 5)
                    AddError(nameof(GroupName), "Group name max length is 5");
                else
                    ClearError(nameof(GroupName), "Group name max length is 5");

                RaisePropertyChangedEvent();
                _groupName = value;
            }
        }
        public bool IsNew { get; set; } = false;
        public bool IsUpdated { get; set; } = false;

        public StudentModel Clone()
        {
            return new StudentModel()
            {
                StudentId = StudentId,
                FirstName = FirstName,
                LastName = LastName,
                PhoneNumber = PhoneNumber,
                GroupName = GroupName,
                PropertyInModelChange = PropertyInModelChange,
            };
        }

        public void CopyTo(StudentModel target)
        {
            target.FirstName = FirstName;
            target.LastName = LastName;
            target.PhoneNumber = PhoneNumber;
            target.GroupName = GroupName;
        }

        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFStudent.Models
{
    public class StudentModel
    {
        public int StudentId { get; init; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? GroupName { get; set; }

        public StudentModel Clone()
        {
            return new StudentModel()
            {
                StudentId = StudentId,
                FirstName = FirstName,
                LastName = LastName,
                PhoneNumber = PhoneNumber,
                GroupName = GroupName
            };
        }

        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }
    }
}

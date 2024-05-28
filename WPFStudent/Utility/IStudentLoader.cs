using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFStudent.Models;

namespace WPFStudent.Utility
{
    public interface IStudentLoader
    {
        Task<IEnumerable<StudentModel>> LoadAsync();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFStudent.Models;

namespace WPFStudent.Utility.CourseLoaders
{
    public interface ICourseLoader
    {
        Task<IEnumerable<CourseModel>> LoadAsync();
    }
}

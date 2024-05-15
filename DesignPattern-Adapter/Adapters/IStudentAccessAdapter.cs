using DesignPattern_Adapter.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern_Adapter.Adapters
{
    internal interface IStudentAccessAdapter
    {
        public Student GetStudent(int id);
    }
}

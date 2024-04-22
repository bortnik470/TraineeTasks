using StudentsInfo.Data.DataModels;
using StudentsInfo.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentsInfo.Data.FinishedClasses.Student
{
    public partial class StudentDbAccessor
    {
        public void DeleteStudentById(int id)
        {
            var KVT = new List<KeyValueType>
            {
                new KeyValueType("studentId", id, "number")
            };

            DeleteDisciplinesByStudentId(id);

            dbRepository.DeleteData(stTableName, KVT);
        }

        public void DeleteDisciplinesByStudentId(int studentId)
        {
            var KVT = new List<KeyValueType>
            {
                new KeyValueType("studentId", studentId, "number")
            };

            dbRepository.DeleteData(discTableName, KVT);
        }
    }
}

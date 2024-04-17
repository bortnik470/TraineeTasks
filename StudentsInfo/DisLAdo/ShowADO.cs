using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentsInfo
{
    public partial class DisADO
    {
        public void ShowAllStudent()
        {
            foreach (DataColumn column in dataSet.Tables["Student"].Columns)
            {
                Console.Write($"{column.ColumnName}\t\t");
            }
            Console.WriteLine();

            foreach (DataRow row in dataSet.Tables["Student"].AsEnumerable())
            {
                Console.WriteLine($"{row[0]}\t\t{row[1]}\t\t{row[2]}\t\t{row[3]}\t\t{row[4]}\n");
            }
        }

        public void ShowAllDisciplines()
        {
            foreach (DataColumn column in dataSet.Tables["Discipline"].Columns)
            {
                Console.Write($"{column.ColumnName}\t\t");
            }
            Console.WriteLine();

            foreach (DataRow row in dataSet.Tables["Discipline"].AsEnumerable())
            {
                Console.WriteLine($"{row[0]}\t\t{row[1]}\t\t{row[2]}\t\t{row[3]}\t\t{row[4]}\t\t{row[5]}");
            }
        }
    }
}

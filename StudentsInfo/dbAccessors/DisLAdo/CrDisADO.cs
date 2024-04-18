using System.Data;

namespace StudentsInfo
{
    public partial class DisADO
    {
        private DataSet dataSet;

        public DisADO(string dataSetName)
        {
            dataSet = new DataSet(dataSetName);
        }

        public void CreateDisciplineTable()
        {
            DataTable dt = new DataTable("Discipline");

            DataColumn idColumn = new DataColumn("id", typeof(int))
            {
                Unique = true,
                AllowDBNull = false,
                AutoIncrement = true,
                AutoIncrementSeed = 1,
                AutoIncrementStep = 1
            };

            DataColumn nameColumn = new DataColumn("dName", typeof(string));
            DataColumn startTimeColumn = new DataColumn("startTime", typeof(DateOnly));
            DataColumn endTimeColumn = new DataColumn("endTime", typeof(DateOnly));
            DataColumn scoreColumn = new DataColumn("score", typeof(string));
            DataColumn studentIdColumn = new DataColumn("studentID", typeof(int));

            dt.Columns.AddRange([
                idColumn, nameColumn, startTimeColumn,
                endTimeColumn, scoreColumn, studentIdColumn
                ]);

            dataSet.Tables.Add(dt);
        }

        public void CreateStudentTable()
        {
            DataTable dt = new DataTable("Student");

            DataColumn idColumn = new DataColumn("id", typeof(int))
            {
                ReadOnly = true,
                Unique = true,
                AllowDBNull = false,
                AutoIncrement = true,
                AutoIncrementSeed = 1,
                AutoIncrementStep = 1
            };

            DataColumn fNameColumn = new DataColumn("fName", typeof(string));
            DataColumn sNameColumn = new DataColumn("lName", typeof(string));
            DataColumn phoneNumColumn = new DataColumn("pNumber", typeof(string))
            {
                Unique = true,
                AllowDBNull = true
            };
            DataColumn groupNumColumn = new DataColumn("gNumber", typeof(string));

            dt.Columns.AddRange([
                idColumn, fNameColumn, sNameColumn,
                phoneNumColumn, groupNumColumn
                ]);

            dt.PrimaryKey = [idColumn];

            dataSet.Tables.Add(dt);
        }
    }
}

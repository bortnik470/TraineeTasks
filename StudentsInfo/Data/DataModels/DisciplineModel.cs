using StudentsInfo.Enums;

namespace StudentsInfo.DataModels
{
    [Serializable]
    public class DisciplineModel
    {
        public DisciplineModel()
        {
        }

        public DisciplineModel(DisciplineName disciplineName, Score score, DateTime startDate, DateTime endDate)
        {
            this.disciplineName = disciplineName;
            this.score = score;
            this.startDate = startDate;
            this.endDate = endDate;

            tableName = "Discipline";
        }

        public DisciplineName disciplineName { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public Score score { get; set; }

        public string tableName { get; private set; }

        public void FillDbModel(object[] values)
        {
            throw new NotImplementedException();
        }
    }
}

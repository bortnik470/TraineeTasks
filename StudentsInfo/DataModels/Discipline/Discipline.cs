using StudentsInfo.DataModels.Utility;
using StudentsInfo.Enums;

namespace StudentsInfo.DataModels.Disciplines
{
    [Serializable]
    public class Discipline
    {
        public Discipline()
        {
        }

        public Discipline(DisciplineName disciplineName, Score score, DateModel startDate, DateModel endDate)
        {
            this.disciplineName = disciplineName;
            this.score = score;
            this.startDate = startDate;
            this.endDate = endDate;
        }

        public DisciplineName disciplineName { get; set; }
        public DateModel startDate { get; set; }
        public DateModel endDate { get; set; }
        public Score score { get; set; }
    }
}

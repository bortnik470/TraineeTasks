using StudentsInfo.Enums;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace StudentsInfo.DataModels.Disciplines
{
    [Serializable]
    public class Discipline
    {
        public Discipline()
        {
        }

        public Discipline(DisciplineName disciplineName, Score score, DateTime startDate, DateTime endDate)
        {
            this.disciplineName = disciplineName;
            this.score = score;
            this.startDate = startDate;
            this.endDate = endDate;
        }

        public DisciplineName disciplineName { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public Score score { get; set; }
    }
}

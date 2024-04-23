using StudentsInfo.DataModels;
using StudentsInfo.Enums;
using System.Text;

namespace StudentsInfo
{
    [Serializable]
    public class Student : StudentModel
    {
        public Student() { }
        public Student(string firstName, string lastName, string phoneNumber, string groupName, List<DisciplineModel> disciplines) :
                  base(firstName, lastName, phoneNumber, groupName, disciplines)
        {
        }

        public void SetScoreForDiscipline(DisciplineName disciplineName, Score score)
        {
            DisciplineModel discipline = disciplines.Find(x => disciplineName.Equals(x.disciplineName));
            if (discipline == null)
            {
                Console.WriteLine($"{firstName} {lastName} doen`t have {disciplineName} discipline");
            }
            else discipline.score = score;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Student`s name is: {firstName} {lastName}");
            sb.AppendLine($"His phone number is: {phoneNumber}");
            sb.AppendLine($"His group is: {groupName}");
            sb.AppendLine($"He has next disciplines: ");
            foreach (var discipline in disciplines)
            {
                sb.AppendLine($"\tDiscipline name: {discipline.disciplineName}");
                sb.AppendLine($"\tDiscipline period: from {DateOnly.FromDateTime(discipline.startDate)} " +
                    $"to {DateOnly.FromDateTime(discipline.endDate)}");
                if (!discipline.score.Equals(Score.None))
                {
                    sb.AppendLine($"\tStudent`s score is {discipline.score.ToString()}\n");
                }
                else sb.AppendLine($"\tStudent hasn`t completed the discipline\n");
            }

            return sb.ToString();
        }
    }
}
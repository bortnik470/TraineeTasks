using StudentsInfo.AbstractClases;
using StudentsInfo.DataModels.Disciplines;
using StudentsInfo.Enums;

namespace StudentsInfo.DataModels.Student
{
    [Serializable]
    public class Student : BaseStudent
    {
        public Student() { }
        public Student(string firsName, string lastName, int phoneNumber, string groupName, List<Discipline> disciplines) :
                  base(firsName, lastName, phoneNumber, groupName, disciplines)
        {
        }

        public void SetScoreForDiscipline(DisciplineName disciplineName, Score score)
        {
            Discipline discipline = disciplines.Find(x => disciplineName.Equals(x.disciplineName));
            if(discipline == null)
            {
                Console.WriteLine($"{firsName} {lastName} doen`t have {disciplineName} discipline");
            } else discipline.score = score;
        }
    }
}
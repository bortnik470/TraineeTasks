using StudentsInfo.DataModels.Disciplines;

namespace StudentsInfo.Interfaces
{
    public interface IStudent : IPerson
    {
        public string groupName { get; set; }
        public List<Discipline> disciplines { get; set; }
    }
}

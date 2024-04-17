using StudentsInfo.DataModels;

namespace StudentsInfo.Interfaces
{
    public interface IStudent : IPerson
    {
        public string groupName { get; set; }
        public List<DisciplineModel> disciplines { get; set; }
    }
}

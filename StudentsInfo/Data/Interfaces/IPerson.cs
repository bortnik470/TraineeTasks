namespace StudentsInfo.Interfaces
{
    public interface IPerson : IDbModel
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string phoneNumber { get; set; }
    }
}

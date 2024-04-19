using StudentsInfo.Data.DataModels;

namespace StudentsInfo.Interfaces
{
    public interface IRepository
    {
        string connectionString { get; }

        public List<object[]> GetDataFromTable(string tableName);

        public List<object[]> GetSomeDateFromTable(string tableName, 
            List<KeyValueType> valuesForFinder);

        public void DeleteData(string tableName, 
            List<KeyValueType> valuesForFinder);

        public void UpdateData(string tableName, 
            List<KeyValueType> valuesForFinder, 
            List<KeyValueType> updateData);

        public void InsertData(string tableName, List<KeyValueType> data);
    }
}
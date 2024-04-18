using StudentsInfo.ExtraModules.DateTypes;

namespace StudentsInfo.Interfaces
{
    public interface IRepository
    {
        string connectionString { get; }

        public List<object> GetDataFromTable(string tableName);

        public List<object> GetSomeDateFromTable(string tableName, List<KeyValue> valuesForFinder);

        public void DeleteData(string tableName, List<KeyValue> valuesForFinder);

        public void UpdateData(string tableName, List<KeyValue> valuesForFinder, List<KeyValue> updateData);

        public void InsertData(string tableName, params object[] data);
    }
}
namespace StudentsInfo.Interfaces
{
    public interface IRepository
    {
        string connectionString { get; }

        public List<object> GetDataFromTable(string tableName);

        public void DeleteData(string tableName, object key, string prKeyName);

        public void UpdateData(string tableName, object key, string prKeyName, string columnName, object value);

        public void InsertData(string tableName, params object[] data);
    }
}
namespace WPFStudent.Models
{
    public interface IDbValue
    {
        bool IsNew { get; }
        bool IsUpdated { get; }
    }
}
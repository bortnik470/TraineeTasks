namespace StudentsInfo.DataModels.Utility
{
    public class DateModel
    {
        public int year;
        public int month;
        public int day;

        public DateModel()
        {
        }

        public DateModel(int year, int month, int day)
        {
            this.year = year;
            this.month = month;
            this.day = day;
        }
    }
}

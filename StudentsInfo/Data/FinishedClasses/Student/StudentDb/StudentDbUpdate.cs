using StudentsInfo.Data.DataModels;
using StudentsInfo.Enums;

namespace StudentsInfo.Data.FinishedClasses.StudentClass
{
    public partial class StudentDbAccessor
    {
        private void BasicStudentUpdate(int id, List<KeyValueType> setKVT)
        {
            var whereKVT = new List<KeyValueType>
            {
                new KeyValueType("studentId", id, "number")
            };

            dbRepository.UpdateData(stTableName, whereKVT, setKVT);
        }

        private void BasicDisciplineUpdate(int id, List<KeyValueType> setKVT)
        {
            var whereKVT = new List<KeyValueType>
            {
                new KeyValueType("disciplineId", id, "number")
            };

            dbRepository.UpdateData(discTableName, whereKVT, setKVT);
        }

        public void UpdateStudentNameInfoById(int id, string firstName, string lastName)
        {
            var setKVT = new List<KeyValueType>
            {
                new KeyValueType("firstName", firstName),
                new KeyValueType("lastName", lastName)
            };

            BasicStudentUpdate(id, setKVT);
        }

        public void UpdateStudentPhoneNumInfoById(int id, string phoneNumber)
        {
            var setKVT = new List<KeyValueType>
            {
                new KeyValueType("phoneNumber", phoneNumber)
            };

            BasicStudentUpdate(id, setKVT);
        }

        public void UpdateStudentGroupNameInfoById(int id, string groupName)
        {
            var setKVT = new List<KeyValueType>
            {
                new KeyValueType("groupName", groupName)
            };

            BasicStudentUpdate(id, setKVT);
        }

        public void UpdateDisciplineDateById(int id, DateTime startDate, DateTime endDate)
        {
            var setKVT = new List<KeyValueType>
            {
                new KeyValueType("startDate", DateTimeToString(startDate)),
                new KeyValueType("endDate", DateTimeToString(endDate))
            };

            BasicDisciplineUpdate(id, setKVT);
        }

        public void UpdateDisciplineScoreById(int id, Score score)
        {
            var setKVT = new List<KeyValueType>
            {
                new KeyValueType("score", score.ToString())
            };

            BasicDisciplineUpdate(id, setKVT);
        }
    }
}

namespace ADO_Net_demo.DAL
{
    internal interface IStudentsRepo
    {
        List<Student> GetList();

        Student GetById(int id);

        Student Update(Student student);

        void Delete(int id);

        Student Insert(Student student);
    }
}

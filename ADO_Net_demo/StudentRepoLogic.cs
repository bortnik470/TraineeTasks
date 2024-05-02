using ADO_Net_demo.DAL;

namespace ADO_Net_demo
{
    public class StudentRepoLogic
    {
        private IStudentsRepo studentsRepo;

        public StudentRepoLogic(IStudentsRepo studentsRepo)
        {
            this.studentsRepo = studentsRepo;
        }

        public Student AddStudent(Student student)
        {
            return studentsRepo.Insert(student);
        }

        public Student UpdateStudent(Student student)
        {
            return studentsRepo.Update(student);
        }

        public void DeleteStudent(int studentId)
        {
            studentsRepo.Delete(studentId);
        }

        public Student GetStudentById(int studentId)
        {
            return studentsRepo.GetById(studentId);
        }

        public List<Student> GetAllStudents()
        {
            return studentsRepo.GetList();
        }
    }
}

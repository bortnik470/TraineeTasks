using ADO_Net_demo;
using ADO_Net_demo.DAL;

var StudentRepo = new StudentRepoLogic(new DataAccessEF());

var test = new LinqToXml();

test.AddStudent(StudentRepo.GetStudentById(5));

test.GetStudents();
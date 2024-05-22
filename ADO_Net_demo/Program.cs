using ADO_Net_demo;
using ADO_Net_demo.DAL;

var StudentRepo = new ADO_Net_demo.StudentRepoLogic(new DataAccessEF());

var test = new LinqToXml();

test.AddStudent(StudentRepo.GetStudentById(27));

test.GetStudents();
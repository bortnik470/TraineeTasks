using ADO_Net_demo;
using ADO_Net_demo.DAL;

var StudentRepo = new StudentRepoLogic(new DataAccessEF());

var students = StudentRepo.GetAllStudents();

var student = StudentRepo.GetStudentById(5);

foreach (var st in students)
{
    Console.WriteLine(st);
}

Console.WriteLine("----------------------------------------------\n\n");

Console.WriteLine(student);

var studentToAdd = new Student("Vlad", "Smith", "3609432698", "2B", new List<Course> {
                    new Course("PE", "A",
                        new DateOnly(1992, 2, 24), new DateOnly(1993, 2, 23)),
                    new Course("Art", "C",
                        new DateOnly(1992, 2, 22), new DateOnly(1993, 2, 23))
                    });

//StudentRepo.AddStudent(studentToAdd);

studentToAdd.FirstName = "Igor";
studentToAdd.Courses.ToList()[0].CourseName = "Chemistry";

StudentRepo.UpdateStudent(studentToAdd);

//StudentRepo.DeleteStudent(46);
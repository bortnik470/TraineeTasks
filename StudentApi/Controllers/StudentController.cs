using ADO_Net_demo;
using ADO_Net_demo.DAL;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using StudentApi.Models;
using StudentApi.Utility;

namespace StudentApi.Controllers
{
    [Route("api/students")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly StudentRepoLogic _studentsRepo;

        public StudentController(StudentRepoLogic studentsRepo)
        {
            _studentsRepo = studentsRepo;
        }

        [HttpGet]
        public ActionResult<Student> GetStudents()
        {
            var studentsList = _studentsRepo.GetAllStudents();

            return Ok(studentsList);
        }

        [HttpGet("{id}")]
        public ActionResult<Student> GetStudent(int id)
        {
            var student = _studentsRepo.GetStudentById(id);

            if (student == null)
                return NotFound();

            return Ok(student);
        }

        [HttpDelete]
        public ActionResult DeleteStudent(int id)
        {
            _studentsRepo.DeleteStudent(id);

            return NoContent();
        }

        [HttpPost]
        public ActionResult CreateStudent(StudentToCreate studentToCreate)
        {
            var student = new Student().TransformFromStudentToCreate(studentToCreate);

            _studentsRepo.AddStudent(student);

            return Created();
        }

        [HttpPut]
        public ActionResult UpdateStudent(StudentToCreate studentToUpdate, int id)
        {
            if (_studentsRepo.GetStudentById(id) == null)
                return NotFound();

            var student = new Student().TransformFromStudentToCreate(studentToUpdate, id);

            _studentsRepo.UpdateStudent(student);

            return NoContent();
        }

        [HttpPatch]
        public ActionResult PathStudent(JsonPatchDocument<StudentToCreate> patchDocument, int id)
        {
            var studentToUpdate = _studentsRepo.GetStudentById(id);

            if (studentToUpdate == null)
                return NotFound();

            var pathedStudent = new StudentToCreate().TransformFromDbStudent(studentToUpdate);

            patchDocument.ApplyTo(pathedStudent, ModelState);

            studentToUpdate = studentToUpdate.TransformFromStudentToCreate(pathedStudent);

            _studentsRepo.UpdateStudent(studentToUpdate);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return NoContent();
        }
    }
}
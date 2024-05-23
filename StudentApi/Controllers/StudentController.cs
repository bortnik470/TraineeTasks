using ADO_Net_demo;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using StudentApi.Models;

namespace StudentApi.Controllers
{
    [Route("api/students")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly StudentRepoLogic _studentsRepo;
        private readonly IMapper _mapper;

        public StudentController(StudentRepoLogic studentsRepo,
            IMapper mapper)
        {
            _studentsRepo = studentsRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Student>> GetStudents()
        {
            var studentsList = _studentsRepo.GetAllStudents() as IEnumerable<Student>;

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
            var student = _mapper.Map<Student>(studentToCreate);

            _studentsRepo.AddStudent(student);

            return Created();
        }

        [HttpPut]
        public ActionResult UpdateStudent(StudentToCreate studentToUpdate, int id)
        {
            if (_studentsRepo.GetStudentById(id) == null)
                return NotFound();

            var student = _mapper.Map<Student>(studentToUpdate);
            student.StudentId = id;

            _studentsRepo.UpdateStudent(student);

            return NoContent();
        }

        [HttpPatch]
        public ActionResult PathStudent(JsonPatchDocument<StudentToCreate> patchDocument, int id)
        {
            var studentToUpdate = _studentsRepo.GetStudentById(id);

            if (studentToUpdate == null)
                return NotFound();

            var pathedStudent = _mapper.Map<StudentToCreate>(studentToUpdate);

            patchDocument.ApplyTo(pathedStudent, ModelState);

            studentToUpdate = _mapper.Map<Student>(pathedStudent);

            _studentsRepo.UpdateStudent(studentToUpdate);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return NoContent();
        }
    }
}
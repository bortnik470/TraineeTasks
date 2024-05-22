using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace TestWebApi.Controllers
{
    [Route("api/student")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        [HttpGet]
        public ActionResult<string> Get()
        {
            JsonResult jsonResult = new JsonResult("Hello");

            return Ok("hello");
        }
    }
}

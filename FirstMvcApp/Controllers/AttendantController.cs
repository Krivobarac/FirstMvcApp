using FirstMvcApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace FirstMvcApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendantController : ControllerBase
    {
        // GET: api/<AttendantController>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(Attendance.GetAttendants());
        }

        // GET api/<AttendantController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(Attendance.GetAttendant(id));
        }

        // POST api/<AttendantController>
        [HttpPost]
        public ActionResult<Person> Post([FromBody] Person person)
        {
            Attendance.AddAttendant(person);
            return CreatedAtAction(nameof(Get), new { id = person.Id }, person);
        }

        [HttpOptions]
        public IActionResult Options()
        {
            Response.Headers.Add("Access-Control-Allow-Origin", new[] { (string)Request.Headers["Origin"] });
            Response.Headers.Add("Access-Control-Allow-Headers", new[] { "Origin, X-Requested-With, Content-Type, Accept" });
            Response.Headers.Add("Access-Control-Allow-Methods", new[] { "GET, POST, PUT, DELETE, OPTIONS" });
            Response.Headers.Add("Access-Control-Allow-Credentials", new[] { "true" });
            return NoContent();
        }
    }
}

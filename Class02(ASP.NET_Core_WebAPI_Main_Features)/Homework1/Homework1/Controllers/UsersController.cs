using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Homework1.Controllers
{
    [Route("api/[controller]")] //http://localhost:[port]/api/users
    [ApiController] //  API controller
    public class UsersController : ControllerBase
    {
        // GET: api/users
        [HttpGet]
        public ActionResult<List<string>> GetAllUsers()
        {
            return Ok(StaticDb.Users);
        }

        // GET: api/users/{index}
        [HttpGet("{index}")]
        public ActionResult<string> GetUserByIndex(int index)
        {
            try
            {
                if (index < 0)
                {
                    return StatusCode(StatusCodes.Status400BadRequest,
                        "The index has negative value");
                }

                if (index >= StaticDb.Users.Count)
                {
                    return StatusCode(StatusCodes.Status404NotFound,
                        $"There is no resource on index {index}");
                }

                return StatusCode(StatusCodes.Status200OK, StaticDb.Users[index]);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "An error occurred. Contact The admin");
            }
        }
    }
 }


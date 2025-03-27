using FakeAPIApp.Models;
using FakeAPIApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace FakeAPIApp.Controllers
{
    [ApiController]
    [Route("users")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        // GET /users
        [HttpGet()]
        public IActionResult GetUsers()
        {
            var user = _userService.GetUsers();
            if (user == null)
                return NotFound();

            return Ok(user);
        }

        // GET /users/{id}
        [HttpGet("{id}")]
        public IActionResult GetUserById(long id)
        {
            var user = _userService.GetUser(id);
            if (user == null)
                return NotFound();

            return Ok(user);
        }

        // POST /users
        [HttpPost()]
        public IActionResult CreateUser([FromBody] User updatedUser)
        {
            var user = _userService.CreateUser(updatedUser);
            if (user == null)
                return BadRequest();

            return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
        }

        // PUT /users/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateUser(long id, [FromBody] User updatedUser)
        {
            if(!_userService.UserExists(id))
                return NotFound();
            var user = _userService.Update(id, updatedUser);
            if (user == null)
                return BadRequest();

            return Ok(user);
        }

        // PATCH /users/{id}
        [HttpPatch("{id}")]
        public IActionResult PatchUser(long id, [FromBody] User partialUser)
        {
            var user = _userService.Patch(id, partialUser);
            if (user == null)
                return NotFound();

            return Ok(user);
        }

        // DELETE /users/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(long id)
        {
            var removed = _userService.Delete(id);
            if (!removed)
                return NotFound();

            return NoContent();
        }
    }
}

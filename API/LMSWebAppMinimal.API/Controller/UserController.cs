using LMSWebAppMinimal.API.DTO;
using LMSWebAppMinimal.Application.Interface;
using Microsoft.AspNetCore.Mvc;

namespace LMSWebAppMinimal.API.Controllers
{
    [Route("api/users")]
    [ApiController]
    [Produces("application/json")]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        [HttpGet]
        public IActionResult GetUsers()
        {
            var users = userService.GetAllUsers();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
            try
            {
                var user = userService.GetUser(id);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return NotFound("User with Id not found.");
            }
        }

        [HttpPost]
        public IActionResult AddUser([FromBody] CreateUserDTO CreateUserDTO)
        {
            try
            {
                var user = userService.AddUser(CreateUserDTO.Name, CreateUserDTO.Type);
                return CreatedAtAction(nameof(user), new { id = user.Id }, user);
            }
            catch (Exception ex)
            {
                return BadRequest("User could not be created.");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            try
            {
                var user = userService.RemoveUser(id);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return NotFound("User with Id could not be deleted.");
            }
        }
    }
}
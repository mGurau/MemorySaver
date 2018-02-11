using MemorySaver.Domain.ServiceContracts.DTOs.Request;
using MemorySaver.Domain.ServiceContracts.DTOs.Response;
using MemorySaver.Domain.ServiceContracts.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MemorySaver.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Account")]
    public class AccountController : Controller
    {
        private readonly IUserService userService;

        public AccountController(IUserService userService)
        {
            this.userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult CreateUser([FromBody] CreateUserRequestDTO newUser)
        {
            GenericResponseDTO response = userService.CreateUser(newUser);
            if (response.Success)
            {
                return Ok();
            }
            return BadRequest(response.Message);
        }
    }
}
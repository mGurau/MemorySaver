using System;
using EnsureThat;
using MemorySaver.Domain.ServiceContracts.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MemorySaver.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/User")]
    public class UserController : Controller
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            Ensure.That(userService).IsNotNull();

            this.userService = userService;
        }

        [HttpGet("{id:guid}/chests")]
        public IActionResult GetUserChests(Guid id)
        {
            var result = userService.GetUserChests(id);

            return Ok(result);
        }



    }
}

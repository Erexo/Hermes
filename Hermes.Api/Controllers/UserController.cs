using Hermes.Infrastructure.Commands;
using Hermes.Infrastructure.Commands.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Hermes.Api.Controllers
{
    public class UserController : BaseController
    {
        public UserController(ICommandDispatcher commandDispatcher)
            :base(commandDispatcher)
        {
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody]CreateUser command)
        {
            await DispatchAsync(command);
            return Created(string.Empty, null);
        }

        [HttpGet]
        [Route("login")]
        public async Task<IActionResult> Login(LoginUser command)
        {
            await DispatchAsync(command);
            return Json(command.Jwt);
        }

        [HttpPut]
        [Authorize]
        [Route("password")]
        public async Task<IActionResult> Password([FromBody]ChangePassword command)
        {
            await DispatchAsync(command);
            return Ok();
        }

        [HttpPut]
        [Authorize]
        [Route("email")]
        public async Task<IActionResult> Email([FromBody]ChangeEmail command)
        {
            await DispatchAsync(command);
            return Ok();
        }

        [HttpPut]
        [Authorize]
        [Route("registerEmail")]
        public async Task<IActionResult> RegisterEmail([FromBody]RegisterEmail command)
        {
            await DispatchAsync(command);
            return Ok();
        }
    }
}

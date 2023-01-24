using AspNetIdentityOnly.Models;
using AspNetIdentityOnly.Services;
using Microsoft.AspNetCore.Mvc;

namespace AspNetIdentityOnly.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("UserRegister")]
        public async Task<ActionResult> UserRegister(RequestUserRegisterDTO requestUserRegisterDTO)
        {
            IdentityResultDTO result = await _userService.RegisterAsync(requestUserRegisterDTO.username, requestUserRegisterDTO.password);

            

            if (result.Errors is not null)
            {
                return BadRequest(result.Errors);
            }
            return Ok(result);
        }

        [HttpPost("UserLogin")]
        public async Task<ActionResult> UserLogin(RequestUserLoginDTO requestUserLoginDTO)
        {
            IdentityResultDTO result = await _userService.LoginAsync(requestUserLoginDTO.username, requestUserLoginDTO.password);
            if (result.Errors is not null)
            {
                return BadRequest(result.Errors);
            }
            return Ok(result);
        }
    }
}

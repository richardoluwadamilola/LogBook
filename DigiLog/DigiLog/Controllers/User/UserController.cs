using DigiLog.Data;
using DigiLog.DTOs;
using DigiLog.Services.Abstraction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DigiLog.Controllers.Admin
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

        [HttpPost("create-user")]
        //[Authorize(Roles = "Admin")]
        public IActionResult CreateUser([FromBody] UserDTO UserDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(UserDto);

            var createdUser = _userService.CreateUser(UserDto);
            return Ok(createdUser);
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserLoginDTO userLoginDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(userLoginDto);

            var response = _userService.Login(userLoginDto);
            return Ok(response);
        }

        [HttpPost("change-password")]
        public IActionResult ChangePassword([FromBody] ChangePasswordDTO changePasswordDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(changePasswordDto);

            var response = _userService.ChangePassword(changePasswordDto);
            return Ok(response);
        }

        [HttpDelete]
        //[Authorize (Roles = "Admin")]
        public IActionResult DeleteUser(string username)
        {
            var response = _userService.DeleteUser(username);
            return Ok(response);
        }
    }
}

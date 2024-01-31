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
        public IActionResult CreateUser([FromBody] RegisterUserDTO registerUserDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(registerUserDto);

            var createdUser = _userService.CreateUser(registerUserDto);
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

        [HttpDelete("delete-user")]
        //[Authorize (Roles = "Admin")]
        public IActionResult DeleteUser([FromBody] UserDTO userDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Call the service to delete the user
            var response = _userService.DeleteUser(userDTO.Username);

            if (response.HasError)
            {
                // Return appropriate status code and response in case of an error
                return NotFound(new { Message = response.Description });
            }

            // Return success response
            return Ok(new { Message = "User deleted successfully." });
        }
    }
}

using DigiLog.Data;
using DigiLog.DTOs;
using DigiLog.Models;
using DigiLog.Models.ResponseModels;
using DigiLog.Services.Abstraction;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Policy;
using System.Text;

namespace DigiLog.Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly LogDbContext _context;
        private readonly AppSettings _appSettings;

        public UserService(LogDbContext context, IOptions<AppSettings> appSettings)
        {
            _context = context;
            _appSettings = appSettings.Value;
            _appSettings.GenerateRandomKey();
        }

        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
           var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Role, user.Department.DepartmentName),
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public ServiceResponse<string> CreateUser(RegisterUserDTO registerUserDto)
        {
            // Check if the department exists.
            var department = _context.Departments
                .FirstOrDefault(d => d.DepartmentName.ToLower() == registerUserDto.Department.ToLower());

            // If the department does not exist, return an error response.
            if (department == null)
            {
                return new ServiceResponse<string>
                {
                    HasError = true,
                    Description = "Department does not exist. Please select a valid department.",
                };
            }
            var user = new User
            {
                Username = registerUserDto.Username,
                Password = registerUserDto.Password,
                DepartmentId = department.DepartmentId,
                Department = department,
                DateCreated = DateTime.Now,
            };

            _context.Users.Add(user);
            _context.SaveChanges();
            return new ServiceResponse<string>();
        }

        public ServiceResponse<UserResponseDTO> Login (UserLoginDTO userLoginDTO)
        {
            var user = _context.Users
                .Include(u => u.Department)
                .FirstOrDefault(u => u.Username == userLoginDTO.Username);

            if (user == null)
            {
                return new ServiceResponse<UserResponseDTO>
                {
                    HasError = true,
                    Description = $"User with username {userLoginDTO.Username} not found.",
                };
            }

            if (user.Password != userLoginDTO.Password)
            {
                return new ServiceResponse<UserResponseDTO>
                {
                    HasError = true,
                    Description = $"Incorrect password.",
                };
            }

            var token = GenerateJwtToken(user);


            return new ServiceResponse<UserResponseDTO>
            {
                Data = new UserResponseDTO { Token = token, Username = user.Username, Department = user.Department.DepartmentName}
            };
        }

        public ServiceResponse<string> ChangePassword(ChangePasswordDTO changePasswordDto)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == changePasswordDto.Username);

            if (user == null)
            {
                return new ServiceResponse<string>
                {
                    HasError = true,
                    Description = $"User with username {changePasswordDto.Username} not found.",
                };
            }

            if (user.Password != changePasswordDto.OldPassword)
            {
                return new ServiceResponse<string>
                {
                    HasError = true,
                    Description = $"Incorrect password.",
                };
            }

            if (changePasswordDto.NewPassword != changePasswordDto.ConfirmNewPassword)
            {
                return new ServiceResponse<string>
                {
                    HasError = true,
                    Description = "New password and confirm new password do not match.",
                };
            }

            //Update the password
            user.Password = changePasswordDto.NewPassword;

            //Update the database
            user.DateModified = DateTime.Now;

            //Save changes to the database
            _context.SaveChanges();


            return new ServiceResponse<string> { Data = "Password changed Successfully. "};
        }

        public ServiceResponse<string> Logout()
        {
            return new ServiceResponse<string>();
        }

        public ServiceResponse<string> DeleteUser(string username)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == username);

            if (user == null)
            {
                return new ServiceResponse<string>
                {
                    HasError = true,
                    Description = $"User with username {username} not found.",
                };
            }

            _context.Users.Remove(user);
            _context.SaveChanges();

            return new ServiceResponse<string>();
        }


    }
}

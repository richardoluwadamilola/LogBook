using DigiLog.Data;
using DigiLog.DTOs;
using DigiLog.Models;
using DigiLog.Models.ResponseModels;
using DigiLog.Services.Abstraction;
using System.Security.Policy;

namespace DigiLog.Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly LogDbContext _context;

        public UserService(LogDbContext context)
        {
            _context = context;
        }

        public ServiceResponse<string> CreateUser(RegisterUserDTO registerUserDto)
        {
            var user = new User
            {
                Username = registerUserDto.Username,
                Password = registerUserDto.Password,
                Role = registerUserDto.Role,
                //DateCreated = DateTime.Now,
            };

            _context.Users.Add(user);
            _context.SaveChanges();
            return new ServiceResponse<string>();
        }

        public ServiceResponse<string> Login (UserLoginDTO userLoginDTO)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == userLoginDTO.Username);

            if (user == null)
            {
                return new ServiceResponse<string>
                {
                    HasError = true,
                    Description = $"User with username {userLoginDTO.Username} not found.",
                };
            }

            if (user.Password != userLoginDTO.Password)
            {
                return new ServiceResponse<string>
                {
                    HasError = true,
                    Description = $"Incorrect password.",
                };
            }

            return new ServiceResponse<string>
            {
                Data = user.Role,
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

            user.Password = changePasswordDto.NewPassword;
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

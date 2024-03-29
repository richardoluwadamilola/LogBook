﻿using DigiLog.DTOs;
using DigiLog.Models.ResponseModels;

namespace DigiLog.Services.Abstraction
{
    public interface IUserService
    {
        ServiceResponse<string> CreateUser(RegisterUserDTO registerUserDto);
        ServiceResponse<UserResponseDTO> Login(UserLoginDTO userLoginDTO);
        ServiceResponse<string> ChangePassword(ChangePasswordDTO changePasswordDTO);
        ServiceResponse<string> Logout();
        ServiceResponse<string> DeleteUser(string username);
    }
}

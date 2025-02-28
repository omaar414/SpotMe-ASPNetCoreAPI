using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpotMeAPI.DTOs.User;
using SpotMeAPI.Models;

namespace SpotMeAPI.Mappers
{
    public static class UserMapper
    {
        public static UserDto FromUserToUserDTO(this User model)
        {
            return new UserDto {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Username = model.Username,
            };
        }

        public static User FromCreateUserToUser(this CreateUserDto model)
        {
            return new User {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Username = model.Username,
                PasswordHash = model.Password,
                CreatedAt = DateTime.UtcNow,
            };
        }

        
    }
}